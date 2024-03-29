﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    class UserLoginService: IUserLoginService
    {
        private IUserLoginDataAccess data;
        public UserLoginService(IUserLoginDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<UserLogin> GetAll()
        {
            return this.data.GetAll();
        }
        public UserLogin Get(int id)
        {
            return this.data.Get(id);
        }
        public int Insert(UserLogin userLogin)
        {
            return this.data.Insert(userLogin);
        }
        public int Update(UserLogin userLogin)
        {
            return this.data.Update(userLogin);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
