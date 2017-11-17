using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class PostingCategoryDataAccess : IPostingCategoryDataAccess
    {
        private FundRaiserDBContext context;
        public PostingCategoryDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public List<PostingCategory> GetAll()
        {          
            return this.context.PostingCategories.ToList();
        }
        public PostingCategory Get(int id)
        {
            return this.context.PostingCategories.SingleOrDefault(x => x.CategoryId == id);
        }
        public int Insert(PostingCategory postingCategory)
        {
            this.context.PostingCategories.Add(postingCategory);
            return this.context.SaveChanges();

        }
        public int Update(PostingCategory postingCategory)
        {
            PostingCategory category = this.context.PostingCategories.SingleOrDefault(x => x.CategoryId == postingCategory.CategoryId);
            category.CategoryName = postingCategory.CategoryName;

            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            PostingCategory category = this.context.PostingCategories.SingleOrDefault(x => x.CategoryId == id);
            this.context.PostingCategories.Remove(category);

            return this.context.SaveChanges();
        }
    }
}
