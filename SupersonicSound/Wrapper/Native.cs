#if DEBUG
using System.Linq;
using System.Reflection;
#endif

using FMOD;
using FMOD.Studio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace SupersonicSound.Wrapper
{
    public static class Native
    {
        [DllImport("Kernel32.dll")]
        private static extern IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool FreeLibrary(IntPtr hModule);

        private static readonly List<IntPtr> Loaded = new List<IntPtr>();

        /// <summary>
        /// Load a dll from the Dependencies/{{ platform specific name }} directory. Automatically switches based on if this is a 32 or 64 bit process.
        /// </summary>
        /// <param name="name"></param>
        private static void LoadSystemDependentDll(string name)
        {
            var directory = "x86";
            var fileName = name.Split('.')[0];
            if (Environment.Is64BitProcess)
            {
                directory = "x86_64";
            }

            var path = Path.Combine(Environment.CurrentDirectory, "Dependencies", directory, name);

            lock (LoadLock)
            {
                var ptr = LoadLibrary(path);
                if (ptr == IntPtr.Zero)
                    throw new DllNotFoundException($"Failed to load DLL '{path}'. See https://github.com/martindevans/SupersonicSound#installation-instructions");

                Loaded.Add(ptr);
            }
        }

        public static bool IsLoaded { get; private set; }
        private static readonly object LoadLock = new object();

        /// <summary>
        /// Load native DLLs (if necessary)
        /// </summary>
        public static void Load()
        {
            //Early exit
            if (IsLoaded || Util.IsUnix)
                return;

            //Lock
            lock (LoadLock)
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
            lock (LoadLock)
            {
                if (IsLoaded)
                    throw new InvalidOperationException("FMOD already loaded");

                IsLoaded = true;
            }
        }

        /// <summary>
        /// Unload native dependencies
        /// </summary>
        public static void Unload()
        {
            if (Util.IsUnix)
                return;

            lock (LoadLock)
            {
                if (!IsLoaded)
                    throw new InvalidOperationException("FMOD is not loaded");

                foreach (var intPtr in Loaded)
                    FreeLibrary(intPtr);
                Loaded.Clear();

                IsLoaded = false;
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
                        let a = new { t = type, a = type.GetCustomAttributes(typeof(EquivalentEnumAttribute), true).Cast<EquivalentEnumAttribute>().SingleOrDefault() }
                        where a.a != null
                        select a;

            foreach (var item in enums)
                item.a.Validate(item.t);
#endif
        }
    }
}
