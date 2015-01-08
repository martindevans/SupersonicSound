using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;

namespace SupersonicSound.Wrapper
{
    public static class Native
    {
        [DllImport("Kernel32.dll")]
        private static extern IntPtr LoadLibrary(string path);

        /// <summary>
        /// Load a dll from the Wrapper/Dependencies/{{ platform specific name }} directory. Automatically switches based on if this is a 32 or 64 bit process.
        /// </summary>
        /// <param name="name"></param>
        private static void LoadSystemDependentDll(string name)
        {
            string directory = "x86";
            if (Environment.Is64BitProcess)
                directory = "x86_64";

            var path = Path.Combine(Environment.CurrentDirectory, "Wrapper", "Dependencies", directory, name);
            LoadLibrary(path);
        }

        public static bool IsLoaded { get; private set; }
        private static readonly object _loadLock = new object();

        /// <summary>
        /// Load native DLLs (if necessary)
        /// </summary>
        public static void Load()
        {
            //Early exit
            if (IsLoaded)
                return;

            //Lock
            lock (_loadLock)
            {
                //Check again
                if (IsLoaded)
                    return;

                LoadSystemDependentDll(VERSION.dll);
                LoadSystemDependentDll(STUDIO_VERSION.dll);

                CheckCompatibility();

                IsLoaded = true;
            }
            
        }

        /// <summary>
        /// Set the "IsLoaded" flag which will prevent DLLs from being loaded. Use this if you have loaded the necessary DLLs yourself
        /// </summary>
        public static void SuppressLoad()
        {
            lock (_loadLock)
            {
                if (IsLoaded)
                    throw new InvalidOperationException("FMOD already loaded");

                IsLoaded = true;
            }
        }

        /// <summary>
        /// Perform checks on the compatibility of this wrapper with the lower level FMOD wrapper (based on attributes)
        /// </summary>
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
