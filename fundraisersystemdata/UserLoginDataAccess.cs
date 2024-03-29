﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class UserLoginDataAccess:IUserLoginDataAccess
    {
        private FundRaiserDBContext context;
        public UserLoginDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }
        public IEnumerable<UserLogin> GetAll()
        {
            return this.context.UserLogins.ToList();
        }
        public UserLogin Get(int id)
        {
            return this.context.UserLogins.SingleOrDefault(x => x.UserId == id);
        }
        public int Insert(UserLogin userLogin)
        {
            this.context.UserLogins.Add(userLogin);
            return this.context.SaveChanges();
        }
        public int Update(UserLogin userLogin)
        {
            UserLogin login = this.context.UserLogins.SingleOrDefault(x => x.UserId == userLogin.UserId);
            login.Email = userLogin.Email;
            login.Password = userLogin.Password;
            login.Status = userLogin.Status;
            login.UserCreationDate = userLogin.UserCreationDate;
            login.AutoGeneratedLink = userLogin.AutoGeneratedLink;
            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            UserLogin login = this.context.UserLogins.SingleOrDefault(x => x.UserId == id);
            this.context.UserLogins.Remove(login);
            return this.context.SaveChanges();
        }
    }
}
