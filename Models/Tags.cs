using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementApp.Models {
    public class Tags {
        [Key]
        public int id { get; set; }
        public string tagName { get; set; }
        [ForeignKey("Post_FK")]
        public Posts Posts { get; set; }
        public int Post_FK { get; set; }
        }
    }
