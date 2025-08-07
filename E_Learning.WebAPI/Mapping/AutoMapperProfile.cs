using System.Reflection;
using AutoMapper;
using E_Learning.Extensions.Mappings;

namespace E_Learning.WebAPI.Mapping
{
    /// <summary>
    /// AutoMapper profile that scans all loaded assemblies for types implementing <see cref="IMapFrom{T}"/>
    /// and automatically creates mapping configurations for them.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instanse of the <see cref="AutoMapperProfile"/> class
        /// and applies mappings from all loaded assemblies in the current application domain. 
        /// </summary>
        public AutoMapperProfile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic && a.FullName.StartsWith("E_Learning"))
                    .ToArray();

            foreach (var assembly in assemblies)
            {
                ApplyMappingsFromAssembly(assembly);
            }
        }

        /// <summary>
        /// Applies mappings from the given assembly by looking for types implementing <see cref="IMapFrom{T}"/>
        /// Automatically create bi-directional mappings with null-value skipping.
        /// </summary>
        /// <param name="assembly">The assembly to scan for mapping configurations.</param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            // Get all exported types that implement IMapFrom<T> and create mapping configurations for them.
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            // For each type, create mapping.
            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>));

                var sourceType = interfaceType.GetGenericArguments()[0];

                // Create a bi-directional map between source and destination.
                var mapping = CreateMap(sourceType, type).ReverseMap();

                // Skip mapping if the source member is null.
                mapping.ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null)
                );
            }
        }
    }
}
