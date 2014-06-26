using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinStudio.Permission.IBusiness.Reception
{
    public class LockerManager
    {
        private static ReaderWriterLockSlim _lockerManager = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        internal static ReaderWriterLockSlim State
        {
            get
            {
                return _lockerManager;
            }
        }
    }
}
