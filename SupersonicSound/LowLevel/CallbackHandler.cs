using FMOD;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.LowLevel
{
    class CallbackHandler
    {
        private readonly ChannelControl _fmod;

        // This field exists to keep the registered callback alive as long as necessary
        // ReSharper disable NotAccessedField.Local
        private object _callbackHandle;
        // ReSharper restore NotAccessedField.Local

        public CallbackHandler(ChannelControl fmod)
        {
            _fmod = fmod;
        }

        public void SetCallback(Action<ChannelControlCallbackType, IntPtr, IntPtr> callback)
        {
            //Remove previous callback
            RemoveCallback();

            //Passing in null to set removes any existing callbacks
            if (callback == null)
                return;

            //Keep a reference to the callback handler
            //Create a callback which wraps the actual callback
            //This will clean itself up when the "end" event happens
            var callbackFunction = new CHANNEL_CALLBACK((channelraw, controltype, type, commanddata1, commanddata2) =>
            {
                //Call the real callback
                callback((ChannelControlCallbackType)type, commanddata1, commanddata2);

                //Clean up as necessary
                if (type == CHANNELCONTROL_CALLBACK_TYPE.END)
                {
                    // End of sound, we can release our callback handle now
                    _callbackHandle = null;
                }

                return RESULT.OK;
            });

            //Set the callback into FMOD
            _fmod.setCallback(callbackFunction).Check();

            // Hold the delegate object in memory
            _callbackHandle = callbackFunction;
        }

        public void RemoveCallback()
        {
            //Unset callback in FMOD
            _fmod.setCallback(null).Check();

            //Drop the reference to the old callback
            _callbackHandle = null;
        }
    }
}
