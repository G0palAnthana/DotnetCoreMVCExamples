using DAGR.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAGR.Data.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
