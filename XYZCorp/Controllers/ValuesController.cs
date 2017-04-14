using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XYZCorp.Models;
using Newtonsoft.Json;

namespace XYZCorp.Controllers
{
    
    public class UsersController : ApiController
    {
        List<User> users = new List<User> {  
            new User { id = 1, name = "Fred", points = 4 },
            new User { id = 2, name = "Max", points=5 },
            new User { id = 3, name = "Larry", points = 3 }
        }; // static list of users implemented
        
        // GET api/users
        public string GetAllUsers()
        {
            return JsonConvert.SerializeObject(users); // return json encoded list of objects
        }

        // GET api/users/{id}
        public string Get(int id)
        {
            var user = users.FirstOrDefault((u) => u.id == id); //check user with such id
            if (user == null) {  // if no such user, then variable's value is null
                return "Error: No user with such id!";
            } 
            return JsonConvert.SerializeObject(user); // otherwise, return json encoded user
        }

        // POST api/users
        public string Post([FromBody]User userJason) // accept json of user
        {
            var matches = users.Where(p => p.name == userJason.name);  //check user with such name          
            if (!matches.Any()) { // If no match in name, it's ok
                users.Add(userJason); // add to list
                return JsonConvert.SerializeObject(userJason.id); // return id of added
            }
            return "Error: User with such name already exists!";  // If match in name, then not ok, return error message          
        }

        // POST api/setPoints
        [Route("api/setpoints")]  // Two posts in one controller will conflict, so set another route
        public string SetPoints([FromBody]Point point) {  
            var matches = users.Where(p => p.id == point.id); // Find user with such id
            if (!matches.Any()) {  // No user with such id: error message
                return "Error: No user with such id!";
            }
            // otherwise, set the points to the object that matches
            users.Where(p => p.id == point.id).ElementAt(0).points = point.points;
            return "Successfully done!";
        }
    }
}
