using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkoutHunterV2.Models.DbModels;

namespace WorkoutHunterV2.Models.Adventure
{
    public class ViewHunter
    {

        public CSkill NowSkill { get; set; }
        public CItem NowItem { get; set; }

        public double DPS { get; set; }
        public double HP { get; set; }
        public double maxHP { get; set; }

        public int ADCoefficient { get; set; }
        public int HPCoefficient { get; set; }
        public int ASCoefficient { get; set; }

        public double Str { get; set; }
        public double Vit { get; set; }
        public double Agi { get; set; }

        public ViewHunter(CHunterData HunterData, string SavePoint, in List<Skill> querySkills, in List<Item> queryItems)
        {
            ADCoefficient = 10;
            HPCoefficient = 10;
            ASCoefficient = 500;
            Str = HunterData.Str ?? 0;
            Agi = HunterData.Agi ?? 0;
            Vit = HunterData.Vit ?? 0;
            NowItem = new CItem(HunterData.NowItem ?? 0, queryItems);
            NowSkill = new CSkill(HunterData.NowSkill ?? 0, querySkills);
            
            InitialLoad();
            if (SavePoint != null)
            {
                int[] SaveDataList = JsonConvert.DeserializeObject<int[]>(SavePoint);
                HP = (double)SaveDataList[0] / 100 * maxHP;
            }
        }

        public void InitialLoad()
        {
            maxHP = (Vit + NowItem.VitBuff) * HPCoefficient;
            HP = maxHP;
            DPS = ((Str + NowItem.StrBuff) * ADCoefficient) * ((Agi + NowItem.AgiBuff) / ASCoefficient);
            if (NowSkill.DmgSkill != 0) NowSkill.DmgSkill = Convert.ToInt32(DPS / 10) * NowSkill.DmgSkill;
        }
        public void ViewLoad(string PercentHP)
        {
            InitialLoad();
            var Reg = Regex.Match(PercentHP, "[[](\\d*), (\\d*)[]]");
            HP = double.Parse(Reg.Groups[1].ToString()) / 100 * maxHP;
        }
    }

    public class CHunterData
    {
        public string LastDate{get;set;}
        public string HPic{get;set;}
        public string SavePoint{get;set;}
        public int? Agi{get;set;}
        public int? Str{get;set;}
        public int? Vit{get;set;}
        public int? NowSkill{get;set;}
        public int? NowItem{get;set;}
        public int? Money { get; set; }
    }
}
