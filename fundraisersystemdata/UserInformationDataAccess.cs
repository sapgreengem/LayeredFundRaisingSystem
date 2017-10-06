using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class UserInformationDataAccess : IUserInformationDataAccess
    {
        private FundRaiserDBContext context;
        public UserInformationDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }
        public IEnumerable<UserInformation> GetAll(bool includeUserLogins = false)
        {
            if (includeUserLogins)
            {
                return this.context.UserInformations.Include("UserLogin").ToList();
            }
            else
            {
                return this.context.UserInformations.ToList();
            }
        }
        public UserInformation Get(int id, bool includeUserLogins = false)
        {
            if (includeUserLogins)
            {
                return this.context.UserInformations.Include("UserLogin").SingleOrDefault(x => x.UserInformationId == id);
            }
            else
            {
                return this.context.UserInformations.SingleOrDefault(x=> x.UserInformationId == id);
            }
        }
        public int Insert(UserInformation userInformation)
        {
            this.context.UserInformations.Add(userInformation);
            return this.context.SaveChanges();
        }
        public int Update(UserInformation userInformation)
        {
            UserInformation info = this.context.UserInformations.SingleOrDefault(x => x.UserInformationId == userInformation.UserInformationId);
            info.FirstName = userInformation.FirstName;
            info.LastName = userInformation.LastName;
            info.PresentAddress = userInformation.PresentAddress;
            info.PermanentAddress = userInformation.PermanentAddress;
            info.NationalId = userInformation.NationalId;
            info.ContactNo = userInformation.ContactNo;
            info.ProfilePicture = userInformation.ProfilePicture;
            info.Country = userInformation.Country;
            info.UserId = userInformation.UserId;
            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            UserInformation info = this.context.UserInformations.SingleOrDefault(x => x.UserInformationId == id);
            this.context.UserInformations.Remove(info);
            return this.context.SaveChanges();
        }
    }
}
