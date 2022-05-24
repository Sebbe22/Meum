using Meum.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        private static Enhed _enhed = null;
        private static EnhedKatalog _enhedDatabase = null;

        //[ClassInitialize]
        //public static void Classinitializer()
        //{

        //    //EnhedDatabase enhedData = new EnhedDatabase();

        //    //_enhed.Antal = 100;
        //    //_enhed.Navn = "Test Enhed";
        //    //_enhed.Pris = 50;

        //    //enhedData.AddEnhed(_enhed);

        //    //_enhedDatabase = new EnhedDatabase();
        //    //Enhed enhed  = _enhedDatabase.GetAllEnheder().Find(e => e.Navn == "Test Enhed");
        //    //_enhed.VareId = enhed.VareId;
        //}

        [TestInitialize]
        public void BeforeTest()
        {

            EnhedKatalog enhedData = new EnhedKatalog();

            _enhed = new Enhed();
            _enhed.Antal = 100;
            _enhed.Navn = "Test Enhed";
            _enhed.Pris = 50;

            enhedData.AddEnhed(_enhed);

            _enhedDatabase = new EnhedKatalog();
            Enhed enhed = _enhedDatabase.GetAllEnheder().Find(e => e.Navn.Contains("Test Enhed"));
            _enhed.VareId = enhed.VareId;
        }

        [TestMethod]
        public void TestMethod1UpdateEnhed()
        {
            Enhed UpEnhed = new Enhed(_enhed.VareId, "Test Enhed", 49, 90);


            _enhedDatabase.UpdateEnhed(_enhed.VareId, UpEnhed);

            Enhed actEnhed = _enhedDatabase.GetEnhedById(_enhed.VareId);

            Assert.AreEqual(UpEnhed.VareId, actEnhed.VareId);
            Assert.AreEqual(UpEnhed.Navn.Trim(), actEnhed.Navn.Trim());
            Assert.AreEqual(UpEnhed.Antal, actEnhed.Antal);
            Assert.AreEqual(UpEnhed.Pris, actEnhed.Pris);

            //_enhedDatabase.DeleteEnhedById(_enhed.VareId);
        }

        [TestMethod]
        public void TestMethod2DeleteEnhed()
        {
            Assert.IsNotNull(_enhedDatabase.GetAllEnheder().Find(e => e.VareId == _enhed.VareId));
            _enhedDatabase.DeleteEnhedById(_enhed.VareId);
            Assert.IsNull(_enhedDatabase.GetAllEnheder().Find(e => e.VareId == _enhed.VareId));
        }

        [TestMethod]
        public void TestMethod3AddEnhed()
        {
            Enhed testEnhed = new Enhed();

            testEnhed.Navn = "dette er test enhed";
            testEnhed.Antal = 500;
            testEnhed.Pris = 50;
            _enhedDatabase.AddEnhed(testEnhed);

            Assert.IsNotNull(_enhedDatabase.GetAllEnheder().Find(e => e.Navn.Trim() == "dette er test enhed"));

            //_enhedDatabase.DeleteEnhedById(_enhedDatabase.GetEnhedByName("dette er en test enhed").VareId);

        }

        //[ClassCleanup]
        //public void CleanupMethod()
        //{
        //    _enhedDatabase.DeleteEnhedById(_enhedDatabase.GetEnhedByName("dette er en test enhed").VareId);
        //    _enhedDatabase.DeleteEnhedById(_enhedDatabase.GetEnhedByName("Test Enhed").VareId);
        //}
    }
}
