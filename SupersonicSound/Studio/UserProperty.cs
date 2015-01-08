using FMOD.Studio;
using System;
using System.Runtime.InteropServices;

namespace SupersonicSound.Studio
{
    public struct UserProperty
    {
        public string Name { get; private set; }

        public UserPropertyType Type { get; private set; }

        private readonly UnionIntBoolFloat _value;

        public int IntValue
        {
            get
            {
                Expect(UserPropertyType.Integer);
                return _value.IntValue;
            }
        }

        public bool BooleanValue
        {
            get
            {
                Expect(UserPropertyType.Boolean);
                return _value.BoolValue;
            }
        }

        public float SingleValue
        {
            get
            {
                Expect(UserPropertyType.Single);
                return _value.FloatValue;
            }
        }

        private readonly string _stringValue;
        public string StringValue
        {
            get
            {
                Expect(UserPropertyType.String);
                return _stringValue;
            }
        }

        public UserProperty(USER_PROPERTY property)
            : this()
        {
            Name = property.name;
            Type = (UserPropertyType)property.type;

            switch (Type)
            {
                case UserPropertyType.Integer:
                    _value.IntValue = property.intValue;
                    break;
                case UserPropertyType.Single:
                    _value.FloatValue = property.floatValue;
                    break;
                case UserPropertyType.Boolean:
                    _value.BoolValue = property.boolValue;
                    break;
                case UserPropertyType.String:
                    _stringValue = property.stringValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Expect(UserPropertyType type)
        {
            if (type != Type)
                throw new InvalidOperationException(string.Format("Attempted to access user property {0} of type {1} as a user property of type {2}", Name, Type, type));
        }
    }

    [EquivalentEnum(typeof(USER_PROPERTY_TYPE))]
    public enum UserPropertyType
    {
        Integer = USER_PROPERTY_TYPE.INTEGER,

        Single = USER_PROPERTY_TYPE.FLOAT,

        Boolean = USER_PROPERTY_TYPE.BOOLEAN,

        String = USER_PROPERTY_TYPE.STRING
    }

    [StructLayout(LayoutKind.Explicit)]
    struct UnionIntBoolFloat
    {
        [FieldOffset(0)]
        public int IntValue;
        [FieldOffset(0)]
        public bool BoolValue;
        [FieldOffset(0)]
        public float FloatValue;
    }
}
