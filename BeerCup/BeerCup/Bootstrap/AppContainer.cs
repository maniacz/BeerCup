using Autofac;
using BeerCup.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Bootstrap
{
    public class AppContainer
    {
        public static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //services - data
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.Build();
        }
    }
}
