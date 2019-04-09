using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Net.ArcanaStudio.NikoSDK;
using Net.ArcanaStudio.NikoSDK.Interfaces;

namespace NikoRestAPI.Hubs
{
    public class NikoEventHub : Hub
    {
        private readonly NikoClient _nikoClient;

        public NikoEventHub(INikoService service)
        {
            
        }

        public async Task SendMessage(IEvent events)
        {
            var tasks = new List<Task>();
            foreach (var @event in events.Data)
            {
                tasks.Add(Clients.All.SendAsync("ReceiveEvent"));
            
            }

            await Task.WhenAll(tasks);
        }
    }
}
