using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutHunterV2.Models.DbModels;
using WorkoutHunterV2.Models.Student;

namespace WorkoutHunterV2.Controllers
{
    [Authorize(Roles = "C")]
    public class CoachController : Controller
    {
        private readonly WorkoutHunterContext _context;
        public CoachController(WorkoutHunterContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //找出身分為學生的資料
            var query = _context.UserInfos.Where(o => o.Role == "S");
            // 得到教練的UID
            string UID = User.Claims.Single(p => p.Type == "ID").Value;
            // 找出登錄這位教練的學生
            var query2 = query.Where(o => o.PT == UID);

            return View(await query2.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string str, string vit, string agi, string uid)
        {
            UserStatus query = _context.UserStatuses.Where(o => o.Uid == uid).FirstOrDefault();

            query.Strength += Convert.ToInt32(str);
            query.Vitality += Convert.ToInt32(vit);
            query.Agility += Convert.ToInt32(agi);

            _context.SaveChanges();

            //找出身分為學生的資料
            var query2 = _context.UserInfos.Where(o => o.Role == "S");
            // 得到教練的UID
            string UID = User.Claims.Single(p => p.Type == "ID").Value;
            // 找出登錄這位教練的學生
            var query3 = query2.Where(o => o.PT == UID);

            return View(await query3.ToListAsync());
        }

        public async Task<IActionResult> Register()
        {
            //找出身分為學生的資料
            var query = _context.UserInfos.Where(o => o.Role == "S").Where(o => o.PT == null);

            string UID = User.Claims.Single(p => p.Type == "ID").Value;

            return View(await query.ToListAsync());

        }

        [HttpPost]
        public async Task<IActionResult> Register(string str,string vit,string agi,string uid,string cid)
        {
            UserStatus Reg = _context.UserStatuses.Where(o => o.Uid == uid).FirstOrDefault();

            Reg.Strength = Convert.ToInt32(str);
            Reg.Vitality = Convert.ToInt32(vit);
            Reg.Agility = Convert.ToInt32(agi);

            UserInfo Info = _context.UserInfos.Where(o => o.Uid == uid).FirstOrDefault();

            Info.PT = cid;

            _context.SaveChanges();

            var query = _context.UserInfos.Where(o => o.Role == "S");

            string UID = User.Claims.Single(p => p.Type == "ID").Value;

            var query2 = _context.UserInfos.Where(o => o.PT == UID);

            return View("Index", await query2.ToListAsync());
        }
    }
}
