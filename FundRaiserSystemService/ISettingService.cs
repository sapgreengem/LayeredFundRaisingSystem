using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    public interface ISettingService
    {
        IEnumerable<Setting> GetAll();
        Setting Get(int id);
        int Insert(Setting setting);
        int Update(Setting setting);
        int Delete(int id);
    }
}
