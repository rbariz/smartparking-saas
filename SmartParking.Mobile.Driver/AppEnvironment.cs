using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Mobile.Driver
{
    public static class AppEnvironment
    {
#if DEBUG
        public const bool IsDev = true;
#else
    public const bool IsDev = false;
#endif
    }
}
