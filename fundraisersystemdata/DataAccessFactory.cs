using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiserSystemData
{
    public abstract class DataAccessFactory
    {
        public static IAdministrationDataAccess GetAdministrationDataAccess()
        {
            return new AdministrationDataAccess(new FundRaiserDBContext());
        }
        public static IBankInformationDataAccess GetBankInformationDataAccess()
        {
            return new BankInformationDataAccess(new FundRaiserDBContext());
        }
        public static IDonationOnPostDataAccess GetDonationOnPostDataAccess()
        {
            return new DonationOnPostDataAccess(new FundRaiserDBContext());
        }
        public static IFundRequestPostDataAccess GetFundRequestPostDataAccess()
        {
            return new FundRequestPostDataAccess(new FundRaiserDBContext());
        }
        public static IFundWithdrawDataAccess GetFundWithdrawDataAccess()
        {
            return new FundWithdrawDataAccess(new FundRaiserDBContext());
        }
        public static IOnlineTransectionDataAccess GetOnlineTransectionDataAccess()
        {
            return new OnlineTransectionDataAccess(new FundRaiserDBContext());
        }
        public static IPostingCategoryDataAccess GetPostingCategoryDataAccess()
        {
            return new PostingCategoryDataAccess(new FundRaiserDBContext());
        }
        public static IPostProofDataAccess GetPostProofDataAccess()
        {
            return new PostProofDataAccess(new FundRaiserDBContext());
        }
        public static IRefundingInformationDataAccess GetRefundingInformationDataAccess()
        {
            return new RefundingInformationDataAccess(new FundRaiserDBContext());
        }
        public static ISettingDataAccess GetSettingDataAccess()
        {
            return new SettingDataAccess(new FundRaiserDBContext());
        }
        public static ITransectionMethodNameDataAccess GetTransectionMethodNameDataAccess()
        {
            return new TransectionMethodNameDataAccess(new FundRaiserDBContext());
        }
        public static IUserBankAccountDataAccess GetUserBankAccountDataAccess()
        {
            return new UserBankAccountDataAccess(new FundRaiserDBContext());
        }
        public static IUserCommentDataAccess GetUserCommentDataAccess()
        {
            return new UserCommentDataAccess(new FundRaiserDBContext());
        }
        public static IUserInformationDataAccess GetUserInformationDataAccess()
        {
            return new UserInformationDataAccess(new FundRaiserDBContext());
        }
        public static IUserLoginDataAccess GetUserLoginDataAccess()
        {
            return new UserLoginDataAccess(new FundRaiserDBContext());
        }
    }
}
