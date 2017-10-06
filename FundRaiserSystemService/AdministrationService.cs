using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class AdministrationService : IAdministrationService
    {
        private IAdministrationDataAccess data;
        public AdministrationService(IAdministrationDataAccess data)
        {
            this.data = data;
        }

        public IEnumerable<Administration> GetAll()
        {
            return this.data.GetAll();
        }
        public Administration Get(int id)
        {
            return this.data.Get(id);
        }
        public int Insert(Administration admin)
        {
            return this.data.Insert(admin);
        }
        public int Update(Administration admin)
        {
            return this.data.Update(admin);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
