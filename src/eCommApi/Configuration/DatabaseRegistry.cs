using Raven.Client;
using Raven.Client.Embedded;
using StructureMap;
using StructureMap.Pipeline;

namespace eCommApi.Configuration
{
    public class DatabaseRegistry : Registry
    {
        public DatabaseRegistry()
        {
            For<IDocumentStore>().Use(x => Create(x)).SetLifecycleTo(Lifecycles.ThreadLocal);
            
        }
        

        private IDocumentStore Create(IContext container)
        {
            
            var docStore = new EmbeddableDocumentStore
            {
                RunInMemory = true                
            };

            docStore.Configuration.Storage.Voron.AllowOn32Bits = true;

            docStore.Initialize();

            return docStore;
        }
    }
}