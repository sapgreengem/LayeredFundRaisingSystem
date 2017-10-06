using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class OnlineTransectionService: IOnlineTransectionService
    {
        private IOnlineTransectionDataAccess data;
        public OnlineTransectionService(IOnlineTransectionDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<OnlineTransection> GetAll(bool includeTransectionMethodNames = false, bool includeDonationOnPosts = false)
        {
            return this.data.GetAll(includeTransectionMethodNames,includeDonationOnPosts );

        }
        public OnlineTransection Get(int id, bool includeTransectionMethodNames = false, bool includeDonationOnPosts = false)
        {
            return this.data.Get(id, includeTransectionMethodNames, includeDonationOnPosts);

        }
        public int Insert(OnlineTransection onlineTransection)
        {
            return this.data.Insert(onlineTransection);

        }
        public int Update(OnlineTransection onlineTransection)
        {
            return this.data.Update(onlineTransection);

        }
        public int Delete(int id)
        {
            return this.data.Delete(id);

        }
    }
}
