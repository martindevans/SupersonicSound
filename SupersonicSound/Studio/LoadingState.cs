using FMOD.Studio;

namespace SupersonicSound.Studio
{
    [EquivalentEnum(typeof(LOADING_STATE))]
    public enum LoadingState
    {
        Unloading = LOADING_STATE.UNLOADING,
        Unloaded = LOADING_STATE.UNLOADED,
        Loading = LOADING_STATE.LOADING,
        Loaded = LOADING_STATE.LOADED
    }
}
