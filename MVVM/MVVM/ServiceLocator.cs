using System;
using Autofac;
namespace HTToolkit.MVVM
{
    public static class ServiceLocator
    {
        static ContainerBuilder _builder;
        static IContainer _contianer;

        static ServiceLocator()
        {
            _builder = new ContainerBuilder();
        }

        public static void Build()
        {
            _contianer = _builder.Build();
        }

        public static void Register<T>(bool singleton = false) where T : class
        {
            if (singleton)
                _builder.RegisterType<T>().SingleInstance();
            else
                _builder.RegisterType<T>();
        }

        public static void Register<TInterface,T>(bool singleton = false) where TInterface : class where T : class, TInterface
        {
            if (singleton)
                _builder.RegisterType<T>().As<TInterface>().SingleInstance();
            else
                _builder.RegisterType<T>().As<TInterface>();
        }

        public static T Resolve<T>() where T : class
        {
            return _contianer.Resolve<T>();
        }
    }
}
