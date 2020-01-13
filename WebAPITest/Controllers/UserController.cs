using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using WebAPITest.Models;

namespace WebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        public List<User> Get()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://bpdts-test-app.herokuapp.com/city/London/users");

            WebResponse response = request.GetResponse();
            string responseStream;

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseStream = streamReader.ReadToEnd();
            }

            var serializer = new JavaScriptSerializer();
            var users = serializer.Deserialize<List<User>>(responseStream);

            return users;
        }

        [HttpGet]
        public List<User> GetUsersWithinRange()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://bpdts-test-app.herokuapp.com/city/London/users");

            WebResponse response = request.GetResponse();
            string responseStream;

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseStream = streamReader.ReadToEnd();
            }

            var serializer = new JavaScriptSerializer();
            var users = serializer.Deserialize<List<User>>(responseStream);

            return users;
        }
    }
}
