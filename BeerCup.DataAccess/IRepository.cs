using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.DataAccess
{
    public interface IRepository<T> where T : EntityBase
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public void Insert(T entity);
        public void Update(T entity);
        public void Delete(int id);
    }
}
