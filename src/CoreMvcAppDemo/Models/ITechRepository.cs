using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models
{
    public interface ITechRepository
    {
        void AddTech(Tech model);
        List<Tech> GetTeches();
    }
}
