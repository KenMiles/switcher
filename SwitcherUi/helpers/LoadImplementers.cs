using SwitcherUi.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherUi.helpers
{
    class LoadImplementers 
    {
        private readonly static Type[] _constructorParamTypes = new[] { typeof(IConfiguration) };

        internal static Object Create(Type type, IConfiguration config)
        {
            var construtor = type.GetConstructor(_constructorParamTypes);
            if (construtor != null) return construtor.Invoke(new Object[] { config });
            construtor = type.GetConstructor(Type.EmptyTypes);
            if (construtor != null) return construtor.Invoke(new Object[0]);
            throw new Exception("Unable to create " + type.Name);
        }

        public static T[] Load<T>(IConfiguration config) {
            var type = typeof(T);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                .Select(t => Create(t, config))
                .Cast<T>()
                .ToArray();
        }
    }
}
