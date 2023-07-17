using AjaxHMK.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace AjaxHMK.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;

        public ApiController(DemoContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckAccount(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var member = _context.Members.FirstOrDefault(p => p.Name.Equals(Name));
                if(member != null)
                {
                    return Content("此帳號已存在");
                }
                else
                {
                    return Content("此帳號可使用");
                }
            }
            else
            {
                return Content("此帳號可使用");
            }
        }

        //載入縣市
        public IActionResult Cities()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
            return Json(cities);
        }

        //根據縣市載入鄉鎮區
        public IActionResult Districts(string city)
        {
            var districts = _context.Address.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        //根據鄉鎮區載入路名
        public IActionResult Roads(string SiteId)
        {
            var roads = _context.Address.Where(a => a.SiteId == SiteId).Select(a => a.Road);
            return Json(roads);
        }

    }
}
