using FMOD;

namespace SupersonicSound.Exceptions
{
    public abstract class FmodPluginException
        : FmodException
    {
        protected FmodPluginException(RESULT fmodError)
            : base(fmodError)
        {
        }
    }

    public class FmodPluginUnspecifiedErrorException
        : FmodPluginException
    {
        public FmodPluginUnspecifiedErrorException()
            : base(RESULT.ERR_PLUGIN)
        {
        }
    }

    public class FmodPluginMissingException
        : FmodPluginException
    {
        public FmodPluginMissingException()
            : base(RESULT.ERR_PLUGIN_MISSING)
        {
        }
    }

    public class FmodPluginResourceException
        : FmodPluginException
    {
        public FmodPluginResourceException()
            : base(RESULT.ERR_PLUGIN_RESOURCE)
        {
        }
    }

    public class FmodPluginVersionException
        : FmodPluginException
    {
        public FmodPluginVersionException()
            : base(RESULT.ERR_PLUGIN_VERSION)
        {
        }
    }
}
