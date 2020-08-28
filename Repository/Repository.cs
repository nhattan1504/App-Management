using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Threading.Tasks;
using ManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using AppContext = ManagementApp.Models.AppContext;

namespace ManagementApp.Repository {
    public class Repository<TEntity>:IRepository<TEntity> where TEntity:class {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSetEntity;
        public Repository(DbContext context) {
            Context = context;
            DbSetEntity = Context.Set<TEntity>();
            }
        //public void Add() {

        //    }
        //public void Save(TEntity entity) {
        //    DbSetEntity.Update();
        //    }
        public void Add(TEntity entity) {
            DbSetEntity.Add(entity);
            Context.SaveChanges();
            }
        public void AddRange(IEnumerable<TEntity> entities) {
            DbSetEntity.AddRange(entities);
            }

        public void Edit(TEntity entity) {
            throw new NotImplementedException();
            }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) {
            return DbSetEntity.Where(predicate);
            //throw new NotImplementedException();
            }

        public TEntity Get(int id) {
            return DbSetEntity.Find(id);
            }

        public TEntity GetByIdNoTracking(Expression<Func<TEntity, bool>> predicate) {
            var item = DbSetEntity.AsNoTracking().Where(predicate).FirstOrDefault();
            Context.SaveChanges();
            return item;
            }

        public IEnumerable<TEntity> GetAll() {
            return DbSetEntity.ToList();
            }
        public IEnumerable<TEntity> Getpage() {
            return DbSetEntity.AsNoTracking().ToList();
            }

        public void Remove(TEntity entity) {
            DbSetEntity.Remove(entity);
            Context.SaveChanges();
            }

        public void RemoveRange(IEnumerable<TEntity> entities) {
            DbSetEntity.RemoveRange(entities);
            Context.SaveChanges();
            }

        public void Update(TEntity entity) {
            DbSetEntity.Update(entity);
            Context.SaveChanges();
            }

        public void Save() {
            throw new NotImplementedException();
            }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate) {
            return DbSetEntity.SingleOrDefault(predicate);
            }

        //private AppContext context;
        //public UserRepository(AppContext context) {
        //    this.context = context;
        //    }

        //public void DeleteUser(User user) {
        //    throw new NotImplementedException();
        //    }

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing) {
        //    if (!this.disposed)
        //        {
        //        if (disposing)
        //            {
        //            context.Dispose();
        //            }
        //        }
        //    this.disposed = true;
        //    }

        //public void Dispose() {
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //    }

        //public User GetUserByID(int userId) {
        //    return context.Users.Find(userId);
        //    }

        //public IEnumerable<User> GetUsers() {
        //    return context.Users.ToList();
        //    }

        //public void InsertUser(User user) {
        //    context.Users.Add(user);
        //    }

        //public void Save() {
        //    context.SaveChanges();
        //    }

        //public void UpdateUser(User user) {
        //    context.Entry(user).State = EntityState.Modified;
        //    }
        }
    }
