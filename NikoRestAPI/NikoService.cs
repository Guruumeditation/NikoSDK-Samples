using System.Threading.Tasks;
using Net.ArcanaStudio.NikoSDK;

namespace NikoRestAPI
{
    public interface INikoService
    {
        NikoClient Client { get; }
        Task Initialize();
    }

    public class NikoService : INikoService
    {
        public NikoService()
        {
        }

        public Task Initialize()
        {
            return NikoClient.AutoDetect().ContinueWith(t => { Client = t.Result; });
        }

        public NikoClient Client { get; private set; }
    }
}
