﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net.Config;
using NHibernate;
using NHibernate.Context;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using WebApi2Book.Common;
using WebApi2Book.Common.Logging;
using WebApi2Book.Common.Security;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Data.SqlServer.Mapping;
using WebApi2Book.Web.Common;

namespace WebApi2Book.Web.Api
{
    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        private void AddBindings(IKernel container)
        {
            ConfigureLog4net(container);
            ConfigureUserSession(container);
            ConfigureNHibernate(container);

            container.Bind<IDateTime>().To<DateTimeAdapter>().InSingletonScope();
            container.Bind<IAddTaskQueryProcessor>().To<AddTaskQueryProcessor>().InRequestScope();
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();
            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }

        private void ConfigureNHibernate(IKernel container)
        {
            var sessionFactory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("WebApi2BookDb")))
                .CurrentSessionContext("web")
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                .BuildSessionFactory();

            container.Bind<ISessionFactory>().ToConstant(sessionFactory);
            container.Bind<ISession>().ToMethod(CreateSession).InRequestScope();
            container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>().InRequestScope();
        }

        private ISession CreateSession(IContext context)
        {
            var sessionFactory = context.Kernel.Get<ISessionFactory>();

            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                var session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return sessionFactory.GetCurrentSession();
        }

        private void ConfigureUserSession(IKernel container)
        {
            var userSession = new UserSession();
            container.Bind<IUserSession>().ToConstant(userSession).InSingletonScope();
            container.Bind<IWebUserSession>().ToConstant(userSession).InSingletonScope();
        }
    }
}