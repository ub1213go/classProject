using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkoutHunterV2.Models.DbModels;
using Newtonsoft.Json;

namespace WorkoutHunterV2.Models.Adventure
{
    public class ViewMonster
    {
        public readonly List<Monster> _MonsterList;

        public int CPoint { get; set; }
        public int Money { get; set; }
        public double HP { get; set; }
        public double maxHP { get; set; }
        public double AD { get; set; }
        public string Pic { get; set; }

        public int AtkInterval { get; set; }
        public int MoneyCoefficient { get; set; }
        public int HPCoefficient { get; set; }
        public int ADCoefficient { get; set; }
        public int RangeCoefficient { get; set; }

        public int AtkTime { get; set; }

        public ViewMonster(List<Monster> monsterlist, string SavePoint)
        {
            _MonsterList = monsterlist;
            MoneyCoefficient = 1;
            HPCoefficient = 100;
            ADCoefficient = 3;
            RangeCoefficient = 50;
            AtkInterval = 5;
            if (SavePoint != null)
            {
                int[] SaveDataList = JsonConvert.DeserializeObject<int[]>(SavePoint);
                CPoint = SaveDataList[4];
                InitialLoad();
                SetPic(SaveDataList[2]);
                AtkTime = SaveDataList[3];
                HP = (double)SaveDataList[1] / 100 * maxHP;
            }
            else
            {
                InitialLoad();
            }
        }

        public void InitialLoad()
        {
            Random R = new Random();
            int number = R.Next(1, _MonsterList.Count());
            if (CPoint == 0) CPoint = 1;
            Pic = _MonsterList.Where(p => p.Mid == number).Select(p => p.MonsterPic).FirstOrDefault();
            Money = MoneyCoefficient * CPoint + (MoneyCoefficient * CPoint * R.Next(0, RangeCoefficient) / 100);
            AD = ADCoefficient * CPoint;
            maxHP = HPCoefficient * CPoint;
            HP = maxHP;
        }
        public void SetPic(int MID)
        {
            Pic = _MonsterList.Where(p => p.Mid == MID).Select(p => p.MonsterPic).FirstOrDefault();
        }
        public int GetPic()
        {
            return int.Parse(Regex.Match(Pic, "(\\d*)[.]").Groups[1].ToString());
        }
        public bool AtkCheck()
        {
            if (AtkTime == 5) throw new Exception("AtkTime 異常");
            AtkTime++;
            if(AtkTime == 5)
            {
                AtkTime = 0;
                return true;
            }
            return false;
        }
    }
}
