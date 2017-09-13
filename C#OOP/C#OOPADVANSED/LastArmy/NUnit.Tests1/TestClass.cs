using NUnit.Framework;
using System;

[TestFixture]
public class TestClass
{
    private MissionController missionController;

    [SetUp]
    public void TestInitialized()
    {
        IArmy army = new Army();
        army.AddSoldier(new Corporal("Pepo", 12, 23, 2));
        army.AddSoldier(new Corporal("Ricky", 29, 29, 20));
        IWareHouse wareHouse = new WareHouse();
        //wareHouse.WeaponAvailable["Knife"] = 12;
        //wareHouse.WeaponAvailable["Gun"] = 12;
        //wareHouse.WeaponAvailable["AutomaticMachine"] = 12;
        //wareHouse.WeaponAvailable["Helmet"] = 12;
        //wareHouse.WeaponAvailable["MachineGun"] = 12;
        //wareHouse.WeaponAvailable["NightVision"] = 12;
        //wareHouse.WeaponAvailable["RPG"] = 12;
        this.missionController = new MissionController(army, wareHouse);
    }

    [Test]
    public void TestMissionControlerMissionQueueInitialization()
    {
        //Assert
        Assert.AreEqual(0, this.missionController.Missions.Count);
    }

 
    [Test]
    public void TestMissionControlerFailedMissionCounterInitialization()
    {
        //Assert
        Assert.AreEqual(0, this.missionController.FailedMissionCounter);
    }

    public void TestMissionControlerSuccessMissionCounterInitialization()
    {
        //Assert
        Assert.AreEqual(0, this.missionController.SuccessMissionCounter);
    }


    [Test]
    public void TestMissionControlerCountTestQueue()
    {
        //Arrange
        this.missionController.Missions.Enqueue(new Easy(23));
        this.missionController.Missions.Enqueue(new Easy(140));
        this.missionController.Missions.Enqueue(new Easy(34));
        //Act
        this.missionController.PerformMission(new Hard(78));
        //Assert
        Assert.AreEqual(3, this.missionController.Missions.Count);
    }

    [Test]
    public void TestMissionControlerFailedMissionCounter()
    {
        //Arrange
        this.missionController.Missions.Enqueue(new Easy(23));
        this.missionController.Missions.Enqueue(new Easy(140));
        this.missionController.Missions.Enqueue(new Easy(34));
        //Act
        this.missionController.FailMissionsOnHold();
        //Assert
        Assert.AreEqual(3, this.missionController.FailedMissionCounter);
    }

    [Test]
    public void TestPerformMissionEnqueMissions()
    {

        this.missionController.PerformMission(new Easy(23));
        this.missionController.PerformMission(new Easy(23));
        this.missionController.PerformMission(new Easy(23));
        this.missionController.PerformMission(new Easy(23));

        Assert.AreEqual(3, this.missionController.Missions.Count);
    }

    [Test]
    public void TestPerformMissionMethodOnHold()
    {

        string res = this.missionController.PerformMission(new Medium(23));
        string result = "Mission on hold - Capturing dangerous criminals" + Environment.NewLine;

        Assert.AreEqual(1, this.missionController.Missions.Count);
        //Assert.AreEqual(result, res);
    }

    [Test]
    public void TestPerformMissionMethodCompleted()
    {
        string res = this.missionController.PerformMission(new Easy(23));
        string result = "Mission completed - Suppression of civil rebellion" + Environment.NewLine;

        Assert.AreEqual(0,this.missionController.SuccessMissionCounter);
        //Assert.AreEqual(result, res);
    }


}

