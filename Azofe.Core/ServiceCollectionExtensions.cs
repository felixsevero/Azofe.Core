using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Azofe.Core;

public sealed class AzofeOptions {

	readonly MediatRServiceConfiguration configuration;

	public AzofeOptions(MediatRServiceConfiguration configuration) => this.configuration = configuration;

	public void AddPipelineBehavior(Type type) => configuration.AddOpenBehavior(type, ServiceLifetime.Scoped);

	public void AddServicesFromAssemblies(params Assembly[] assemblies) => configuration.RegisterServicesFromAssemblies(assemblies);

	public void AddServicesFromAssembly(Assembly assembly) => configuration.RegisterServicesFromAssembly(assembly);

}

public static class ServiceCollectionExtensions {

	public static IServiceCollection AddAzofe(this IServiceCollection services, Action<AzofeOptions> options) => services.AddMediatR(configuration => options(new(configuration)));

}
