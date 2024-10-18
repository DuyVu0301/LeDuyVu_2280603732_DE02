using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LoaiSPService
    {
        public List<LoaiSP> GetAll()
        {
            Model1 Model1 = new Model1();
            return Model1.LoaiSPs.ToList();
        }
    }
}
