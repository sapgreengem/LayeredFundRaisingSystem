using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using FundRaiserSystemData;
using FundRaiserSystemService;
using FundRaiserSystemEntity;

namespace layeredFundRaiserSystem
{
    [HubName("CustomHubNewName")]
    public class CustomHubName : Hub
    {
        [HubMethodName("CustomSendMethod")]
        public void Send(string id, int id1 )
        {
            //IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            //FundRequestPost itemToUpdate = service.Get(id1, true, true, false);
            //if (id == "Active")
            //{
            //    itemToUpdate.PostStatus = "Active"; 
            //}
            //else
            //{
            //    itemToUpdate.PostStatus = "Reject";
            //}
            //service.Update(itemToUpdate);

            string msg = id;

            if (id == "Active")
                msg = "Post Has Been Accepted";
            else
                msg = "Post Has Been Rejected";

            Clients.All.broadcastMessage(msg);
        }
    }
}