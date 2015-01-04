using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using System = FMOD.Studio.System;
using Debug = System.Diagnostics.Debug;

namespace SupersonicSound.Wrapper
{
    public static class Native
    {
        [DllImport("Kernel32.dll")]
        private static extern IntPtr LoadLibrary(string path);

        private static IntPtr LoadSystemDependentDll(string name)
        {
            string directory = "x86";
            if (Environment.Is64BitProcess)
                directory = "x86_64";

            var path = Path.Combine(Environment.CurrentDirectory, "Wrapper", "Dependencies", directory, name);
            return LoadLibrary(path);
        }

        private static bool _isLoaded = false;
        private static readonly object _loadLock = new object();

        public static void Load()
        {
            //Early exit
            if (_isLoaded)
                return;

            //Lock
            lock (_loadLock)
            {
                //Check again
                if (_isLoaded)
                    return;

                LoadSystemDependentDll(VERSION.dll);
                LoadSystemDependentDll(STUDIO_VERSION.dll);

                CheckCompatibility();

                _isLoaded = true;
            }
            
        }

        private static void CheckCompatibility()
        {
#if DEBUG
            var enums = from type in Assembly.GetExecutingAssembly().GetTypes()
                        where type.IsEnum
                        let a = new { t = type, a = type.GetCustomAttribute<EquivalentEnumAttribute>() }
                        where a.a != null
                        select a;

            foreach (var item in enums)
                item.a.Validate(item.t);
#endif
        }
    }
}
