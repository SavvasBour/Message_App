using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<Model>
        where Model : class
    {
        List<Model> GetAll();
        Model GetById(int id);
        void Add(Model entity);
        void Delete(int id);
        void Update(Model entity);
        Dictionary<string, object> GetValuesDictionary(Model entity, bool isForUpdate);
    }
}