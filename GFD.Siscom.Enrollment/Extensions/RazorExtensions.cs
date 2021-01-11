using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Extensions
{
    public static class RazorExtensions
    {
        /// <summary>
        /// Gets the <see cref="ViewLayoutAttribute"/> from the current calling controller of the
        /// <see cref="ViewContext"/>.
        /// </summary>
        public static ViewLayoutAttribute GetLayoutAttribute(this ViewContext viewContext)
        {
            // See if Razor Page...
            if (viewContext.ActionDescriptor is CompiledPageActionDescriptor actionDescriptor)
            {
                // We want the attribute no matter what.
                return Attribute.GetCustomAttribute(actionDescriptor.ModelTypeInfo, typeof(ViewLayoutAttribute)) as ViewLayoutAttribute;
            }

            // See if MVC Controller...

            // Property ControllerTypeInfo can be seen on runtime.
            var controllerType = (Type)viewContext.ActionDescriptor
                .GetType()
                .GetProperty("ControllerTypeInfo")?
                .GetValue(viewContext.ActionDescriptor);

            if (controllerType != null && controllerType.IsSubclassOf(typeof(Microsoft.AspNetCore.Mvc.Controller)))
            {
                return Attribute.GetCustomAttribute(controllerType, typeof(ViewLayoutAttribute)) as ViewLayoutAttribute;
            }

            // Nothing found.
            return null;
        }
    }
}
