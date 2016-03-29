using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using StructureMap;
using StructureMap.Pipeline;

namespace EcommApi.Configuration
{
    public class DatabaseRegistry : Registry
    {
        public DatabaseRegistry()
        {
            For<IDocumentStore>().Singleton().Use(x => Create(x));            
        }
        

        private IDocumentStore Create(IContext container)
        {
            
            var docStore = new DocumentStore
            {
                Url = @"http://localhost:8080",
                DefaultDatabase = "DistributedSystemDemo"
            };

            //docStore.Configuration.Storage.Voron.AllowOn32Bits = true;

            docStore.Initialize();

            return docStore;
        }
    }
}