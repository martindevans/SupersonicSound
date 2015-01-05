using FMOD.Studio;

namespace SupersonicSound.Studio
{
    public struct ParameterDescription
    {
        /// <summary>
        /// Name of the parameter.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Minimum parameter value.
        /// </summary>
        public float Minimum { get; private set; }

        /// <summary>
        /// Maximum parameter value.
        /// </summary>
        public float Maximum { get; private set; }

        /// <summary>
        /// Type of the parameter
        /// </summary>
        public ParameterType Type { get; private set; }

        public ParameterDescription(PARAMETER_DESCRIPTION description)
            : this()
        {
            Name = description.name;
            Minimum = description.minimum;
            Maximum = description.maximum;
            Type = (ParameterType)description.type;
        }
    }
}
