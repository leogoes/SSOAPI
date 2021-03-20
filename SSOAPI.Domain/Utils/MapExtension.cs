using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SSOAPI.API.Extensions
{
    public static class MapExtension
    {
        public static TS Map<T, TS>(this T source, TS destination)
            where T : class, new()
            where TS : class, new()
        {
            source ??= new T();
            destination ??= new TS();

            var sourceProps = source.GetType().GetProperties().ToList();
            var destinationProps = destination.GetType().GetProperties().ToList();
            sourceProps.ForEach(src =>
            {
                var destinationProp = destinationProps.Find(dest => dest.Name == src.Name);

                if (destinationProp is null) return;
                Enum.TryParse(typeof(TypeCode), destinationProp.PropertyType.Name, out var paramType);

                if (!(paramType is null))
                    destinationProp?.SetValue(destination, src.GetValue(source, null), null);
            });

            return destination;
        }
    }
}
