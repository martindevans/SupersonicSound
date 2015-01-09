using FMOD;

namespace SupersonicSound.LowLevel
{
    [EquivalentEnum(typeof(SPEAKER), "MAX")]
    public enum Speaker
    {
        FrontLeft = SPEAKER.FRONT_LEFT,
        FrontRight = SPEAKER.FRONT_RIGHT,
        FrontCenter = SPEAKER.FRONT_CENTER,
        LowFrequency = SPEAKER.LOW_FREQUENCY,
        SurroundLeft = SPEAKER.SURROUND_LEFT,
        SurroundRight = SPEAKER.SURROUND_RIGHT,
        BackLeft = SPEAKER.BACK_LEFT,
        BackRight = SPEAKER.BACK_RIGHT,
    }
}
