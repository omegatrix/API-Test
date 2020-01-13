using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using WebAPITest.Models;

namespace WebAPITest.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("api/[controller]/InLondon")]
        [HttpGet]
        public List<User> GetUsersInLondon()
        {
            List<User> users = null;
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://bpdts-test-app.herokuapp.com/city/London/users");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseStream;

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseStream = streamReader.ReadToEnd();
                }

                var serializer = new JavaScriptSerializer();
                users = serializer.Deserialize<List<User>>(responseStream);
            }
         
            return users;
        }

        [Route("api/[controller]/WithinDistance")]
        [HttpGet]
        public List<User> GetUserWithinRange()
        {
            const int Distance = 50;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://bpdts-test-app.herokuapp.com/users");

            WebResponse response = request.GetResponse();
            string responseStream;

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseStream = streamReader.ReadToEnd();
            }

            var serializer = new JavaScriptSerializer();
            var users = serializer.Deserialize<List<User>>(responseStream);
            
            List<User> filteredUsers = new List<User>();

            foreach (User user in users)
            {
                if (user.IsWithinDistance(user.latitude, user.longitude, Distance))
                {
                    filteredUsers.Add(user);
                }
            }

            return filteredUsers;
        }
    }
}
