using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WebAPITest.Models;

namespace WebAPITest.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("[controller]/WithinLondon")]
        [HttpGet]
        public ActionResult<User> GetAllUsersInLondon()
        {
            List<User> users = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://bpdts-test-app.herokuapp.com/city/London/users");
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
                List<User> usersWithinRange = GetUserWithinRange(50);

                usersWithinRange.ForEach(u => users.Add(u));

                return Ok(users);
            }

            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }

            return NoContent();
        }

        private List<User> GetUserWithinRange(int distance)
        {
            List<User> filteredUsers = new List<User>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://bpdts-test-app.herokuapp.com/users");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string responseStream;

                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseStream = streamReader.ReadToEnd();
                }

                var serializer = new JavaScriptSerializer();
                var users = serializer.Deserialize<List<User>>(responseStream);

                foreach (User user in users)
                {
                    if (user.IsWithinDistance(user.latitude, user.longitude, distance))
                    {
                        filteredUsers.Add(user);
                    }
                }

                return filteredUsers;
            }

            return filteredUsers;
        }
    }
}
