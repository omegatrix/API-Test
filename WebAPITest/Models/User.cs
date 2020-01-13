using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITest.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string first_name { get; set; }

        [Required]
        [StringLength(100)]
        public string last_name { get; set; }

        [StringLength(100)]
        public  string email { get; set; }

        [StringLength(15)]
        public string ip_address { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }
    }
}