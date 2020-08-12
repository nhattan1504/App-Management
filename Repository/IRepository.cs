using ManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagementApp.Repository {
    interface IRepository<TEntity> where TEntity:class {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        bool CheckExist();
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Edit(TEntity entity);
        void Save();
        //User GetUserByID(int userId);
        //void InsertUser(User user);
        //void DeleteUser(User user);
        //void UpdateUser(User user);
        //void Save();
        }
    }
