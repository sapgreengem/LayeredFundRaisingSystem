//
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace layeredFundRaiserSystem.Models
{
    public class LoadAllCategory
    {
        public IEnumerable<PostingCategory> getCategory()
        {
            IPostingCategoryService catService = ServiceFactory.GetPostingCategoryService();
            IEnumerable<PostingCategory> loadCategories = catService.GetAll();
            return loadCategories;
        }

        public IEnumerable<FundRequestPost> GetPostsById(int Category)
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> post = service.GetAll().Where(b => b.CategoryId == Category).Where(a => a.PostStatus == "Active");
            return post;
        }
    }
}