using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodFileException
        : FmodException
    {
        public string FileName { get; private set; }

        public FmodFileException(RESULT fmodError, string fileName)
            : base(fmodError)
        {
            FileName = fileName;
        }
    }

    public class FmodFileNotFoundException
        : FmodFileException
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
        : FmodFileException
    {
        public FmodEndOfDataException(string fileName = null)
            : base(RESULT.ERR_FILE_ENDOFDATA, fileName)
        {
        }
    }

    public class FmodBadFileException
        : FmodFileException
    {
        public FmodBadFileException(string fileName = null)
            : base(RESULT.ERR_FILE_BAD, fileName)
        {
        }
    }

    public class FmodCouldNotSeekException
        : FmodFileException
    {
        public FmodCouldNotSeekException(string fileName = null)
            : base(RESULT.ERR_FILE_BAD, fileName)
        {
        }
    }

    public class FmodDiskEjectedException
        : FmodFileException
    {
        public FmodDiskEjectedException(string fileName = null)
            : base(RESULT.ERR_FILE_BAD, fileName)
        {
        }
    }
}
