using FMOD;
using System.Runtime.InteropServices;

namespace SupersonicSound.LowLevel
{
    public struct Tag
    {
        /// <summary>
        /// The type of this tag.
        /// </summary>
        public TagType Type { get; private set; }

        /// <summary>
        /// The type of data that this tag contains
        /// </summary>
        public TagDataType DataType { get; private set; }

        /// <summary>
        /// The name of this tag i.e. "TITLE", "ARTIST" etc.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// True if this tag has been updated since last being accessed with Sound::getTag
        /// </summary>
        public bool Updated { get; private set; }

        /// <summary>
        /// Data of this tag
        /// </summary>
        public byte[] Data { get; private set; }

        public Tag(TAG tag)
            : this()
        {
            Name = tag.name;
            Type = (TagType)tag.type;
            DataType = (TagDataType)tag.datatype;
            Updated = tag.updated;

            Data = new byte[tag.datalen];
            Marshal.Copy(tag.data, Data, 0, (int)tag.datalen);
        }
    }

    [EquivalentEnum(typeof(TAGTYPE), "MAX")]
    public enum TagType
    {
        Unknown = TAGTYPE.UNKNOWN,
        ID3V1 = TAGTYPE.ID3V1,
        ID3V2 = TAGTYPE.ID3V2,
        VorbisComment = TAGTYPE.VORBISCOMMENT,
        ShoutCast = TAGTYPE.SHOUTCAST,
        IceCast = TAGTYPE.ICECAST,
        ASF = TAGTYPE.ASF,
        MIDI = TAGTYPE.MIDI,
        Playlist = TAGTYPE.PLAYLIST,
        FMOD = TAGTYPE.FMOD,
        User = TAGTYPE.USER,
    }

    [EquivalentEnum(typeof(TAGDATATYPE), "MAX")]
    public enum TagDataType
    {
        Binary = TAGDATATYPE.BINARY,
        Integer = TAGDATATYPE.INT,
        Float = TAGDATATYPE.FLOAT,
        String = TAGDATATYPE.STRING,
        UTF16 = TAGDATATYPE.STRING_UTF16,
        UTF16BE = TAGDATATYPE.STRING_UTF16BE,
        UTF8 = TAGDATATYPE.STRING_UTF8,
        CDTOC = TAGDATATYPE.CDTOC,
    }
}
