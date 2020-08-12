using ManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppContext = ManagementApp.Models.AppContext;
namespace ManagementApp.Repository {
    public class PostRepository:Repository<Posts>, IPostRepository{
        public PostRepository(AppContext context) : base(context) {
            }
        public AppContext AppContext {
            get { return Context as AppContext; }
            }
        }
    }
    
