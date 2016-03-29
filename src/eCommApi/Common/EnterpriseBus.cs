using MassTransit;

namespace EcommApi.Common
{
    public interface IEnterpriseBus
    {
        void Publish<T>(T message) where T : class;
    }

    public class EnterpriseBus : IEnterpriseBus
    {
        private readonly IBusControl _busControl;

        public EnterpriseBus(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public void Publish<T>(T message) where T : class
        {
            _busControl.Publish<T>(message);
        }
    }
}