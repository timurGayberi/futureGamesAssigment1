using System;
using System.Collections.Generic;
using UnityEngine;

namespace generalScripts
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service) where T : class
        {
            services[typeof(T)] = service ?? throw new ArgumentNullException(nameof(service));
        }

        public static bool TryGetService<T>(out T service) where T : class
        {
            if (services.TryGetValue(typeof(T), out var obj))
            {
                service = obj as T;
                return true;
            }

            service = null;
            return false;
        }

        public static T GetService<T>() where T : class
        {
            if (services.TryGetValue(typeof(T), out var service)) return service as T;

            Debug.LogError($"[ServiceLocator] Service not found: {typeof(T).Name}");
            throw new Exception($"Service {typeof(T).Name} is not registered!");
        }

        public static bool IsServiceRegistered<T>() where T : class
        {
            return services.ContainsKey(typeof(T));
        }

        public static void UnregisterService<T>(T service) where T : class
        {
            services.Remove(typeof(T));
        }

        public static void Clear()
        {
            services.Clear();
        }
    }
}