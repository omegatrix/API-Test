using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [Route("/WithinLondon")]
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

                    users = JsonConvert.DeserializeObject<List<User>>(responseStream);
                    List<User> usersWithinRange = GetUserWithinRange(50);

                    usersWithinRange.ForEach(u => users.Add(u));

                    return Ok(users);
                }
            }

            catch (Exception ex)
            {
                
                return BadRequest();
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

                    var users = JsonConvert.DeserializeObject<List<User>>(responseStream);

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

        [Route("/AllUsers")]
        [HttpGet]
        public ActionResult<User> GetUsers()
        {
            try
            {
                List<User> users = null;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://bpdts-test-app.herokuapp.com/users");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseStream;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseStream = streamReader.ReadToEnd();
                    }

                    users = JsonConvert.DeserializeObject<List<User>>(responseStream);
                   
                    return Ok(users);
                }
            }

            catch (Exception ex)
            {

                return BadRequest();
            }

            return NoContent();
        }

        [Route("/User/{id}")]
        [HttpGet]
        public ActionResult<User> GetUserById(string id)
        {
            try
            {
                User user = null;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://bpdts-test-app.herokuapp.com/user/" + id.ToString());
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseStream;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseStream = streamReader.ReadToEnd();
                    }

                    user = JsonConvert.DeserializeObject<User>(responseStream);

                    return Ok(user);
                }
            }

            catch (Exception ex)
            {

                return NotFound("Id '" + id + "' doesn't exist. You have requested this URI [/User/" + id + "] but did you mean /user/<string:id> or /AllUsers ?");
            }

            return NoContent();
        }

        [Route("/Users/{city}")]
        [HttpGet]
        public ActionResult<User> GetUsersByCity(string city)
        {

            try
            {
                List<User> users = null;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://bpdts-test-app.herokuapp.com/city/"+ city + "/users");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseStream;

                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseStream = streamReader.ReadToEnd();
                    }

                    users = JsonConvert.DeserializeObject<List<User>>(responseStream);

                    return Ok(users);
                }
            }

            catch (Exception ex)
            {

                return NotFound("City '" + city + "' doesn't exist. You have requested this URI [/Users/" + city + "]");
            }

            return NoContent();
        }
    }
}
