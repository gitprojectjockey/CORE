using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

namespace MSTestFundamentals
{
    [TestClass]
    public class UnitTest1
    {
        [AssemblyInitialize]
        public static void BeforeAssembly(TestContext testContext)
        {
            WriteLine("This is from the static BeforeAssembly() (AssemblyInitialize Attibute)");
        }

        [ClassInitialize]
        public static void BeforeAllTests(TestContext textContext)
        {
            WriteLine("This is from the static BeforeAllTests() (ClassInitialize Attibute)");
        }

        [TestInitialize]
        public void BeforeEachTest()
        {
            WriteLine("This is from BeforeEachTest() (TestInitialize Attibute)");
        }

        [TestMethod]
        public void TestMethod1()
        {
            WriteLine("This is from TestMethod1()  (TestMethod Attibute)");
        }

        [TestMethod]
        public void TestMethod2()
        {
            WriteLine("This is from TestMethod2()  (TestMethod Attibute)");
        }

        [TestMethod]
        public void TestMethod3()
        {
            WriteLine("This is from TestMethod3()  (TestMethod Attibute)");
        }

        [TestCleanup]
        public void AfterEachTest()
        {
            WriteLine("This is from AfterEachTest() (TestCleanup Attibute)");
        }

        [ClassCleanup]
        public static void AfterAllTests()
        {
            WriteLine("This is from the static AfterAllTests() (ClassCleanup Attibute)");
        }

        [AssemblyCleanup]
        public static void AfterAssembly()
        {
            WriteLine("This is from the static AfterAssembly() (AssemblyCleanup Attibute)");
        }
    }
}
