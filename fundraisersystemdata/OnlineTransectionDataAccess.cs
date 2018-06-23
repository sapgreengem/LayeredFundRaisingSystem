using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class OnlineTransectionDataAccess : IOnlineTransectionDataAccess
    {
        private FundRaiserDBContext context;
        public OnlineTransectionDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<OnlineTransection> GetAll(bool includeTransectionMethodNames = false, bool includeDonationOnPosts = false)
        {
            if ( includeTransectionMethodNames || includeDonationOnPosts )
            {
                return this.context.OnlineTransections.Include("TransectionMethodName").Include("DonationOnPost").ToList();
            }
            else
            {
                return this.context.OnlineTransections.ToList();
            }

        }
        public OnlineTransection Get(int id, bool includeTransectionMethodNames = false, bool includeDonationOnPosts = false)
        {
            if (includeTransectionMethodNames || includeDonationOnPosts)
            {
                return this.context.OnlineTransections.Include("TransectionMethodName").Include("DonationOnPost").SingleOrDefault(x => x.TransectionId == id);
            }
            else
            {
                return this.context.OnlineTransections.SingleOrDefault(x => x.TransectionId == id);
            }
        }
        public int Insert(OnlineTransection onlineTransection)
        {
            this.context.OnlineTransections.Add(onlineTransection);
            return this.context.SaveChanges();

        }
        public int Update(OnlineTransection onlineTransection)
        {
            OnlineTransection transection = this.context.OnlineTransections.SingleOrDefault(x => x.TransectionId == onlineTransection.TransectionId);
            transection.GetwayId = onlineTransection.GetwayId;
            transection.MethodId = onlineTransection.MethodId;
            transection.DonationId = onlineTransection.DonationId;
            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            OnlineTransection transection = this.context.OnlineTransections.SingleOrDefault(x => x.TransectionId == id);
            this.context.OnlineTransections.Remove(transection);

            return this.context.SaveChanges();
        }
    }
}
