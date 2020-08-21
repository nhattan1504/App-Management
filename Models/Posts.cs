using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ManagementApp.Models {
    public class Posts {
        public int id { get; set; }
        public string content { get; set; }
        //[DisplayName("Upload image")]
        public string title { get; set; }

        [Column(TypeName="nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; }
        //public HttpPostAttribute ImageFile { get; set; }
        public bool isAccept { get; set; }
        public User User { get; set; }
        }
    }
