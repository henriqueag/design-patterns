using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Decorator;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection Decorate<TInterface, TDecorator>(this IServiceCollection services)
        where TInterface: class
        where TDecorator: class, TInterface
    {
        var wrappedDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(TInterface))
            ?? throw new InvalidOperationException($"{typeof(TInterface).Name} não está registrada");

        var objectFactory = ActivatorUtilities.CreateFactory(typeof(TDecorator), new[] { typeof(TInterface) });

        services.Replace(ServiceDescriptor.Describe(
            typeof(TInterface),
            s => (TInterface)objectFactory(s, new[] { s.CreateInstance(wrappedDescriptor) }),
            wrappedDescriptor.Lifetime));

        return services;
    }

    public static object CreateInstance(this IServiceProvider services, ServiceDescriptor descriptor) => descriptor switch
    {
        { ImplementationInstance: not null } => descriptor.ImplementationInstance,
        { ImplementationFactory: not null } => descriptor.ImplementationFactory(services),
        _ => ActivatorUtilities.GetServiceOrCreateInstance(services, descriptor.ImplementationType!)
    };
}