using System;
using System.Collections.Generic;
using System.Linq;

namespace Pumpkin.Common
{
    public static class AssemblyScanner
    {
        public static IEnumerable<Type> AllTypes
        {
            get { return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()); }
        }
    }
}