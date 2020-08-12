using ManagementApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementApp.WorkOfUnit {
    interface IUnitOfWork:IDisposable {
        IUserRepository Users { get; }
        IPostRepository Post { get; }
        int complete();
        //Task SaveAsync();
        }
    }
