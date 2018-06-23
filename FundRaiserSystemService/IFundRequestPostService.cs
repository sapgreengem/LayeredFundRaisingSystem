using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    public interface IFundRequestPostService
    {
        IEnumerable<FundRequestPost> GetAll(bool includeUserInformations = false, bool includeCategories = false, bool includeRefunds = false);
        FundRequestPost Get(int id, bool includeUserInformations = false, bool includeCategories = false, bool includeRefunds = false);
        int Insert(FundRequestPost fundRequestPost);
        int Update(FundRequestPost fundRequestPost);
        int Delete(int id);
        IEnumerable<FundRequestPost> GetActivePost();
    }
}
