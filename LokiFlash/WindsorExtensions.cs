using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;

namespace LokiFlash
{
    public static class WindsorExtensions
    {

        public static void BuildUp(this IWindsorContainer container, object instance)
        {
            instance.GetType().GetProperties()
                .Where(property => property.CanWrite && property.PropertyType.IsPublic)
                .Where(property => container.Kernel.HasComponent(property.PropertyType))
                .ForEach(property => property.SetValue(instance, container.Resolve(property.PropertyType), null));
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

    }
}