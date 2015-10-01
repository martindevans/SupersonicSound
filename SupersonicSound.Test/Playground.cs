using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupersonicSound.LowLevel;
using SupersonicSound.Wrapper;

namespace SupersonicSound.Test
{
    [TestClass]
    public class Playground
    {
        private void TestInitSystem(IPreInitilizeLowLevelSystem sys)
        {
            sys.Output = OutputMode.NoSoundNotRealtime;
        }

        [TestMethod]
        public void CreateSystem()
        {
            using (var sys = new LowLevelSystem(preInit: TestInitSystem))
            {
            }
        }
    }
}
