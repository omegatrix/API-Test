using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
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

            try
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
            }

            catch (Exception ex)
            {
                
                return NotFound();
            }

            return NoContent();
        }

        private List<User> GetUserWithinRange(int distance)
        {
            List<User> filteredUsers = new List<User>();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://bpdts-test-app.herokuapp.com/users");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                HttpStatusCode httpStatusCode = response.StatusCode;

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
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            
            return filteredUsers;
        }
    }
}
