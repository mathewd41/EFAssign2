using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Repo : IRepo
    {
        private DataContext dataContext { get; set; }
        public Repo()
        {
            dataContext=new DataContext();
        }
        public void Create<T>(T entity) where T : BaseEntity
        {
            var dbSet=dataContext.Set<T>();
            entity.CreateDateTime = DateTime.Now;
            entity.UpdateDateTime = DateTime.Now;
            dbSet.Add(entity);
            dataContext.SaveChanges();

        }

        public void Delete<T>(int id) where T : BaseEntity
        {
            var dbSet = dataContext.Set<T>();
            var entity = dbSet.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                entity.IsDeleted = true;
            }
            dataContext.SaveChanges();
        }

        public T Read<T>(int id) where T : BaseEntity
        {
            var dbSet = dataContext.Set<T>();
            var entity = dbSet.FirstOrDefault(x=>x.Id==id);
            if (entity != null)
            {
                return entity;
            }
            else
            {
                Console.WriteLine("Select a valid user id");
                return null;
            }
           
            
        }
      
        public void Update<T>(T entity) where T : BaseEntity
        {
            var dbSet = dataContext.Set<T>();
            var foundEntity = dbSet.FirstOrDefault(x => x.Id == entity.Id);
            if(foundEntity != null)
            {
                entity.UpdateDateTime = DateTime.Now;
                foundEntity=entity;
                //dataContext.Update(entity);
                dataContext.SaveChanges();
            }
            
        }
    }
}
