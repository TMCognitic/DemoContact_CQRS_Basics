using DemoContact.Tools.CQRS.Commands;
using DemoContact.Tools.CQRS.Queries;
using System.Reflection;

namespace DemoContact.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            List<Type> handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Union(Assembly.GetExecutingAssembly().GetReferencedAssemblies().SelectMany(an => Assembly.Load(an).GetTypes()))
                .Where(x => x.GetInterfaces().Any(y => IsHandlerInterface(y)))
                .Where(x => x.Name.EndsWith("Handler"))
                .ToList();

            foreach (Type type in handlerTypes)
            {
                Type interfaceType = type.GetInterfaces().Single(y => IsHandlerInterface(y));
                services.AddTransient(interfaceType, type);
            }
        }

        private static bool IsHandlerInterface(Type type)
        {
            if (!type.IsGenericType)
                return false;

            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(ICommandHandler<>) || typeDefinition == typeof(IQueryHandler<,>);
        }
    }
}
