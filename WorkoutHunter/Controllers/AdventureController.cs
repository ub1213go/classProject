using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutHunterV2.Models.Adventure;
using WorkoutHunterV2.Models.DbModels;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WorkoutHunterV2.Controllers
{
    [Authorize]
    public class AdventureController : Controller
    {
        private WorkoutHunterContext _context;
        public AdventureController(WorkoutHunterContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string UID = User.Claims.Single(p => p.Type == "ID").Value;
            AdventureModel AdModel = new AdventureModel(_context, UID);
            return View(AdModel.Load());
        }
        public async Task<IActionResult> NewRound()
        {
            string UID = User.Claims.Single(p => p.Type == "ID").Value;
            AdventureModel AdModel = new AdventureModel(_context, UID);
            // 抓取data
            var b = await Request.BodyReader.ReadAsync();
            string str = Encoding.UTF8.GetString(b.Buffer);
            JObject data = JsonConvert.DeserializeObject<JObject>(str);
            int Stage = data["Stage"].Value<int>();
            int Money = data["Money"].Value<int>();
            // 回合結束的存檔
            AdModel.SaveRoundOver(Stage, Money);

            return Content("OK");
        }
    }
}
