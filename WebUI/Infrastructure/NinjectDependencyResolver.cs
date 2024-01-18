using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Moq;
using Ninject;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<IBookRepository> mock = new Mock<IBookRepository>();
            //mock.Setup(m => m.Books).Returns(new List<Book>
            //{
            //    new Book {Title = "An Introduction to C & GUI Programming, 2nd Edition", Subtitle = "Architecting, Designing, and Deploying on the Snowflake Data Cloud", Price = 5890 },
            //    new Book {Title = "Snowflake: The Definitive Guide", Subtitle = "Architecting, Designing, and Deploying on the Snowflake Data Cloud", Price = 5890 },
            //    new Book {Title = "Python for Data Analysis, 3rd Edition", Subtitle = "Architecting, Designing, and Deploying on the Snowflake Data Cloud", Price = 5890 }
            //});
            kernel.Bind<IBookRepository>().To<EFBookRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }
    }
}