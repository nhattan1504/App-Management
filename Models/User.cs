using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementApp.Models {
    public class User {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int id { get; set; }
        public bool isAdmin { get; set; }

        public ICollection<Posts> Postss { get; set; }
        }
    }
