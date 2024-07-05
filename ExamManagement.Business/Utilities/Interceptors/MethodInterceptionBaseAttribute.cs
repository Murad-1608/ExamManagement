using Castle.DynamicProxy;

namespace ExamManagement.Business.Utilities.Interceptors
{
    public partial class AspectInterceptorSelector
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
        public abstract class MethodInterceptionBaseAttribute : Attribute, Castle.DynamicProxy.IInterceptor
        {
            public int Priority { get; set; }

            public virtual void Intercept(IInvocation invocation)
            {

            }
        }
    }
}
