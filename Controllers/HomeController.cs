using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAuth.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MVCAuth.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Reflection;

namespace MVCAuth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbcontext;
        public HomeController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CountryDetails(string countryData)
        {
            // Note : you can bind same list from database  

            // Searching records from list using LINQ query  
            // var CountryList = (from N in ObjList  
            //                 where N..StartsWith(Prefix)  
            //               select new { N.CityName});  
            // return Json(CountryList, JsonRequestBehavior.AllowGet); 
            // RootObject data = new RootObject();
            using (var client = new HttpClient())
            {
                // string countryData = Request.Form["SearchString"];
                client.BaseAddress = new Uri("https://restcountries.eu/rest/v2/name/" + countryData);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<RootObject>(data);
                    return View(result);
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }

            }
            return View();

        }
        public async Task<JsonResult> Search(string searchString)
        {
            var countries = from m in _dbcontext.Country
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                countries = countries.Where(s => s.CountryName.Contains(searchString));
            }

            return Json(await countries.ToListAsync());
        }
        public async Task<IActionResult> About(int page =0)
        {
            try
            {
                var fav = string.Join(",", (from m in _dbcontext.Favorites
                                            where m.UserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)
                                            select m.CountryId).ToArray()).ToString();


                List<JObject> r = await GetData(fav);
                List<RootObject> ro = new List<RootObject>();
                //ro.flag= r.flag;
                foreach (JObject o in r)
                {
                   RootObject obj = o.ToObject<RootObject>();
                   ro.Add(obj);
                }
                
               const int PageSize = 2; // you can always do something more elegant to set this

                var count = ro.Count();

                var data = ro.Skip(page * PageSize).Take(PageSize).ToList();

                this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

                this.ViewBag.Page = page;

                return View(data);

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);

            }
            return View(null);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult AddFavCountry(string countryCode, string likeIndicator)
        {
            Favorites fc = null;
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (likeIndicator.Equals("Like"))
            {
                fc = new Favorites();
                fc.CountryId = countryCode;
                fc.UserId = userId;
                _dbcontext.Favorites.Add(fc);
            }
            else
            {
                fc = _dbcontext.Favorites.FirstOrDefault(x=>x.CountryId.Equals(countryCode)&&x.UserId.Equals(userId));
                _dbcontext.Favorites.Remove(fc);
            }
            _dbcontext.SaveChanges();
            return Json(true);
        }

        public async Task<List<JObject>> GetData(string fc)
        {
            List<JObject> dataList = new List<JObject>();
            foreach (string f in fc.Split(","))
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://restcountries.eu/rest/v2/alpha/" + f);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        dataList.Add(JObject.Parse(data));

                    }
                }
            }


            return dataList;

        }

    public bool GetFavCountry(string countryCode)
    {
         var fc = from m in _dbcontext.Favorites
                        where m.CountryId==countryCode
                        && m.UserId== this.User.FindFirstValue(ClaimTypes.NameIdentifier)
                            select m.CountryId;
       if(fc.ToList().Count > 0)
       {
             return true;
       }
       else{
           return false;
       }
       
    }
    }
}
