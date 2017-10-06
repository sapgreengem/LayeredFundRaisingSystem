using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IOnlineTransectionDataAccess
    {
        IEnumerable<OnlineTransection> GetAll(bool includeTransectionMethodNames = false, bool includeDonationOnPosts = false);
        OnlineTransection Get(int id, bool includeTransectionMethodNames = false, bool includeDonationOnPosts = false);
        int Insert(OnlineTransection onlineTransection);
        int Update(OnlineTransection onlineTransection);
        int Delete(int id);
    }
}
