using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutHunterV2.Models.DbModels;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace WorkoutHunterV2.Models.Adventure
{
    public class AdventureModel
    {
        private WorkoutHunterContext _context;
        private ViewHunter VH;
        private ViewMonster VM;
        private int MonsterMoney;
        private string _UID;
        private double diffTime;
        public AdventureModel(WorkoutHunterContext context, string UID)
        {
            MonsterMoney = 0;
            _context = context;
            _UID = UID;
            var queryMonster = (from o in _context.Monsters select o).ToList();
            var queryHunter = (from a in _context.GameProgresses
                               join b in _context.UserStatuses
                               on a.Uid equals b.Uid
                               join c in _context.CharacterItemSkills
                               on a.Uid equals c.Uid
                               where a.Uid == _UID
                               select new CHunterData
                               {
                                   LastDate = a.StartTime,
                                   SavePoint = a.SavePoint,
                                   Agi = b.Agility,
                                   Str = b.Strength,
                                   Vit = b.Vitality,
                                   HPic = c.ChaPic,
                                   NowSkill = c.NowSkill,
                                   NowItem = c.NowItem,
                                   Money = c.Money,
                               }).FirstOrDefault();
            var querySkills = (from o in _context.Skills select o).ToList();
            var queryItems = (from o in _context.Items select o).ToList();
            VM = new ViewMonster(queryMonster, queryHunter.SavePoint);
            VH = new ViewHunter(queryHunter, queryHunter.SavePoint, querySkills, queryItems);
            diffTime = (DateTime.Now - DateTime.Parse(queryHunter.LastDate)).TotalSeconds;
        }
        public ViewModel Load()
        {
            
            CWhoWin round;
            int SpendTime;
            double number = 0;
            while (diffTime > 0)
            {
                // 若場次不只一次，每次更新模組
                if(number != 0)
                {
                    VH.InitialLoad();
                    VM.InitialLoad();
                }
                // 計算這回合需要花費的時間
                round = RoundSpendTime(VH, VM);
                SpendTime = round.SpendTime;
                // 若紀錄的時間扣完此回合還大於零，進行回合模擬
                if (diffTime - SpendTime > 0)
                {
                    if (round.WhoWin == EWhoWin.Hunter)
                    {
                        MonsterMoney += VM.Money;
                        VM.CPoint += 1;
                    }
                    else if (VM.CPoint > 1)
                    {
                        VM.CPoint -= 1;
                    }
                    diffTime -= SpendTime;
                }
                // 打不完一回合計算剩下血量
                else
                {
                    RoundHalfway(Convert.ToInt32(diffTime));
                    // 一個回合以上才進行存檔
                    if(number != 0)
                        SaveGameProgresses();
                    
                    diffTime = 0;
                }
                number++;
            }

            return new ViewModel {
                VH = VH,
                VM = VM,
            };
        }
        private CWhoWin RoundSpendTime(ViewHunter simVH, ViewMonster simVM)
        {
            double VMHP = simVM.HP;
            double VHDPS = simVH.DPS;
            double VHHP = simVH.HP;
            double VMAD = simVM.AD;
            int roundTime = 0;
            for(int i = 0; true; i++)
            {
                roundTime++;
                VMHP -= Convert.ToInt32(VHDPS);
                if (i % simVM.AtkInterval == 0)
                    VHHP -= VMAD;

                if (VMHP < 0 && VHHP < 0)
                {
                    return new CWhoWin()
                    {
                        WhoWin = EWhoWin.Both,
                        SpendTime = roundTime,
                    };
                }
                else if (VMHP < 0)
                {
                    return new CWhoWin()
                    {
                        WhoWin = EWhoWin.Hunter,
                        SpendTime = roundTime,
                    };
                }
                else if (VHHP < 0) 
                {
                    return new CWhoWin()
                    {
                        WhoWin = EWhoWin.Monster,
                        SpendTime = roundTime,
                    };
                }
            }
        }
        private void RoundHalfway(int time)
        {
            for (int i = 0; i < time; i++)
            {
                VM.HP -= Convert.ToInt32(VH.DPS);
                if (VM.AtkCheck())
                    VH.HP -= VM.AD;
            }
            if (VH.HP < 0) VH.HP = 0;
            if (VM.HP < 0) VM.HP = 0;
        }
        public void SaveGameProgresses()
        {
            
            int[] T = new int[] {
                Convert.ToInt32(((VH.maxHP != 0)? VH.HP / VH.maxHP : 0) * 100),
                Convert.ToInt32(((VM.maxHP != 0)? VM.HP / VM.maxHP : 0) * 100),
                VM.GetPic(),
                VM.AtkTime,
                VM.CPoint,
            };
            var DbGamePorgresses = _context.GameProgresses.Single(p => p.Uid == _UID);
            DbGamePorgresses.SavePoint = JsonConvert.SerializeObject(T);
            DbGamePorgresses.StartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            var DbCharacterItemSkills = _context.CharacterItemSkills.Single(p => p.Uid == _UID);
            DbCharacterItemSkills.Money += MonsterMoney;
            _context.SaveChanges();
        }
        public void SaveRoundOver(int CPoint, int Money)
        {
            VM.CPoint = CPoint;
            VH.InitialLoad();
            VM.InitialLoad();
            int[] T = new int[] {
                100,
                100,
                VM.GetPic(),
                VM.AtkTime,
                VM.CPoint,
            };
            var DbGamePorgresses = _context.GameProgresses.Single(p => p.Uid == _UID);
            DbGamePorgresses.SavePoint = JsonConvert.SerializeObject(T);
            DbGamePorgresses.StartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            var DbCharacterItemSkills = _context.CharacterItemSkills.Single(p => p.Uid == _UID);
            DbCharacterItemSkills.Money += Money;
            _context.SaveChanges();
        }

    }
    
    public class CWhoWin
    {
        public EWhoWin WhoWin { get; set; }
        public int SpendTime { get; set; }
    }
    public class CItem
    {

        public int StrBuff { get; set; }
        public int VitBuff { get; set; }
        public int AgiBuff { get; set; }
        public CItem(int NowItem, List<Item> Items)
        {
            StrBuff = 0;
            VitBuff = 0;
            AgiBuff = 0;
            if(NowItem != 0)
            {
                var query = Items.Where(p => p.Iid == NowItem).FirstOrDefault();
                int[] ItemBuff = JsonConvert.DeserializeObject<int[]>(query.Buff);
                if (ItemBuff.Length != 2)
                    throw new Exception("道具Buff字串異常");
                if (ItemBuff[0] == 1)
                    StrBuff = ItemBuff[1];
                else if (ItemBuff[0] == 2)
                    VitBuff = ItemBuff[1];
                else if (ItemBuff[0] == 3)
                    AgiBuff = ItemBuff[1];
            }
        }
    }
    public class CSkill
    {
        public int DmgSkill { get; set; }
        public int CD { get; set; }
        public string PicSkill { get; set; }
        public CSkill(int NowSkill, List<Skill> Skills)
        {
            if(NowSkill != 0)
            {
                var query = Skills.Where(p => p.Sid == NowSkill).FirstOrDefault();
                DmgSkill = query.SkillDamage ?? 0;
                PicSkill = query.SkillPic;
                CD = query.Cd ?? 0;
            }
        }
    }
    public enum EBuff : byte
    {
        None = 0,
        Str = 1,
        Vit = 2,
        Agi = 3,
    }
    public enum EWhoWin : byte
    {
        None = 0,
        Hunter = 1,
        Monster = 2,
        Both = 3,
    }
    public class ViewModel
    {
        public ViewHunter VH { get; set; }
        public ViewMonster VM { get; set; }
    }
}
