using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MvcDataAccess.Models;

namespace MvcBusinessLogic.Installers
{
    public class SchoolInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<CourseController>());
            container.Register(Component.For<DepartmentController>());
            container.Register(Component.For<SchoolEntities>().LifestyleTransient());

            container.Register(Component.For<ICourseCrud>().ImplementedBy<CourseCrud>());
            container.Register(Component.For<IDepartmentCrud>().ImplementedBy<DepartmentCrud>());
        }
    }
}
