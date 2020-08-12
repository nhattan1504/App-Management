using ManagementApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppContext = ManagementApp.Models.AppContext;
namespace ManagementApp.WorkOfUnit {
    public class UnitOfWork {

        private readonly AppContext _context;

        public UnitOfWork(AppContext context) {
            _context = context;
            }

        public UserRepository Users { get { return new UserRepository(_context); } }
        public PostRepository Post { get { return new PostRepository(_context); } }


        public int Complete() {
            return _context.SaveChanges();
            }

        public void Dispose() {
            throw new NotImplementedException();
            }

        public Task SaveAsync() {
            throw new NotImplementedException();
            }


        //public int Complete() {
        //    return _context.SaveChanges();
        //    }

        //public void Dispose() {
        //    _context.Dispose();
        //    }

        }
    }
