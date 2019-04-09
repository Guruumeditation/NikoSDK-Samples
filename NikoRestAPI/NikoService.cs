using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Net.ArcanaStudio.NikoSDK;
using Net.ArcanaStudio.NikoSDK.Interfaces;
using NikoRestAPI.Hubs;

namespace NikoRestAPI
{
    public interface INikoService
    {
        NikoClient Client { get; }
        Task Initialize();
        Task SendEventToClients(IEvent events);
    }

    public class NikoService : INikoService
    {
        private readonly IHubContext<NikoEventHub> _hubContext;

        public NikoService(IHubContext<NikoEventHub> context)
        {
            _hubContext = context;
        }

        public Task Initialize()
        {
            return NikoClient.AutoDetect().ContinueWith(t =>
            {
                Client = t.Result;
                Client.StartClient();
                Client.StartEvents();
#pragma warning disable 4014
                Client.OnValueChanged += (s, e) => SendEventToClients(e);
#pragma warning restore 4014
            });
        }

        public async Task SendEventToClients(IEvent events)
        {
            var tasks = new List<Task>();
            foreach (var @event in events.Data)
            {
                tasks.Add(_hubContext.Clients.All.SendAsync("ReceiveEvent"));

            }

            await Task.WhenAll(tasks);
        }

        public NikoClient Client { get; private set; }
    }
}
