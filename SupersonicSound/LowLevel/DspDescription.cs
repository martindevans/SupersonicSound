//using System;
//using System.Runtime.InteropServices;
//using FMOD;

//namespace SupersonicSound.LowLevel
//{
//    public class DspDescription
//        : IDisposable
//    {
//        private DSP_DESCRIPTION _description;

//        /// <summary>
//        /// The plugin SDK version this plugin is built for.  set to this to FMOD_PLUGIN_SDK_VERSION defined above.
//        /// </summary>
//        public uint PluginSdkVersion
//        {
//            get
//            {
//                return _description.pluginsdkversion;
//            }
//            set
//            {
//                _description.pluginsdkversion = value;
//            }
//        }

//        /// <summary>
//        /// Name of the unit to be displayed in the network.
//        /// </summary>
//        public string Name
//        {
//            get
//            {
//                return new string(_description.name);
//            }
//            set
//            {
//                if (value.Length > _description.name.Length)
//                    throw new ArgumentException(string.Format("name must be less than {0} characters", _description.name.Length), "value");
//                for (int i = 0; i < _description.name.Length; i++)
//                {
//                    if (i > value.Length)
//                        _description.name[i] = '\0';
//                    else
//                        _description.name[i] = value[i];
//                }
//            }
//        }

//        /// <summary>
//        /// Plugin writer's version number.
//        /// </summary>
//        public uint Version
//        {
//            get
//            {
//                return _description.version;
//            }
//            set
//            {
//                _description.version = value;
//            }
//        }

//        /// <summary>
//        /// Number of input buffers to process.  Use 0 for DSPs that only generate sound and 1 for effects that process incoming sound.
//        /// </summary>
//        public int NumInputBuffers
//        {
//            get
//            {
//                return _description.numinputbuffers;
//            }
//            set
//            {
//                _description.numinputbuffers = value;
//            }
//        }

//        /// <summary>
//        /// Number of audio output buffers.  Only one output buffer is currently supported.
//        /// </summary>
//        public int NumOututBuffers
//        {
//            get
//            {
//                return _description.numoutputbuffers;
//            }
//            set
//            {
//                _description.numoutputbuffers = value;
//            }
//        }

//        public DSP_CREATECALLBACK create;             /* [w] Create callback.  This is called when DSP unit is created.  Can be null. */
//        public DSP_RELEASECALLBACK release;            /* [w] Release callback.  This is called just before the unit is freed so the user can do any cleanup needed for the unit.  Can be null. */
//        public DSP_RESETCALLBACK reset;              /* [w] Reset callback.  This is called by the user to reset any history buffers that may need resetting for a filter, when it is to be used or re-used for the first time to its initial clean state.  Use to avoid clicks or artifacts. */
//        public DSP_READCALLBACK read;               /* [w] Read callback.  Processing is done here.  Can be null. */
//        public DSP_PROCESS_CALLBACK process;            /* [w] Process callback.  Can be specified instead of the read callback if any channel format changes occur between input and output.  This also replaces shouldiprocess and should return an error if the effect is to be bypassed.  Can be null. */
//        public DSP_SETPOSITIONCALLBACK setposition;        /* [w] Setposition callback.  This is called if the unit wants to update its position info but not process data.  Can be null. */

//        /// <summary>
//        /// Number of parameters used in this filter.  The user finds this with DSP::getNumParameters
//        /// </summary>
//        public int NumParameters
//        {
//            get
//            {
//                return _description.numparameters;
//            }
//        }

//        private IntPtr _dspParameters;

//        ///// <summary>
//        ///// Variable number of parameter structures.
//        ///// </summary>
//        //public DspParameterDescription[] DspParameters
//        //{
//        //    get
//        //    {
//        //        throw new NotImplementedException();
//        //    }
//        //    set
//        //    {
//        //        //Deallocate old array
//        //        if (_dspParameters != IntPtr.Zero)
//        //            Marshal.FreeHGlobal(_dspParameters);

//        //        //Allocate new array
//        //        _dspParameters = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DSP_PARAMETER_DESC)) * value.Length);
//        //        for (var i = 0; i < value.Length; i++)
//        //            Marshal.StructureToPtr(value[i].ToFmod(), new IntPtr(_dspParameters.ToInt64() + (Marshal.SizeOf(typeof(DSP_PARAMETER_DESC)) * i)), true);

//        //        _description.paramdesc = _dspParameters;
//        //    }
//        //}

//        public DSP_SETPARAM_FLOAT_CALLBACK setparameterfloat;  /* [w] This is called when the user calls DSP.setParameterFloat. Can be null. */
//        public DSP_SETPARAM_INT_CALLBACK setparameterint;    /* [w] This is called when the user calls DSP.setParameterInt.   Can be null. */
//        public DSP_SETPARAM_BOOL_CALLBACK setparameterbool;   /* [w] This is called when the user calls DSP.setParameterBool.  Can be null. */
//        public DSP_SETPARAM_DATA_CALLBACK setparameterdata;   /* [w] This is called when the user calls DSP.setParameterData.  Can be null. */
//        public DSP_GETPARAM_FLOAT_CALLBACK getparameterfloat;  /* [w] This is called when the user calls DSP.getParameterFloat. Can be null. */
//        public DSP_GETPARAM_INT_CALLBACK getparameterint;    /* [w] This is called when the user calls DSP.getParameterInt.   Can be null. */
//        public DSP_GETPARAM_BOOL_CALLBACK getparameterbool;   /* [w] This is called when the user calls DSP.getParameterBool.  Can be null. */
//        public DSP_GETPARAM_DATA_CALLBACK getparameterdata;   /* [w] This is called when the user calls DSP.getParameterData.  Can be null. */
//        public DSP_SHOULDIPROCESS_CALLBACK shouldiprocess;     /* [w] This is called before processing.  You can detect if inputs are idle and return FMOD_OK to process, or any other error code to avoid processing the effect.  Use a count down timer to allow effect tails to process before idling! */

//        /// <summary>
//        /// Optional. Specify 0 to ignore. This is user data to be attached to the DSP unit during creation.  Access via DSP::getUserData.
//        /// </summary>
//        public IntPtr UserData
//        {
//            get
//            {
//                return _description.userdata;
//            }
//            set
//            {
//                _description.userdata = value;
//            }
//        }

//        public DspDescription()
//        {
//        }

//        public DspDescription(DSP_DESCRIPTION description)
//        {
//            _description = description;
//        }

//        ~DspDescription()
//        {
//            Dispose(false);
//        }

//        public DSP_DESCRIPTION ToFmod()
//        {
//            return _description;
//        }

//        public void Dispose()
//        {
//            Dispose(true);

//            GC.SuppressFinalize(this);
//        }

//        private void Dispose(bool disposing)
//        {
//            if (_dspParameters != IntPtr.Zero)
//                Marshal.FreeHGlobal(_dspParameters);
//            _dspParameters

//            if (disposing)
//            {
//            }
//        }
//    }
//}
