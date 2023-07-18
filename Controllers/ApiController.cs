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

        [HttpPost]
        public IActionResult Register(Members member, IFormFile photo)
        {
            //string photoInfo=$"{photo.FileName}-{photo.Length}-{photo.ContentType}";
            //***檔案上傳***
            string rootPath = Path.Combine(_host.WebRootPath, "uploads", photo.FileName);

            using (var fileStream = new FileStream(rootPath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }

            //***寫進資料庫***
            member.FileName = photo.FileName;
            byte[]? photoByte = null;
            using (var memoryStream = new MemoryStream())
            {
                photo.CopyTo(memoryStream);
                photoByte = memoryStream.ToArray();
            }
            member.FileData = photoByte;

            _context.Members.Add(member);
            _context.SaveChanges();


            return Content(rootPath);
        }

        public IActionResult GetImageByte(int id = 0)
        {
            Members? _member = _context.Members.Find(id);
            byte[]? img = _member.FileData;
            return File(img, "image/jpeg");

        }

        public IActionResult GetMemebersName()
        {
            var membersName = _context.Members.Select(c => c.Name);
            return Json(membersName);
        }

    }
}
