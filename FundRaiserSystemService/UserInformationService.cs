using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class UserInformationService: IUserInformationService
    {
        private IUserInformationDataAccess data;
        public UserInformationService(IUserInformationDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<UserInformation> GetAll(bool includeUserLogins = false)
        {
            return this.data.GetAll(includeUserLogins);
        }
        public UserInformation Get(int id, bool includeUserLogins = false)
        {
            return this.data.Get(id, includeUserLogins);
        }
        public int Insert(UserInformation userInformation)
        {
            return this.data.Insert(userInformation);
        }
        public int Update(UserInformation userInformation)
        {
            return this.data.Update(userInformation);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
