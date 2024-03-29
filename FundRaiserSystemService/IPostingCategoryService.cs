﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    public interface IPostingCategoryService
    {
        List<PostingCategory> GetAll();
        PostingCategory Get(int id);
        int Insert(PostingCategory postingCategory);
        int Update(PostingCategory postingCategory);
        int Delete(int id);
    }
}
