using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Pumpkin.Contract.Transaction;

namespace Pumpkin.Web.Extensions
{
    public static class ActionDescriptorExtension
    {
        public static ControllerActionDescriptor AsControllerActionDescriptor(this ActionDescriptor actionDescriptor)
        {
            var controllerActionDescriptor = actionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null)
            {
                throw new Exception(
                    $"{nameof(actionDescriptor)} should be type of {typeof(ControllerActionDescriptor).AssemblyQualifiedName}");
            }

            return controllerActionDescriptor;
        }

        public static MethodInfo GetMethodInfo(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.AsControllerActionDescriptor().MethodInfo;
        }

        public static TransactionAttribute GetTransactionAttribute(this MethodInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(true).OfType<TransactionAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            }

            attrs = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<TransactionAttribute>()
                .ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            }

            return new TransactionAttribute();
        }

        public static TransactionAttribute GetTransactionAttribute(this Type type)
        {
            var attrs = type.GetCustomAttributes(true).OfType<TransactionAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            }

            return new TransactionAttribute();
        }
    }
}