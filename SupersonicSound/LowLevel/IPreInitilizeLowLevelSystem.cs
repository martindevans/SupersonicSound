
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

        //void setFileSystem(FILE_OPENCALLBACK useropen, FILE_CLOSECALLBACK userclose, FILE_READCALLBACK userread, FILE_SEEKCALLBACK userseek, FILE_ASYNCREADCALLBACK userasyncread, FILE_ASYNCCANCELCALLBACK userasynccancel, int blockalign);

        //void attachFileSystem(FILE_OPENCALLBACK useropen, FILE_CLOSECALLBACK userclose, FILE_READCALLBACK userread, FILE_SEEKCALLBACK userseek);

        //void setCallback(SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask);
    }
}
