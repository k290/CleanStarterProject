using AutoMapper;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace MyMovieLibrary.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => 
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            var iMapFromName = typeof(IMapFrom<>).Name;
            var mappingName = nameof(IMapFrom<object>.Mapping);

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);


                var methodInfo = type.GetMethod(mappingName) 
                    ?? type.GetInterface(iMapFromName).GetMethod(mappingName);
                
                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}