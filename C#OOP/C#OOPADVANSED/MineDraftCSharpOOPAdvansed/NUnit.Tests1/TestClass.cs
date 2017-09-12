using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NUnit.Tests1
{
    [TestFixture]
    public class TestClass
    {
        private ProviderController providerController;
        private ProviderFactory factory;

        [SetUp]
        public void TestInitialized()
        {
            IEnergyRepository energyRepository = new EnergyRepository();
            this.providerController = new ProviderController(energyRepository);
            this.factory = new ProviderFactory();
        }

        [Test]
        public void TestRegisterMethodCount()
        {
            //Arrange
            IList<string> providerData = new List<string>(){ "Standart", "4", "20" };
            //Act
            this.providerController.Register(providerData);
            //Assert
            
            Assert.AreEqual(1,this.providerController.Entities.Count);
        }

        [Test]
        public void TestRegisterMethodEntity()
        {
            //Arrange
            IList<string> providerData = new List<string>() { "Solar", "4", "20" };
            IEntity provider = this.factory.GenerateProvider(providerData);
            //Act
            this.providerController.Register(providerData);
            //Assert

            Assert.AreEqual(provider.ID, this.providerController.Entities.FirstOrDefault(en => en.ID == 4).ID);
        }

        [Test]
        public void TestRegisterMethodReturnDataPressure()
        {
            //Arrange
            IList<string> providerData = new List<string>() { "Pressure", "4", "20" };
            
            //Act
            string result = this.providerController.Register(providerData);
            //Assert

            Assert.AreEqual("Successfully registered PressureProvider", result);
        }

        [Test]
        public void TestRepairReturn()
        {
            //Arrange
            IList<string> providerData = new List<string>() { "Pressure", "4", "50" };

            //Act
            this.providerController.Register(providerData);
            string result = this.providerController.Repair(20);
            //Assert

            Assert.AreEqual("Providers are repaired by 20", result,result);
        }

        [Test]
        public void TestProduceMethodReturn()
        {
            //Arrange
            IList<string> providerData = new List<string>() { "Pressure", "4", "50" };

            //Act
            this.providerController.Register(providerData);
            string result = this.providerController.Produce();
            //Assert

            Assert.AreEqual("Produced 100 energy today!", result, result);
        }

        [Test]
        public void TestProduceMethodUnitDamage()
        {
            //Arrange
            IList<string> providerData = new List<string>() { "Pressure", "4", "50" };

            //Act
            this.providerController.Register(providerData);
            for (int i = 0; i < 8; i++)
            {
                this.providerController.Produce();
            }
            string result = this.providerController.Produce();
            //Assert

            // Assert.AreEqual("Produced 0 energy today!", result, result);
            Assert.AreEqual(0, this.providerController.Entities.Count);
        }

    }
}
