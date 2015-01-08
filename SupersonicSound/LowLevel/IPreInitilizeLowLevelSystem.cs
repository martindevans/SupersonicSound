using System;

namespace SupersonicSound.LowLevel
{
    public interface IPreInitilizeLowLevelSystem
    {
        OutputMode Output { get; set; }

        int GetNumDrivers();

        DriverInfo GetDriverInfo(int id);

        int Driver { get; set; }

        int SoftwareChannels { get; set; }

        SoftwareFormat Format { get; set; }

        DspBufferConfiguration DspBufferConfiguration { get; set; }

        void SetFileSystem<THandle>(IFileSystem<THandle> fileSystem);

        void AttachFileSystem(Action<string, uint, IntPtr> opened = null, Action<IntPtr> closed = null, Action<IntPtr, uint, uint> read = null, Action<IntPtr, int> seeked = null);

        void SetCallback(Action<LowLevelSystem, SystemCallbackType, IntPtr, IntPtr> callback, SystemCallbackType callbackMask);
    }
}
