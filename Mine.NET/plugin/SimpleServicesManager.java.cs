using Mine.NET.Event.server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mine.NET.plugin
{
    /**
     * A simple services manager.
     */
    public class SimpleServicesManager : ServicesManager
    {

        /**
         * Map of providers.
         */
        private readonly Dictionary<Type, List<RegisteredServiceProvider>> providers = new Dictionary<Type, List<RegisteredServiceProvider>>();

        /**
         * Register a provider of a service.
         *
         * @param <T> Provider
         * @param service service class
         * @param provider provider to register
         * @param plugin plugin with the provider
         * @param priority priority of the provider
         */
        public void register<T>(T provider, Plugin plugin, ServicePriority priority)
        {
            Type service = typeof(T);
            RegisteredServiceProvider<T> registeredProvider = null;
            lock (providers)
            {
                List<RegisteredServiceProvider> registered = providers[service];
                if (registered == null)
                {
                    registered = new List<RegisteredServiceProvider>();
                    providers.Add(service, registered);
                }

                registeredProvider = new RegisteredServiceProvider<T>(provider, priority, plugin);

                // Insert the provider into the collection, much more efficient big O than sort
                int position = registered.BinarySearch(registeredProvider);
                if (position < 0)
                {
                    registered.Insert(-(position + 1), registeredProvider);
                }
                else
                {
                    registered.Insert(position, registeredProvider);
                }

            }
            Bukkit.getServer().CallEvent(new ServiceRegisterEventArgs(registeredProvider));
        }

        /**
         * Unregister all the providers registered by a particular plugin.
         *
         * @param plugin The plugin
         */
        public void unregisterAll(Plugin plugin)
        {
            List<ServiceUnregisterEventArgs> unregisteredEvents = new List<ServiceUnregisterEventArgs>();
            lock (providers)
            {
                providers.Where(kv =>
                {
                    KeyValuePair<Type, List<RegisteredServiceProvider>> entry = kv;
                    // Removed entries that are from this plugin
                    entry.Value.Where(kv2 =>
                    {
                        RegisteredServiceProvider registered = kv2;

                        if (registered.getPlugin().Equals(plugin))
                        {
                            unregisteredEvents.Add(new ServiceUnregisterEventArgs(registered));
                            return false;
                        }
                        return true;
                    });

                    // Get rid of the empty list
                    if (entry.Value.Count == 0)
                    {
                        return false;
                    }
                    return true;
                });
            }
            foreach (ServiceUnregisterEventArgs event_ in unregisteredEvents)
            {
                Bukkit.getServer().CallEvent(event_);
            }
        }

        /**
         * Unregister a particular provider for a particular service.
         *
         * @param service The service interface
         * @param provider The service provider implementation
         */
        public void unregister(Type service, Object provider)
        {
            List<ServiceUnregisterEventArgs> unregisteredEvents = new List<ServiceUnregisterEventArgs>();
            lock (providers)
            {
                providers.Where(kv => //TODO: Assign result of Where everywhere
                {
                    var entry = kv;

                    // We want a particular service
                    if (entry.Key != service)
                    {
                        //continue;
                        return true;
                    }
                    // Removed entries that are from this plugin
                    entry.Value.Where(kv2 =>
                    {
                        RegisteredServiceProvider registered = kv2;

                        if (registered.getProviderObject() == provider)
                        {
                            unregisteredEvents.Add(new ServiceUnregisterEventArgs(registered));
                            return false;
                        }
                        return true;
                    });

                    // Get rid of the empty list
                    if (entry.Value.Count == 0)
                    {
                        return false;
                    }
                    return true;
                });
                foreach (ServiceUnregisterEventArgs event_ in unregisteredEvents)
                {
                    Bukkit.getServer().CallEvent(event_);
                }
            }
        }

        /**
         * Unregister a particular provider.
         *
         * @param provider The service provider implementation
         */
        public void unregister(Object provider)
        {
            List<ServiceUnregisterEventArgs> unregisteredEvents = new List<ServiceUnregisterEventArgs>();
            lock (providers)
            {
                providers.Where(kv => //TODO: Assign result of Where everywhere
                {
                    var entry = kv;

                    // Removed entries that are from this plugin
                    entry.Value.Where(kv2 =>
                    {
                        RegisteredServiceProvider registered = kv2;

                        if (registered.getProviderObject() == provider)
                        {
                            unregisteredEvents.Add(new ServiceUnregisterEventArgs(registered));
                            return false;
                        }
                        return true;
                    });

                    // Get rid of the empty list
                    if (entry.Value.Count == 0)
                    {
                        return false;
                    }
                    return true;
                });
                foreach (ServiceUnregisterEventArgs event_ in unregisteredEvents)
                {
                    Bukkit.getServer().CallEvent(event_);
                }
            }
        }

        /**
         * Queries for a provider. This may return if no provider has been
         * registered for a service. The highest priority provider is returned.
         *
         * @param <T> The service interface
         * @param service The service interface
         * @return provider or null
         */
        public T load<T>()
        {
            lock (providers)
            { //TODO: Check for potential removed locks
                List<RegisteredServiceProvider> registered = providers[typeof(T)];

                if (registered == null)
                {
                    return default(T);
                }

                // This should not be null!
                return (T)registered[0].getProviderObject();
            }
        }

        /**
         * Queries for a provider registration. This may return if no provider
         * has been registered for a service.
         *
         * @param <T> The service interface
         * @param service The service interface
         * @return provider registration or null
         */
        public RegisteredServiceProvider<T> getRegistration<T>()
        {
            lock (providers)
            {
                List<RegisteredServiceProvider> registered = providers[typeof(T)];

                if (registered == null)
                {
                    return null;
                }

                // This should not be null!
                return (RegisteredServiceProvider<T>)registered[0];
            }
        }

        /**
         * Get registrations of providers for a plugin.
         *
         * @param plugin The plugin
         * @return provider registration or null
         */
        public List<RegisteredServiceProvider> getRegistrations(Plugin plugin)
        {
            List<RegisteredServiceProvider> ret = new List<RegisteredServiceProvider>();
            lock (providers)
            {
                foreach (List<RegisteredServiceProvider> registered in providers.Values)
                {
                    foreach (RegisteredServiceProvider provider in registered)
                    {
                        if (provider.getPlugin().Equals(plugin))
                        {
                            ret.Add(provider);
                        }
                    }
                }
            }
            return ret;
        }

        /**
         * Get registrations of providers for a service. The returned list is
         * an unmodifiable copy.
         *
         * @param <T> The service interface
         * @param service The service interface
         * @return a copy of the list of registrations
         */
        public List<RegisteredServiceProvider<T>> getRegistrations<T>()
        {
            List<RegisteredServiceProvider<T>> ret;
            lock (providers)
            {
                List<RegisteredServiceProvider> registered = providers[typeof(T)];

                if (registered == null)
                {
                    return new List<RegisteredServiceProvider<T>>();
                }

                ret = new List<RegisteredServiceProvider<T>>();

                foreach (RegisteredServiceProvider provider in registered)
                {
                    ret.Add((RegisteredServiceProvider<T>)provider);
                }

            }
            return ret;
        }

        /**
         * Get a list of known services. A service is known if it has registered
         * providers for it.
         *
         * @return a copy of the set of known services
         */
        public HashSet<Type> getKnownServices()
        {
            lock (providers)
            {
                return new HashSet<Type>(providers.Keys);
            }
        }

        /**
         * Returns whether a provider has been registered for a service.
         *
         * @param <T> service
         * @param service service to check
         * @return true if and only if there are registered providers
         */
        public bool isProvidedFor<T>()
        {
            lock (providers)
            {
                return providers.ContainsKey(typeof(T));
            }
        }
    }
}
