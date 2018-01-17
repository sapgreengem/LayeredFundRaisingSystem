using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    public interface IAdministrationService
    {
        IEnumerable<Administration> GetAll();
        Administration Get(int id);
        int Insert(Administration admin);
        int Update(Administration admin);
        int Delete(int id);
    }
}
