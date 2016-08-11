using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Threading;

namespace DataAccess
{
    public static class DependencyContainer
    {

        // ---- Fields

        private static readonly Lazy<IUnityContainer> _defaultField =
            new Lazy<IUnityContainer>(CreateDefault, LazyThreadSafetyMode.ExecutionAndPublication);

        // ---- Properties

        public static IUnityContainer Default
        {
            get { return _defaultField.Value; }
        }

        // ---- Operations

        public static IUnityContainer CreateDefault()
        {
            var defaultInstance = new UnityContainer();

            UnityConfigurationSection section
                = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(defaultInstance);

            return defaultInstance;
        }

        public static T Resolve<T>()
        {
            var result = (T)Default.Resolve(typeof(T));
            return result;
        }

    }
}
