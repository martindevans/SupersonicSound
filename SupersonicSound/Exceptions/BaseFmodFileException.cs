using FMOD;

namespace SupersonicSound.Exceptions
{
    public abstract class BaseFmodFileException
        : FmodException
    {
        public string FileName { get; private set; }

        public BaseFmodFileException(RESULT fmodError, string fileName)
            : base(fmodError)
        {
            FileName = fileName;
        }
    }

    public class FmodFileNotFoundException
        : BaseFmodFileException
    {
        public FmodFileNotFoundException(string fileName = null)
            : base(RESULT.ERR_FILE_NOTFOUND, fileName)
        {
        }
    }

    public class FmodEndOfFileException
        : FmodException
    {
        public FmodEndOfFileException()
            : base(RESULT.ERR_FILE_EOF)
        {
        }
    }

    public class FmodEndOfDataException
        : BaseFmodFileException
    {
        public FmodEndOfDataException(string fileName = null)
            : base(RESULT.ERR_FILE_ENDOFDATA, fileName)
        {
        }
    }

    public class FmodBadFileException
        : BaseFmodFileException
    {
        public FmodBadFileException(string fileName = null)
            : base(RESULT.ERR_FILE_BAD, fileName)
        {
        }
    }

    public class FmodCouldNotSeekException
        : BaseFmodFileException
    {
        public FmodCouldNotSeekException(string fileName = null)
            : base(RESULT.ERR_FILE_BAD, fileName)
        {
        }
    }

    public class FmodDiskEjectedException
        : BaseFmodFileException
    {
        public FmodDiskEjectedException(string fileName = null)
            : base(RESULT.ERR_FILE_BAD, fileName)
        {
        }
    }
}
