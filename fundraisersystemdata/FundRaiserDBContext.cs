using FundRaiserSystemEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiserSystemData
{
    class FundRaiserDBContext: DbContext
    {
        public virtual DbSet<Administration> Administrations { get; set; }
        public virtual DbSet<BankInformation> BankInformations { get; set; }
        public virtual DbSet<DonationOnPost> DonationOnPosts { get; set; }
        public virtual DbSet<FundRequestPost> FundRequestPosts { get; set; }
        public virtual DbSet<FundWithdraw> FundWithdraws { get; set; }
        public virtual DbSet<OnlineTransection> OnlineTransections { get; set; }
        public virtual DbSet<PostingCategory> PostingCategories { get; set; }
        public virtual DbSet<PostProof> PostProofs { get; set; }
        public virtual DbSet<RefundingInformation> RefundingInformations { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<TransectionMethodName> TransectionMethodNames { get; set; }
        public virtual DbSet<UserBankAccount> UserBankAccounts { get; set; }
        public virtual DbSet<UserComment> UserComments { get; set; }
        public virtual DbSet<UserInformation> UserInformations { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
    }
}
