﻿namespace Masa.Contrib.Dispatcher.Events;

public class EventBusBuilder: IEventBusBuilder
{
    public IServiceCollection Services { get; }

    public EventBusBuilder(IServiceCollection services) => Services = services;

    public IEventBusBuilder UseMiddleware(Type middleware, ServiceLifetime middlewareLifetime = ServiceLifetime.Transient)
    {
        if (!typeof(IMiddleware<>).IsGenericInterfaceAssignableFrom(middleware))
            throw new ArgumentException($"{middleware.Name} doesn't implement IMiddleware<>");

        var descriptor = new ServiceDescriptor(typeof(IMiddleware<>), middleware, middlewareLifetime);
        Services.TryAddEnumerable(descriptor);
        return this;
    }
}
