﻿using System;
using System.Reflection;

using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Autofac.Extras.IocManager
{
    internal class ServiceRegistration : IServiceRegistration
    {
        /// <summary>
        ///     The container builder
        /// </summary>
        private readonly ContainerBuilder _containerBuilder;

        /// <summary>
        ///     The decorator service
        /// </summary>
        private readonly DecoratorService _decoratorService = new DecoratorService();

        /// <summary>
        ///     The root resolver
        /// </summary>
        private IRootResolver _rootResolver;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServiceRegistration" /> class.
        /// </summary>
        public ServiceRegistration() : this(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ServiceRegistration" /> class.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        public ServiceRegistration(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder ?? new ContainerBuilder();

            _containerBuilder.RegisterType<Resolver>().As<IResolver>();
            _containerBuilder.RegisterType<ScopeResolver>().As<IScopeResolver>();
            _containerBuilder.Register<IDecoratorService>(_ => _decoratorService).SingleInstance();
        }

        /// <summary>
        ///     Occurs when [registration completed].
        /// </summary>
        public event EventHandler<RegistrationCompletedEventArgs> RegistrationCompleted;

        /// <summary>
        ///     Callback action, when IocManager is isposing.
        /// </summary>
        public event EventHandler<OnDisposingEventArgs> OnDisposing;

        /// <summary>
        ///     Registers the specified lifetime.
        /// </summary>
        /// <typeparam name="TService1">The type of the service1.</typeparam>
        /// <typeparam name="TService2">The type of the service2.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void Register<TService1, TService2, TImplementation>(Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
            where TService1 : class
            where TService2 : class
            where TImplementation : class, TService1, TService2
        {
            IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterType<TImplementation>()
                .As<TService1, TService2>()
                .WithPropertyInjection()
                .AsSelf();

            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                registration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers the specified instance.
        /// </summary>
        /// <typeparam name="TService1">The type of the service1.</typeparam>
        /// <typeparam name="TService2">The type of the service2.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void Register<TService1, TService2, TImplementation>(
            TImplementation instance,
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
            where TService1 : class
            where TService2 : class
            where TImplementation : class, TService1, TService2
        {
            IRegistrationBuilder<TImplementation, SimpleActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterInstance(instance)
                .As<TService1, TService2>()
                .AsSelf()
                .WithPropertyInjection();

            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                registration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers the specified lifetime.
        /// </summary>
        /// <typeparam name="TService1">The type of the service1.</typeparam>
        /// <typeparam name="TService2">The type of the service2.</typeparam>
        /// <typeparam name="TService3"></typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void Register<TService1, TService2, TService3, TImplementation>(
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
            where TService1 : class
            where TService2 : class
            where TService3 : class
            where TImplementation : class, TService1, TService2, TService3
        {
            IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterType<TImplementation>()
                .As<TService1, TService2, TService3>()
                .WithPropertyInjection()
                .AsSelf();

            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                registration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers the specified instance.
        /// </summary>
        /// <typeparam name="TService1">The type of the service1.</typeparam>
        /// <typeparam name="TService2">The type of the service2.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void Register<TService1, TService2>(
            object instance,
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
            where TService1 : class
            where TService2 : class
        {
            IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterInstance(instance)
                .As<TService1, TService2>()
                .AsSelf()
                .WithPropertyInjection();

            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                registration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers the specified instance.
        /// </summary>
        /// <typeparam name="TService1">The type of the service1.</typeparam>
        /// <typeparam name="TService2">The type of the service2.</typeparam>
        /// <typeparam name="TService3">The type of the service3.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void Register<TService1, TService2, TService3, TImplementation>(
            TImplementation instance,
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
            where TService1 : class
            where TService2 : class
            where TService3 : class
            where TImplementation : class, TService1, TService2, TService3
        {
            IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterInstance(instance)
                .As<TService1, TService2, TService3>()
                .AsSelf()
                .WithPropertyInjection();

            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                registration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers the specified instance.
        /// </summary>
        /// <typeparam name="TService1">The type of the service1.</typeparam>
        /// <typeparam name="TService2">The type of the service2.</typeparam>
        /// <typeparam name="TService3"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void Register<TService1, TService2, TService3>(
            object instance,
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
            where TService1 : class
            where TService2 : class
            where TService3 : class
        {
            IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterInstance(instance)
                .As<TService1, TService2, TService3>()
                .AsSelf()
                .WithPropertyInjection();

            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                registration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers the specified lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void Register<TService, TImplementation>(
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
            where TImplementation : class, TService
            where TService : class
        {
            IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterType<TImplementation>()
                .WithPropertyInjection()
                .AsSelf();

            IRegistrationBuilder<TService, SimpleActivatorData, SingleRegistrationStyle> serviceRegistration = _containerBuilder
                .Register<TService>(c => c.Resolve<TImplementation>())
                .As<TService>()
                .WithPropertyInjection()
                .OnActivating(args =>
                {
                    TService instance = _decoratorService.Decorate(args.Instance, new ResolverContext(new Resolver(args.Context)));
                    args.ReplaceInstance(instance);
                });

            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                serviceRegistration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers if absent.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="lifetime">The lifetime.</param>
        public void RegisterIfAbsent<TService, TImplementation>(
            Lifetime lifetime = Lifetime.Transient)
            where TService : class
            where TImplementation : class, TService
        {
            RegisterIfAbsent(typeof(TService), typeof(TImplementation), lifetime);
        }

        /// <summary>
        ///     Registers if absent.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <param name="lifetime">The lifetime.</param>
        public void RegisterIfAbsent(Type serviceType, Type implementationType, Lifetime lifetime = Lifetime.Transient)
        {
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterType(implementationType)
                .WithPropertyInjection()
                .AsSelf()
                .IfNotRegistered(serviceType);

            _containerBuilder.Register(c => c.Resolve(implementationType))
                             .As(serviceType)
                             .WithPropertyInjection()
                             .OnActivating(args =>
                             {
                                 object instance = _decoratorService.Decorate(args.Instance, new ResolverContext(new Resolver(args.Context)));
                                 args.ReplaceInstance(instance);
                             })
                             .IfNotRegistered(serviceType);

            registration.ApplyLifeStyle(lifetime);
        }

        public void RegisterIfAbsent<TService>(Lifetime lifetime = Lifetime.Transient) where TService : class
        {
            RegisterIfAbsent(typeof(TService), lifetime);
        }

        public void RegisterIfAbsent(Type type, Lifetime lifetime = Lifetime.Transient)
        {
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterType(type)
                .WithPropertyInjection()
                .AsSelf()
                .OnActivating(args =>
                {
                    object instance = _decoratorService.Decorate(type, args.Instance, new ResolverContext(new Resolver(args.Context)));
                    args.ReplaceInstance(instance);
                })
                .IfNotRegistered(type);

            registration.ApplyLifeStyle(lifetime);
        }

        /// <summary>
        ///     Registers the specified factory.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="factory">The factory.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void Register<TService>(
            Func<IResolverContext, TService> factory,
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
            where TService : class
        {
            IRegistrationBuilder<TService, SimpleActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .Register(cc => factory(new ResolverContext(new Resolver(cc))))
                .WithPropertyInjection()
                .OnActivating(args =>
                {
                    TService instance = _decoratorService.Decorate(args.Instance, new ResolverContext(new Resolver(args.Context)));
                    args.ReplaceInstance(instance);
                });
            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                registration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void Register(
            Type serviceType,
            Type implementationType,
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
        {
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterType(implementationType)
                .As(serviceType)
                .WithPropertyInjection()
                .AsSelf()
                .OnActivating(args =>
                {
                    object instance = _decoratorService.Decorate(serviceType, args.Instance, new ResolverContext(new Resolver(args.Context)));
                    args.ReplaceInstance(instance);
                });
            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                registration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers the type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void RegisterType(
            Type serviceType,
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
        {
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration = _containerBuilder
                .RegisterType(serviceType)
                .WithPropertyInjection()
                .AsSelf()
                .OnActivating(args =>
                {
                    object instance = _decoratorService.Decorate(args.Instance, new ResolverContext(new Resolver(args.Context)));
                    args.ReplaceInstance(instance);
                });

            registration.ApplyLifeStyle(lifetime);

            if (keepDefault)
            {
                registration.PreserveExistingDefaults();
            }
        }

        /// <summary>
        ///     Registers the type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void RegisterType<TService>(Lifetime lifetime = Lifetime.Transient, bool keepDefault = false)
        {
            RegisterType(typeof(TService), lifetime, keepDefault);
        }

        /// <summary>
        ///     Registers the generic.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void RegisterGeneric(
            Type serviceType,
            Type implementationType,
            Lifetime lifetime = Lifetime.Transient,
            bool keepDefault = false)
        {
            IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> registration = _containerBuilder
                .RegisterGeneric(implementationType)
                .As(serviceType)
                .AsSelf()
                .WithPropertyInjection();

            registration.ApplyLifeStyle(lifetime);
        }

        /// <summary>
        ///     Registers the generic.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keepDefault">if set to <c>true</c> [keep default].</param>
        public void RegisterGeneric<TService, TImplementation>(Lifetime lifetime = Lifetime.Transient, bool keepDefault = false)
        {
            RegisterGeneric(typeof(TService), typeof(TImplementation), lifetime, keepDefault);
        }

        /// <summary>
        ///     Decorates the specified factory.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="factory">The factory.</param>
        public void Decorate<TService>(Func<IResolverContext, TService, TService> factory)
        {
            _decoratorService.AddDecorator(factory);
        }

        /// <summary>
        ///     Registers the assembly by convention.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            _containerBuilder.RegisterAssemblyByConvention(assembly);
        }

        /// <summary>
        ///     Registers the assembly as closed types of.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="closedServiceType">Type of the closed service.</param>
        /// <param name="lifetime">The lifetime.</param>
        public void RegisterAssemblyAsClosedTypesOf(
            Assembly assembly,
            Type closedServiceType,
            Lifetime lifetime = Lifetime.Transient)
        {
            IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registration = _containerBuilder
                .RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(closedServiceType)
                .AsImplementedInterfaces()
                .WithPropertyInjection();

            registration.ApplyLifeStyle(lifetime);
        }

        /// <summary>
        ///     Creates the resolver.
        /// </summary>
        /// <param name="ignoreStartableComponents"></param>
        /// <returns></returns>
        public IRootResolver CreateResolver(bool ignoreStartableComponents = false)
        {
            IContainer container = _containerBuilder.Build(ignoreStartableComponents ? ContainerBuildOptions.IgnoreStartableComponents : ContainerBuildOptions.None);
            _rootResolver = new RootResolver(container);

            EventHandler<RegistrationCompletedEventArgs> handler = RegistrationCompleted;
            handler?.Invoke(this, new RegistrationCompletedEventArgs(_rootResolver));

            _rootResolver.OnDisposing += (sender, args) => { OnServicesDisposing(args.Context.Resolver); };

            return _rootResolver;
        }

        /// <summary>
        ///     Uses the builder to register components.
        /// </summary>
        /// <param name="builderAction">The builder action.</param>
        public void UseBuilder(Action<ContainerBuilder> builderAction)
        {
            builderAction(_containerBuilder);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        private void OnServicesDisposing(IResolver resolver)
        {
            EventHandler<OnDisposingEventArgs> handler = OnDisposing;
            handler?.Invoke(this, new OnDisposingEventArgs(resolver));
        }
    }
}
