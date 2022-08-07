using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DemoProject1.Data;
using DemoProject1.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DemoProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private ApplicationDBContext _appDb;

        public UserController(ApplicationDBContext appDb)
        {
            _appDb = appDb;
        }
        [HttpGet,Route("GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> getAllUsers()
        {
            var users = await _appDb.TestUsers.ToListAsync();
            return Ok(users);
        }
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            //return new string[] { "value1", "value2" };
            var client = new HttpClient();
            string url = string.Format("https://jsonplaceholder.typicode.com/todos");
            var response = client.GetAsync(url).Result;

            string result = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(result);
            foreach (var user in users)
            {
                var obj = _appDb.TestUsers.FirstOrDefault(x => x.id == user.id);
                if (obj != null)
                {
                    _appDb.SaveChanges();
                }
                else
                {
                    _appDb.TestUsers.Add(user);
                    _appDb.SaveChanges();
                }
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _appDb.TestUsers.FirstOrDefault(x => x.id == id);
            return user;
        }

        [HttpPut, Route("Update")]
        public void Update([FromBody] User obj)
        {
            if(obj != null)
            {
                _appDb.Update(obj);
                _appDb.SaveChanges();
            }
        }
    }
}