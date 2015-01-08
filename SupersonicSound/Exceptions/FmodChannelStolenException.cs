using FMOD;

namespace SupersonicSound.Exceptions
{
    /// <summary>
    /// DMA Failure.  See debug output for more information.
    /// </summary>
    public class FmodDmaException
        : FmodException
    {
        public FmodDmaException()
            : base(RESULT.ERR_DMA)
        {
        }
    }
}
