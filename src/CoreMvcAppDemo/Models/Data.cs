using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class DataService
    {
        private readonly List<Data> _data = new List<Data>()
        {
            new Data { Id = 1, Name = "Kimberly Kim", Title = "President" },
            new Data { Id = 2, Name = "Jason Statum", Title = "Programmer" },
            new Data { Id = 3, Name = "Gloria Estefan", Title = "Accountant" }
        };

        public List<Data> GetAll()
        {
            return _data;
        }

        public Data GetDataById(int id)
        {
            return _data.Where(n => n.Id == id).SingleOrDefault();
        }

        public List<Data> GetDataByName(string name)
        {
            return _data.Where(n => n.Name.ToLower().Equals(name.ToLower())).ToList();
        }
    }

    public class DataFinder
    {
        private DataService _service = new DataService();
        public async Task<Data> GetDataById(int id)
        {
            return await Task.FromResult(_service.GetDataById(id));
        }
    }
}
