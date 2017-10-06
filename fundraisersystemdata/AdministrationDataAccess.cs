using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class AdministrationDataAccess : IAdministrationDataAccess
    {
        private FundRaiserDBContext context;
        public AdministrationDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<Administration> GetAll()
        {
            return this.context.Administrations.ToList();
        }

        public Administration Get(int id)
        {
            return this.context.Administrations.SingleOrDefault(x => x.AdminId == id);
        }

        public int Insert(Administration admin)
        {
            this.context.Administrations.Add(admin);
            return this.context.SaveChanges();
        }

        public int Update(Administration admin)
        {
            Administration administration = this.context.Administrations.SingleOrDefault(x => x.AdminId == admin.AdminId);
            administration.Email = admin.Email;
            administration.Password = admin.Password;
            administration.FirstName = admin.FirstName;
            administration.LastName = admin.LastName;
            administration.Address = admin.Address;
            administration.Role = admin.Role;

            return this.context.SaveChanges();
        }

        public int Delete(int id)
        {
            Administration administration = this.context.Administrations.SingleOrDefault(x => x.AdminId == id);
            this.context.Administrations.Remove(administration);

            return this.context.SaveChanges();
        }
    }
}
