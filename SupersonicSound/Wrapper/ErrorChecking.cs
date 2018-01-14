using System.Collections.Generic;
using System.Linq;
using FMOD;
using SupersonicSound.Exceptions;
using System;

namespace SupersonicSound.Wrapper
{
    public static class ErrorChecking
    {
        #region suppressing
        internal static readonly IReadOnlyList<RESULT> SuppressInvalidHandle = new[] {
            RESULT.ERR_INVALID_HANDLE
        };

        internal static readonly IReadOnlyList<RESULT> SuppressChannelStolen = new[] {
            RESULT.ERR_CHANNEL_STOLEN
        };

        internal static readonly IReadOnlyList<RESULT> SuppressInvalidHandleAndStolenChannel = new[] {
            RESULT.ERR_INVALID_HANDLE,
            RESULT.ERR_CHANNEL_STOLEN
        };

        internal static IReadOnlyList<RESULT> Suppress(bool throwHandle, bool throwStolen)
        {
            if (throwHandle && throwStolen)
                return null;

            if (throwHandle)
                return SuppressChannelStolen;

            return throwStolen ? 
                SuppressInvalidHandle 
              : SuppressInvalidHandleAndStolenChannel;
        }
        #endregion

        internal static T Unbox<T>(this T? value) where T : struct
        {
            if (!value.HasValue)
                throw new ArgumentNullException(nameof(value), "Value is null");
            return value.Value;
        }

        internal static T? CheckBox<T>(this RESULT result, T value, IReadOnlyList<RESULT> suppress = null) where T : struct
        {
            if (suppress != null && suppress.Contains(result))
                return null;
            Check(result);

            return value;
        }

        public static bool Check(this RESULT result, IReadOnlyList<RESULT> suppress)
        {
            if (suppress != null && suppress.Contains(result))
                return false;

            Check(result);
            return true;
        }

        public static void Check(this RESULT result)
        {
            switch (result)
            {
                case RESULT.OK:
                    return;

                case RESULT.ERR_BADCOMMAND:
                    throw new FmodBadCommandException();
                case RESULT.ERR_CHANNEL_ALLOC:
                    throw new FmodChannelAllocException();
                case RESULT.ERR_CHANNEL_STOLEN:
                    throw new FmodChannelStolenException();
                case RESULT.ERR_DMA:
                    throw new FmodDmaException();
                case RESULT.ERR_DSP_CONNECTION:
                    throw new FmodDspConnectionException();
                case RESULT.ERR_DSP_DONTPROCESS:
                    throw new FmodDoNotProcessException();
                case RESULT.ERR_DSP_FORMAT:
                    throw new FmodDspFormatException();
                case RESULT.ERR_DSP_INUSE:
                    throw new FmodDspInUseException();
                case RESULT.ERR_DSP_NOTFOUND:
                    throw new FmodDspNotFoundException();
                case RESULT.ERR_DSP_RESERVED:
                    throw new FmodDspReservedException();
                case RESULT.ERR_DSP_SILENCE:
                    throw new FmodDspSilenceException();
                case RESULT.ERR_DSP_TYPE:
                    throw new FmodDspTypeException();
                case RESULT.ERR_FILE_BAD:
                    throw new FmodBadFileException();
                case RESULT.ERR_FILE_COULDNOTSEEK:
                    throw new FmodCouldNotSeekException();
                case RESULT.ERR_FILE_DISKEJECTED:
                    throw new FmodDiskEjectedException();
                case RESULT.ERR_FILE_EOF:
                    throw new FmodEndOfFileException();
                case RESULT.ERR_FILE_ENDOFDATA:
                    throw new FmodEndOfDataException();
                case RESULT.ERR_FILE_NOTFOUND:
                    throw new FmodFileNotFoundException();
                case RESULT.ERR_FORMAT:
                    throw new FmodFormatException();
                case RESULT.ERR_HEADER_MISMATCH:
                    throw new FmodHeaderMismatchException();
                case RESULT.ERR_HTTP:
                    throw new FmodHttpException();
                case RESULT.ERR_HTTP_ACCESS:
                    throw new FmodHttpAccessException();
                case RESULT.ERR_HTTP_PROXY_AUTH:
                    throw new FmodHttpProxyAuthException();
                case RESULT.ERR_HTTP_SERVER_ERROR:
                    throw new FmodHttpServerErrorException();
                case RESULT.ERR_HTTP_TIMEOUT:
                    throw new FmodHttpTimeoutException();
                case RESULT.ERR_INITIALIZATION:
                    throw new FmodInitializationException();
                case RESULT.ERR_INITIALIZED:
                    throw new FmodInitializedException();
                case RESULT.ERR_INTERNAL:
                    throw new FmodInternalException();
                case RESULT.ERR_INVALID_FLOAT:
                    throw new FmodInvalidFloatException();
                case RESULT.ERR_INVALID_HANDLE:
                    throw new FmodInvalidHandleException();
                case RESULT.ERR_INVALID_PARAM:
                    throw new FmodInvalidParamException();
                case RESULT.ERR_INVALID_POSITION:
                    throw new FmodInvalidPositionException();
                case RESULT.ERR_INVALID_SPEAKER:
                    throw new FmodInvalidSpeakerException();
                case RESULT.ERR_INVALID_SYNCPOINT:
                    throw new FmodInvalidSyncPointException();
                case RESULT.ERR_INVALID_THREAD:
                    throw new FmodInvalidThreadException();
                case RESULT.ERR_INVALID_VECTOR:
                    throw new FmodInvalidVectorException();
                case RESULT.ERR_MAXAUDIBLE:
                    throw new FmodMaxAudibleException();
                case RESULT.ERR_MEMORY:
                    throw new FmodMemoryException();
                case RESULT.ERR_MEMORY_CANTPOINT:
                    throw new FmodMemoryCannotPointException();
                case RESULT.ERR_NEEDS3D:
                    throw new FmodNeeds3DException();
                case RESULT.ERR_NEEDSHARDWARE:
                    throw new FmodNeedsHardwareException();
                case RESULT.ERR_NET_CONNECT:
                    throw new FmodNetConnectException();
                case RESULT.ERR_NET_SOCKET_ERROR:
                    throw new FmodNetSocketException();
                case RESULT.ERR_NET_URL:
                    throw new FmodNetUrlException();
                case RESULT.ERR_NET_WOULD_BLOCK:
                    throw new FmodNetWouldBlockException();
                case RESULT.ERR_NOTREADY:
                    throw new FmodNotReadyException();
                case RESULT.ERR_OUTPUT_ALLOCATED:
                    throw new FmodOutputAllocatedException();
                case RESULT.ERR_OUTPUT_CREATEBUFFER:
                    throw new FmodOutputCreateBufferException();
                case RESULT.ERR_OUTPUT_DRIVERCALL:
                    throw new FmodOutputDriverCallException();
                case RESULT.ERR_OUTPUT_FORMAT:
                    throw new FmodOutputFormatException();
                case RESULT.ERR_OUTPUT_INIT:
                    throw new FmodOutputInitException();
                case RESULT.ERR_OUTPUT_NODRIVERS:
                    throw new FmodOutputNoDriversException();
                case RESULT.ERR_PLUGIN:
                    throw new FmodPluginUnspecifiedErrorException();
                case RESULT.ERR_PLUGIN_MISSING:
                    throw new FmodPluginMissingException();
                case RESULT.ERR_PLUGIN_RESOURCE:
                    throw new FmodPluginResourceException();
                case RESULT.ERR_PLUGIN_VERSION:
                    throw new FmodPluginVersionException();
                case RESULT.ERR_RECORD:
                    throw new FmodRecordException();
                case RESULT.ERR_REVERB_CHANNELGROUP:
                    throw new FmodReverbChannelgroupException();
                case RESULT.ERR_REVERB_INSTANCE:
                    throw new FmodReverbInstanceException();
                case RESULT.ERR_SUBSOUNDS:
                    throw new FmodSubsoundsException();
                case RESULT.ERR_SUBSOUND_ALLOCATED:
                    throw new FmodSubsoundAllocatedException();
                case RESULT.ERR_SUBSOUND_CANTMOVE:
                    throw new FmodSubsoundCannotMoveException();
                case RESULT.ERR_TAGNOTFOUND:
                    throw new FmodTagNotFoundException();
                case RESULT.ERR_TOOMANYCHANNELS:
                    throw new FmodTooManyChannelsException();
                case RESULT.ERR_TRUNCATED:
                    throw new FmodTruncatedException();
                case RESULT.ERR_UNIMPLEMENTED:
                    throw new FmodUnimplementedException();
                case RESULT.ERR_UNINITIALIZED:
                    throw new FmodUninitializedException();
                case RESULT.ERR_UNSUPPORTED:
                    throw new FmodUnsupportedException();
                case RESULT.ERR_VERSION:
                    throw new FmodVersionException();
                case RESULT.ERR_EVENT_ALREADY_LOADED:
                    throw new FmodEventAlreadyLoadedException();
                case RESULT.ERR_EVENT_LIVEUPDATE_BUSY:
                    throw new FmodEventLiveUpdateBusyException();
                case RESULT.ERR_EVENT_LIVEUPDATE_MISMATCH:
                    throw new FmodEventLiveUpdateMismatchException();
                case RESULT.ERR_EVENT_LIVEUPDATE_TIMEOUT:
                    throw new FmodEventLiveUpdateTimeoutException();
                case RESULT.ERR_EVENT_NOTFOUND:
                    throw new FmodEventNotFoundException();
                case RESULT.ERR_STUDIO_UNINITIALIZED:
                    throw new FmodStudioUninitializedException();
                case RESULT.ERR_STUDIO_NOT_LOADED:
                    throw new FmodStudioNotLoadedException();
                case RESULT.ERR_INVALID_STRING:
                    throw new FmodInvalidStringException();
                case RESULT.ERR_ALREADY_LOCKED:
                    throw new FmodAlreadyLockedException();
                case RESULT.ERR_NOT_LOCKED:
                    throw new FmodNotLockedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(result));
            }
        }
    }
}
