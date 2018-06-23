using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    class PostingCategoryService: IPostingCategoryService
    {
        private IPostingCategoryDataAccess data;
        public PostingCategoryService(IPostingCategoryDataAccess data)
        {
            this.data = data;
        }
        public List<PostingCategory> GetAll()
        {
            return this.data.GetAll();
        }
        public PostingCategory Get(int id)
        {
            return this.data.Get(id);
        }
        public int Insert(PostingCategory postingCategory)
        {
            return this.data.Insert(postingCategory);
        }
        public int Update(PostingCategory postingCategory)
        {
            return this.data.Update(postingCategory);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
