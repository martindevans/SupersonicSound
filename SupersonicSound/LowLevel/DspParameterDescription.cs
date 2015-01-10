using System;
using FMOD;

namespace SupersonicSound.LowLevel
{
    public struct DspParameterDescription
    {
        public DspParameterType Type { get; private set; }

        public string Name { get; private set; }

        public string Label { get; private set; }

        public string Description { get; private set; }

        private readonly DSP_PARAMETER_DESC_UNION _descUnion;

        public DspParameterDescriptionBool BoolDescription
        {
            get
            {
                if (Type!= DspParameterType.Boolean)
                    throw new InvalidOperationException("Cannot fetch bool parameters, parameter is not bool");
                return new DspParameterDescriptionBool(_descUnion.booldesc);
            }
        }

        public DspParameterDescriptionInt IntDescription
        {
            get
            {
                if (Type != DspParameterType.Integer)
                    throw new InvalidOperationException("Cannot fetch int parameters, parameter is not int");
                return new DspParameterDescriptionInt(_descUnion.intdesc);
            }
        }

        public DspParameterDescriptionFloat FloatDescription
        {
            get
            {
                if (Type != DspParameterType.Float)
                    throw new InvalidOperationException("Cannot fetch float parameters, parameter is not float");
                return new DspParameterDescriptionFloat(_descUnion.floatdesc);
            }
        }

        private DspParameterDescription(string name, string label, string description, DspParameterType type)
            : this()
        {
            Type = type;
            Name = name;
            Label = label;
            Description = description;
        }

        public DspParameterDescription(string name, string label, string description, DspParameterDescriptionBool dBool)
            : this(name, label, description, DspParameterType.Boolean)
        {
            _descUnion.booldesc = dBool.ToFmod();
        }

        public DspParameterDescription(string name, string label, string description, DspParameterDescriptionInt dInt)
            : this(name, label, description, DspParameterType.Integer)
        {
            _descUnion.intdesc = dInt.ToFmod();
        }

        public DspParameterDescription(string name, string label, string description, DspParameterDescriptionFloat dFloat)
            : this(name, label, description, DspParameterType.Float)
        {
            _descUnion.floatdesc = dFloat.ToFmod();
        }

        //public DspParameterDescription(string name, string label, string description, DspParameterDescriptionData dData)
        //  : this(name, label, description, DspParameterType.Data)
        //{
        //      _descUnion = dData.ToFmod();
        //}

        public DspParameterDescription(ref DSP_PARAMETER_DESC desc)
            : this()
        {
            Type = (DspParameterType)desc.type;
            Name = new string(desc.name);
            Label = new string(desc.label);
            Description = desc.description;

            _descUnion = desc.desc;
        }
    }

    public struct DspParameterDescriptionBool
    {
        public bool DefaultValue { get; private set; }

        public DspParameterDescriptionBool(bool defaultValue)
            : this()
        {
            DefaultValue = defaultValue;
        }

        public DspParameterDescriptionBool(DSP_PARAMETER_DESC_BOOL dBool)
            : this(dBool.defaultval)
        {
        }

        public DSP_PARAMETER_DESC_BOOL ToFmod()
        {
            return new DSP_PARAMETER_DESC_BOOL {
                defaultval = DefaultValue
            };
        }
    }

    public struct DspParameterDescriptionInt
    {
        public int DefaultValue { get; private set; }
        public int Min { get; private set; }
        public int Max { get; private set; }
        public bool GoesToInfinity { get; private set; }

        public DspParameterDescriptionInt(int defaultValue, int min, int max, bool goesToInfinity)
            : this()
        {
            DefaultValue = defaultValue;
            Min = min;
            Max = max;
            GoesToInfinity = goesToInfinity;
        }

        public DspParameterDescriptionInt(DSP_PARAMETER_DESC_INT dInt)
            : this(dInt.defaultval, dInt.min, dInt.max, dInt.goestoinf)
        {
        }

        public DSP_PARAMETER_DESC_INT ToFmod()
        {
            return new DSP_PARAMETER_DESC_INT {
                defaultval = DefaultValue,
                goestoinf = GoesToInfinity,
                max = Max,
                min = Min
            };
        }
    }

    public struct DspParameterDescriptionFloat
    {
        public float DefaultValue { get; private set; }
        public float Min { get; private set; }
        public float Max { get; private set; }

        public DspParameterDescriptionFloat(float defaultValue, float min, float max)
            : this()
        {
            DefaultValue = defaultValue;
            Min = min;
            Max = max;
        }

        public DspParameterDescriptionFloat(DSP_PARAMETER_DESC_FLOAT dFloat)
            : this(dFloat.defaultval, dFloat.min, dFloat.max)
        {
        }

        public DSP_PARAMETER_DESC_FLOAT ToFmod()
        {
            return new DSP_PARAMETER_DESC_FLOAT {
                defaultval = DefaultValue,
                min = Min,
                max = Max,
            };
        }
    }

    [EquivalentEnum(typeof(DSP_PARAMETER_TYPE))]
    public enum DspParameterType
    {
        Float = DSP_PARAMETER_TYPE.FLOAT,
        Integer = DSP_PARAMETER_TYPE.INT,
        Boolean = DSP_PARAMETER_TYPE.BOOL,
        Data = DSP_PARAMETER_TYPE.DATA
    }
}
