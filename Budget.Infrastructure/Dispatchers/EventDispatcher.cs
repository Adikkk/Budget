using Autofac;
using Budget.Domain.Shared;

namespace Budget.Infrastructure.Dispatchers
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext componentContext;

        public EventDispatcher(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public Task Dispatch<TEvent>(TEvent e) where TEvent : IEvent
        {
            if (e == null)
            {
                throw new System.ArgumentNullException(nameof(e));
            }

            var eventType = typeof(IEventHandler<>).MakeGenericType(e.GetType());

            dynamic handler = componentContext.Resolve(eventType);

            return (Task)eventType
                .GetMethod("Handle")
                .Invoke(handler, new object[] { e });
        }
    }
}
