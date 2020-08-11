﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementApp.Models {
    public class Posts {
        public int id { get; set; }
        public string content { get; set; }
        [DisplayName("Upload image")]
        public string imageUrl { get; set; }
        public HttpPostAttribute ImageFile { get; set; }
        public bool isAccept { get; set; }
        public User User { get; set; }
        }
    }
