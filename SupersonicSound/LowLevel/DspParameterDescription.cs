using FMOD;

namespace SupersonicSound.LowLevel
{
    public struct DspParameterDescription
    {
        public DspParameterType Type { get; private set; }

        public string Name { get; private set; }

        public string Label { get; private set; }

        public string Description { get; private set; }

        public DspParameterDescription(DSP_PARAMETER_DESC desc)
            : this()
        {
            Type = (DspParameterType)desc.type;
            Name = new string(desc.name);
            Label = new string(desc.label);
            Description = desc.description;
        }
    }

    [EquivalentEnum(typeof(DSP_PARAMETER_TYPE))]
    public enum DspParameterType
    {
        Float = DSP_PARAMETER_TYPE.FLOAT,
        Integer = DSP_PARAMETER_TYPE.INT,
        Boolean = DSP_PARAMETER_TYPE.BOOL,
        Ddata = DSP_PARAMETER_TYPE.DATA
    }
}
