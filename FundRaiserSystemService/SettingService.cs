using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    class SettingService: ISettingService
    {
        private ISettingDataAccess data;
        public SettingService(ISettingDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<Setting> GetAll()
        {
            return this.data.GetAll();
        }
        public Setting Get(int id)
        {
            return this.data.Get(id);
        }
        public int Insert(Setting setting)
        {
            return this.data.Insert(setting);
        }
        public int Update(Setting setting)
        {
            return this.data.Update(setting);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
