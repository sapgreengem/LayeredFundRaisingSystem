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
        public void Send(int id)
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            FundRequestPost post = service.Get(id, true, true, false);
            var percentage = Convert.ToInt32(((post.CollectedAmount / post.RequiredAmount) * 100));

            Clients.All.broadcastMessage(id);
        }
    }
}