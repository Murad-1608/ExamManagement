﻿using Castle.DynamicProxy;
using System.Reflection;

namespace ExamManagement.Business.Utilities.Interceptors
{
    public partial class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            // --> Bu hisseye yazdığın attribut bütün methodlarda isleyir
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
