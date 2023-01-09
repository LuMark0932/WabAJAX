using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Xml.Linq;
using WabAJAX.Models;

namespace WabAJAX.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _conetxt;
        private readonly IWebHostEnvironment _host;
        public ApiController(DemoContext conetxt,IWebHostEnvironment hpst)
        {
            _conetxt = conetxt;
            _host = hpst;
        }
        public IActionResult Index(string name,int age=20)
        {
            //System.Threading.Thread.Sleep(5000);
            if (string.IsNullOrEmpty(name))
                name = "Ajax";
            return Content($"Hello, {name}, You are {age} years old。", "text/plain",Encoding.UTF8);
        }
        public IActionResult Homework(Member member, IFormFile photo)
        {
            Member data =_conetxt.Members.FirstOrDefault(x=>x.Name == member.Name);
            if (data != null)
                return Content($"帳號 {member.Name}已存在。", "text/plain", Encoding.UTF8);
            else 
            {
                if(photo != null)
                {
                    string filename = photo.FileName;
                    string filePath = Path.Combine(_host.WebRootPath, "upload", filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
                member.FileName = filename;
                byte[]? imgbyte = null;
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    imgbyte = memoryStream.ToArray();
                }
                member.FileData = imgbyte;
                }
               
                _conetxt.Add(member);
                _conetxt.SaveChanges();
                return Content($"創建完成");
            }
        }

        public IActionResult Register (Member member, IFormFile photo)
        {
            string filename = photo.FileName;
            //實際路徑
            string filePath = Path.Combine(_host.WebRootPath, "upload", filename);
            
            //檔案上傳到uploads資料夾中
            using (var fileStream =new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }

            member.FileName = filename;
            //將圖轉成二進位
            byte[]? imgbyte = null;
            using(var memoryStream = new MemoryStream())
            {
                photo.CopyTo(memoryStream);
                imgbyte = memoryStream.ToArray();
            }
            member.FileData= imgbyte;

            //將會員資料寫進資料庫
            //_conetxt.Members.Add(member);
            _conetxt.Add(member);
            _conetxt.SaveChanges();

            return Content($"{filePath}");
        }


        //https://localhost:7026/Api/City
        public IActionResult City () 
        {
            var cities = _conetxt.Addresses.Select(x => new
            {
                x.City
            }).Distinct().OrderBy(x => x.City);
            return Json(cities);

        }
        //https://localhost:7026/Api/SiteId?City=金門縣
        public IActionResult SiteId( string City) 
        {
            var sites = _conetxt.Addresses.Where(x => x.City == City).Select(x => new
            {
                x.SiteId
            }).Distinct().OrderBy(x => x.SiteId);
            return Json(sites);
        }
        //https://localhost:7026/Api/Road?SiteId=金門縣金城鎮
        public IActionResult Road(string SiteId)
        {
            var roads =_conetxt.Addresses.Where(x=>x.SiteId== SiteId).Select(x => new
            {
                x.Road
            }).Distinct().OrderBy(x => x.Road);
            return Json(roads);
        }
    }
}
