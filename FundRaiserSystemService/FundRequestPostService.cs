using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class FundRequestPostService : IFundRequestPostService
    {
        private IFundRequestPostDataAccess data;
        public FundRequestPostService(IFundRequestPostDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<FundRequestPost> GetAll(bool includeUserInformations = false, bool includeCategories = false, bool includeRefunds = false)
        {
            return this.data.GetAll(includeUserInformations, includeCategories, includeRefunds);
        }
        public FundRequestPost Get(int id, bool includeUserInformations = false, bool includeCategories = false, bool includeRefunds = false)
        {
            return this.data.Get(id, includeUserInformations, includeCategories, includeRefunds);
        }
        public int Insert(FundRequestPost fundRequestPost)
        {
            return this.data.Insert(fundRequestPost);
        }
        public int Update(FundRequestPost fundRequestPost)
        {
            return this.data.Update(fundRequestPost);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }

        public IEnumerable<FundRequestPost> GetActivePost()
        {
            return this.data.GetActivePost();
        }
    }
}
