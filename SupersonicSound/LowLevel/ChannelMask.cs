using FMOD;
using System;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(CHANNELMASK))]
    [Flags]
    public enum ChannelMask
        : uint
    {
        FrontLeft = CHANNELMASK.FRONT_LEFT,
        FrontRight = CHANNELMASK.FRONT_RIGHT,
        FrontCenter = CHANNELMASK.FRONT_CENTER,
        LowFrequency = CHANNELMASK.LOW_FREQUENCY,
        SurroundLeft = CHANNELMASK.SURROUND_LEFT,
        SurroundRight = CHANNELMASK.SURROUND_RIGHT,
        BackLeft = CHANNELMASK.BACK_LEFT,
        BackRight = CHANNELMASK.BACK_RIGHT,
        BackCenter = CHANNELMASK.BACK_CENTER,

        Mono = CHANNELMASK.MONO,
        Stereo = CHANNELMASK.STEREO,
        Lrc = CHANNELMASK.LRC,
        Quad = CHANNELMASK.QUAD,
        Surround = CHANNELMASK.SURROUND,
        FivePointOne = CHANNELMASK._5POINT1,
        FivePointOneRears = CHANNELMASK._5POINT1_REARS,
        SevenPointZero = CHANNELMASK._7POINT0,
        SevenPointOne = CHANNELMASK._7POINT1
    }
}
