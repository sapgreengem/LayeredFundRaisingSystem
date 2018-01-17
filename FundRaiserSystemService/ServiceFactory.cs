using FundRaiserSystemData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiserSystemService
{
    public abstract class ServiceFactory
    {
        public static IAdministrationService GetAdministrationService()
        {
            return new AdministrationService(DataAccessFactory.GetAdministrationDataAccess());
        }
        public static IBankInformationService GetBankInformationService()
        {
            return new BankInformationService(DataAccessFactory.GetBankInformationDataAccess());
        }
        public static IDonationOnPostService GetDonationOnPostService()
        {
            return new DonationOnPostService(DataAccessFactory.GetDonationOnPostDataAccess());
        }
        public static IFundRequestPostService GetFundRequestPostService()
        {
            return new FundRequestPostService(DataAccessFactory.GetFundRequestPostDataAccess());
        }
        public static IFundWithdrawService GetFundWithdrawService()
        {
            return new FundWithdrawService(DataAccessFactory.GetFundWithdrawDataAccess());
        }
        public static IOnlineTransectionService GetOnlineTransectionService()
        {
            return new OnlineTransectionService(DataAccessFactory.GetOnlineTransectionDataAccess());
        }
        public static IPostingCategoryService GetPostingCategoryService()
        {
            return new PostingCategoryService(DataAccessFactory.GetPostingCategoryDataAccess());
        }
        public static IPostProofService GetPostProofService()
        {
            return new PostProofService(DataAccessFactory.GetPostProofDataAccess());
        }
        public static IRefundingInformationService GetRefundingInformationService()
        {
            return new RefundingInformationService(DataAccessFactory.GetRefundingInformationDataAccess());
        }
        public static ISettingService GetSettingService()
        {
            return new SettingService(DataAccessFactory.GetSettingDataAccess());
        }
        public static ITransectionMethodNameService GetTransectionMethodNameService()
        {
            return new TransectionMethodNameService(DataAccessFactory.GetTransectionMethodNameDataAccess());
        }
        public static IUserBankAccountService GetUserBankAccountService()
        {
            return new UserBankAccountService(DataAccessFactory.GetUserBankAccountDataAccess());
        }
        public static IUserCommentService GetUserCommentService()
        {
            return new UserCommentService(DataAccessFactory.GetUserCommentDataAccess());
        }
        public static IUserRatingService GetUserRatingService()
        {
            return new UserRatingService(DataAccessFactory.GetUserRatingDataAccess());
        }
        public static IUserInformationService GetUserInformationService()
        {
            return new UserInformationService(DataAccessFactory.GetUserInformationDataAccess());
        }
        public static IUserLoginService GetUserLoginService()
        {
            return new UserLoginService(DataAccessFactory.GetUserLoginDataAccess());
        }

    }
}
