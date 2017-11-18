using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemService;
using FundRaiserSystemEntity;
using FundRaiserSystemData;
using layeredFundRaiserSystem.Models;

namespace layeredFundRaiserSystem.Controllers
{
    public class DonateOnPostController : Controller
    {
        // GET: DonateOnPost/Index/4
        public ActionResult Index(int id)
        {
            if (Session["Login"] == null && id != 0)
            {
                Session["RedirectToDonateOnPost"] = "/DonateOnPost/Index/"+id;
                Response.Redirect("/Login");
            }
            if (Session["UserInformationId"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }
            else
            {
                Response.Redirect("/Login");
            }

            ITransectionMethodNameService service = ServiceFactory.GetTransectionMethodNameService();
            ViewBag.TransectionMethodName = service.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Index(int id, FormCollection coll)
        {
            ITransectionMethodNameService service = ServiceFactory.GetTransectionMethodNameService();
            ViewBag.TransectionMethodName = service.GetAll();

            if (coll["method"] != null && !String.IsNullOrWhiteSpace(coll["Ammount"]))
            {
                IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
                FundRequestPost item = postService.Get(id);
                if (item.UserInformationId != Convert.ToInt32(Session["UserInformationId"]))
                {
                    if (Convert.ToDouble(coll["Ammount"]) > 0)
                    {
                        IOnlineTransectionService transectionService = ServiceFactory.GetOnlineTransectionService();
                        OnlineTransection transection = new OnlineTransection();
                        transection.GetwayId = id + " " + DateTime.Now + " " + Convert.ToDouble(coll["Ammount"]);
                        transection.MethodId = Convert.ToInt32(coll["method"]);
                        transection.DonationId = 0;
                        transectionService.Insert(transection);

                        IDonationOnPostService donateService = ServiceFactory.GetDonationOnPostService();
                        DonationOnPost donation = new DonationOnPost();
                        donation.DonationAmount = Convert.ToDouble(coll["Ammount"]);
                        donation.DonationDate = DateTime.Now;
                        donation.PostId = id;
                        donation.UserInformationId = Convert.ToInt32(Session["UserInformationId"]);
                        donation.TransectionId = transection.TransectionId;
                        donateService.Insert(donation);

                        this.updateCollectedMoney(Convert.ToDouble(coll["Ammount"]), id);

                        return Redirect("/PostDetailsView/Index/" + id); 
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Give valid Amount";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "You Cannot Donate on your own post";
                }
                //return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Select payment method and Fill amount first";
            }
            return View();
        }

        public void updateCollectedMoney(double amount, int id)
        {
            
            IFundRequestPostService fundService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost postAmount = fundService.Get(id);

            postAmount.CollectedAmount += amount;
            postAmount.RemainingAmount += amount;
            fundService.Update(postAmount);

            if ((postAmount.RequiredAmount - postAmount.CollectedAmount) <= 0)
            {
                postAmount.PostStatus = "Completed";
                fundService.Update(postAmount);
            }
        }
    }
}