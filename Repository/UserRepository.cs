using ManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppContext= ManagementApp.Models.AppContext;
namespace ManagementApp.Repository {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository(AppContext context) : base(context) {
            }
        }
    }
    
