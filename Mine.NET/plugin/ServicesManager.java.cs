using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mine.NET.plugin
{
    /**
     * Manages services and service providers. Services are an interface
     * specifying a list of methods that a provider must implement. Providers are
     * implementations of these services. A provider can be queried from the
     * services manager in order to use a service (if one is available). If
     * multiple plugins register a service, then the service with the highest
     * priority takes precedence.
     */
    public interface ServicesManager
    {
        /**
         * Register a provider of a service.
         *
         * @param <T> Provider
         * @param service service class
         * @param provider provider to register
         * @param plugin plugin with the provider
         * @param priority priority of the provider
         */
        void register<T>(T provider, Plugin plugin, ServicePriority priority); //TODO: Service class?

        /**
         * Unregister all the providers registered by a particular plugin.
         *
         * @param plugin The plugin
         */
        void unregisterAll(Plugin plugin);

        /**
         * Unregister a particular provider for a particular service.
         *
         * @param service The service interface
         * @param provider The service provider implementation
         */
        void unregister(Type service, Object provider);

        /**
         * Unregister a particular provider.
         *
         * @param provider The service provider implementation
         */
        void unregister(Object provider);

        /**
         * Queries for a provider. This may return if no provider has been
         * registered for a service. The highest priority provider is returned.
         *
         * @param <T> The service interface
         * @param service The service interface
         * @return provider or null
         */
        T load<T>();

        /**
         * Queries for a provider registration. This may return if no provider
         * has been registered for a service.
         *
         * @param <T> The service interface
         * @param service The service interface
         * @return provider registration or null
         */
        RegisteredServiceProvider<T> getRegistration<T>();

        /**
         * Get registrations of providers for a plugin.
         *
         * @param plugin The plugin
         * @return provider registration or null
         */
        List<RegisteredServiceProvider> getRegistrations(Plugin plugin);

        /**
         * Get registrations of providers for a service. The returned list is
         * unmodifiable.
         *
         * @param <T> The service interface
         * @param service The service interface
         * @return list of registrations
         */
        List<RegisteredServiceProvider<T>> getRegistrations<T>();

        /**
         * Get a list of known services. A service is known if it has registered
         * providers for it.
         *
         * @return list of known services
         */
        List<Type> getKnownServices();

        /**
         * Returns whether a provider has been registered for a service. Do not
         * check this first only to call <code>load(service)</code> later, as that
         * would be a non-thread safe situation.
         *
         * @param <T> service
         * @param service service to check
         * @return whether there has been a registered provider
         */
        bool isProvidedFor<T>();
    }
}
