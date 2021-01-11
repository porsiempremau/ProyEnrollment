using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Extensions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewLayoutAttribute : Attribute
    {
        public ViewLayoutAttribute(string layoutName)
        {
            this.LayoutName = layoutName;
        }

        public string LayoutName { get; }
    }
}
