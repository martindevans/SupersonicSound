using System;
using System.IO;
using FMOD;
using SupersonicSound.Exceptions;

namespace SupersonicSound.Wrapper
{
    public static class Util
    {
        public static void Check(this RESULT result)
        {
            if (result == RESULT.OK)
                return;

            switch (result)
            {
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
                    break;
                case RESULT.ERR_DSP_NOTFOUND:
                    break;
                case RESULT.ERR_DSP_RESERVED:
                    break;
                case RESULT.ERR_DSP_SILENCE:
                    break;
                case RESULT.ERR_DSP_TYPE:
                    break;
                case RESULT.ERR_FILE_BAD:
                    break;
                case RESULT.ERR_FILE_COULDNOTSEEK:
                    break;
                case RESULT.ERR_FILE_DISKEJECTED:
                    break;
                case RESULT.ERR_FILE_EOF:
                    break;
                case RESULT.ERR_FILE_ENDOFDATA:
                    break;
                case RESULT.ERR_FILE_NOTFOUND:
                    throw new FileNotFoundException("FMOD File not found");
                case RESULT.ERR_FORMAT:
                    break;
                case RESULT.ERR_HEADER_MISMATCH:
                    break;
                case RESULT.ERR_HTTP:
                    break;
                case RESULT.ERR_HTTP_ACCESS:
                    break;
                case RESULT.ERR_HTTP_PROXY_AUTH:
                    break;
                case RESULT.ERR_HTTP_SERVER_ERROR:
                    break;
                case RESULT.ERR_HTTP_TIMEOUT:
                    break;
                case RESULT.ERR_INITIALIZATION:
                    break;
                case RESULT.ERR_INITIALIZED:
                    break;
                case RESULT.ERR_INTERNAL:
                    break;
                case RESULT.ERR_INVALID_FLOAT:
                    break;
                case RESULT.ERR_INVALID_HANDLE:
                    break;
                case RESULT.ERR_INVALID_PARAM:
                    break;
                case RESULT.ERR_INVALID_POSITION:
                    break;
                case RESULT.ERR_INVALID_SPEAKER:
                    break;
                case RESULT.ERR_INVALID_SYNCPOINT:
                    break;
                case RESULT.ERR_INVALID_THREAD:
                    break;
                case RESULT.ERR_INVALID_VECTOR:
                    break;
                case RESULT.ERR_MAXAUDIBLE:
                    break;
                case RESULT.ERR_MEMORY:
                    break;
                case RESULT.ERR_MEMORY_CANTPOINT:
                    break;
                case RESULT.ERR_NEEDS3D:
                    break;
                case RESULT.ERR_NEEDSHARDWARE:
                    break;
                case RESULT.ERR_NET_CONNECT:
                    break;
                case RESULT.ERR_NET_SOCKET_ERROR:
                    break;
                case RESULT.ERR_NET_URL:
                    break;
                case RESULT.ERR_NET_WOULD_BLOCK:
                    break;
                case RESULT.ERR_NOTREADY:
                    break;
                case RESULT.ERR_OUTPUT_ALLOCATED:
                    break;
                case RESULT.ERR_OUTPUT_CREATEBUFFER:
                    break;
                case RESULT.ERR_OUTPUT_DRIVERCALL:
                    break;
                case RESULT.ERR_OUTPUT_FORMAT:
                    break;
                case RESULT.ERR_OUTPUT_INIT:
                    break;
                case RESULT.ERR_OUTPUT_NODRIVERS:
                    break;
                case RESULT.ERR_PLUGIN:
                    break;
                case RESULT.ERR_PLUGIN_MISSING:
                    break;
                case RESULT.ERR_PLUGIN_RESOURCE:
                    break;
                case RESULT.ERR_PLUGIN_VERSION:
                    break;
                case RESULT.ERR_RECORD:
                    break;
                case RESULT.ERR_REVERB_CHANNELGROUP:
                    break;
                case RESULT.ERR_REVERB_INSTANCE:
                    break;
                case RESULT.ERR_SUBSOUNDS:
                    break;
                case RESULT.ERR_SUBSOUND_ALLOCATED:
                    break;
                case RESULT.ERR_SUBSOUND_CANTMOVE:
                    break;
                case RESULT.ERR_TAGNOTFOUND:
                    break;
                case RESULT.ERR_TOOMANYCHANNELS:
                    break;
                case RESULT.ERR_TRUNCATED:
                    break;
                case RESULT.ERR_UNIMPLEMENTED:
                    break;
                case RESULT.ERR_UNINITIALIZED:
                    break;
                case RESULT.ERR_UNSUPPORTED:
                    break;
                case RESULT.ERR_VERSION:
                    break;
                case RESULT.ERR_EVENT_ALREADY_LOADED:
                    break;
                case RESULT.ERR_EVENT_LIVEUPDATE_BUSY:
                    break;
                case RESULT.ERR_EVENT_LIVEUPDATE_MISMATCH:
                    break;
                case RESULT.ERR_EVENT_LIVEUPDATE_TIMEOUT:
                    break;
                case RESULT.ERR_EVENT_NOTFOUND:
                    break;
                case RESULT.ERR_STUDIO_UNINITIALIZED:
                    break;
                case RESULT.ERR_STUDIO_NOT_LOADED:
                    break;
                case RESULT.ERR_INVALID_STRING:
                    break;
                case RESULT.ERR_ALREADY_LOCKED:
                    break;
                case RESULT.ERR_NOT_LOCKED:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("result");
            }

            //todo: throw a specific type of exception for ever type of FMOD error
            throw new NotImplementedException("Generic FMOD Error (" + result + "): " + Error.String(result));
        }

        public static GUID ToFmod(this Guid guid)
        {
            var bytes = guid.ToByteArray();

            byte[] last8 = new byte[8];
            Array.Copy(bytes, 8, last8, 0, 8);

            return new GUID {
                Data1 = BitConverter.ToUInt32(bytes, 0),
                Data2 = BitConverter.ToUInt16(bytes, sizeof(uint)),
                Data3 = BitConverter.ToUInt16(bytes, sizeof(uint) + sizeof(ushort)),
                Data4 = last8,
            };
        }

        public static Guid FromFmod(this GUID guid)
        {
            return new Guid(
                guid.Data1,
                guid.Data2,
                guid.Data3,
                guid.Data4[0],
                guid.Data4[1],
                guid.Data4[2],
                guid.Data4[3],
                guid.Data4[4],
                guid.Data4[5],
                guid.Data4[6],
                guid.Data4[7]
            );
        }
    }
}
