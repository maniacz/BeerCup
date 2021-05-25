using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<List<T>> GetAll();
        public Task<T> GetById(int id);
        public Task Insert(T entity);
        public Task Update(T entity);
        public Task Delete(int id);
    }
}
