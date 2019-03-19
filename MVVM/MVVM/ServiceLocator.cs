using System;
using Autofac;
namespace HTToolkit.MVVM
{
    public static class ServiceLocator
    {
        static public ContainerBuilder Builder { get; set; }
        static public IContainer Container { get; set; }

        static ServiceLocator()
        {
            Builder = new ContainerBuilder();
        }
    }
}
