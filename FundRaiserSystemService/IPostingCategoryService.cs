using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IPostingCategoryService
    {
        IEnumerable<PostingCategory> GetAll();
        PostingCategory Get(int id);
        int Insert(PostingCategory postingCategory);
        int Update(PostingCategory postingCategory);
        int Delete(int id);
    }
}
