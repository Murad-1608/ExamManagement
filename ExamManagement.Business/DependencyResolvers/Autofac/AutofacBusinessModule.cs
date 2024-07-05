using Autofac;
using Castle.DynamicProxy;
using ExamManagement.Business.Utilities.Interceptors;
using Autofac.Extras.DynamicProxy;
using ExamManagement.DataAccess.Concrete.EntityFramework;
using ExamManagement.DataAccess.Abstract;
using ExamManagement.Business.Concrete;
using ExamManagement.Business.Abstract;

namespace ExamManagement.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfStudentDal>().As<IStudentDal>().InstancePerLifetimeScope();
            builder.RegisterType<StudentManager>().As<IStudentService>().InstancePerLifetimeScope();

            builder.RegisterType<EfTeacherDal>().As<ITeacherDal>().InstancePerLifetimeScope();
            builder.RegisterType<TeacherManager>().As<ITeacherService>().InstancePerLifetimeScope();

            builder.RegisterType<EfSubjectDal>().As<ISubjectDal>().InstancePerLifetimeScope();
            builder.RegisterType<SubjectManager>().As<ISubjectService>().InstancePerLifetimeScope();

            builder.RegisterType<EfExamDal>().As<IExamDal>().InstancePerLifetimeScope();
            builder.RegisterType<ExamManager>().As<IExamService>().InstancePerLifetimeScope();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
