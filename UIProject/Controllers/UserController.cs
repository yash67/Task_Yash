using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UIProject.Controllers
{
    public class UserController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient obj = new HttpClient();
            var users = await obj.GetAsync("https://localhost:44341/api//user/GetAll");
            string result = await users.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<User>>(result);
            return View(list);
        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpClient obj = new HttpClient();
            var users = await obj.GetAsync("https://localhost:44341/api/user");
            string result = await users.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<User>>(result);
            var user = list.FirstOrDefault(x => x.id == id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit([FromForm] User user)
        {
            try
            {
                HttpClient obj = new HttpClient();
                var content = JsonConvert.SerializeObject(user);
                var result = obj.PutAsync("https://localhost:44341/api/user/Update", new StringContent(content, Encoding.Default, "application/json"));
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}