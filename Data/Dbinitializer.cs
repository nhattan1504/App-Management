using ManagementApp.Models;
using Microsoft.EntityFrameworkCore.Internal;
//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementApp.Data {
    public class Dbinitializer {
        public static void Inittialize(AppContext context) {
            context.Database.EnsureCreated();
            if (context.Users.Any())
                {
                return;
                }
            var users = new User[] {
                new User{name="tin",email="1@gmail.com",password="1",isAdmin=true},
                new User{name="tan",email="2@gmail.com",password="1",isAdmin=false}
                };
            foreach (User s in users)
                {
                context.Users.Add(s);
                }
            context.SaveChanges();
            var posts = new Posts[] {
                new Posts{content="mlem",title="C://Users//tannguyen//Pictures//digital-art-night-scenery-forest-uhdpaper.com-hd-100.jpg",isAccept=true},
                new Posts{content="mlemmmmmmmmmm",title="C://Users//tannguyen//Pictures//digital-art-night-scenery-forest-uhdpaper.com-hd-100.jpg",isAccept=false}
                };
            foreach (Posts s in posts)
                {
                context.Postss.Add(s);
                }
            context.SaveChanges();
            //var tags = new Tag[] {
            //    new Tag{tagName="tan"}
            //    };
            //foreach (Tag s in tags)
            //    {
            //    context.Tags.Add(s);
            //    }
            //context.SaveChanges();
            }

        }
    }
