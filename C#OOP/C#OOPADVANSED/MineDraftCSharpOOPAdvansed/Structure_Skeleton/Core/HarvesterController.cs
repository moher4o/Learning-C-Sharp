using System;
using System.Collections.Generic;
using System.Linq;

public class HarvesterController : IHarvesterController
{
    private List<IHarvester> harvesters;
    private IHarvesterFactory factory;
    private double oreProduced;
    private string mode;

    public HarvesterController()
    {
        this.harvesters = new List<IHarvester>();
        this.factory = new HarvesterFactory();
        this.OreProduced = 0d;
        this.Mode = "Full";
    }

    public IReadOnlyCollection<IHarvester> Entities => this.harvesters.AsReadOnly();

    public double TotalEnergyRequirement
    {
        get
        {
            double energyNeeded = 0d;
            switch (this.Mode)
            {
                case "Full":
                    energyNeeded = this.harvesters.Sum(h => h.EnergyRequirement);
                    break;
                case "Half":
                    energyNeeded = this.harvesters.Sum(h => h.EnergyRequirement) * 50 / 100;
                    break;
                case "Energy":
                    energyNeeded = this.harvesters.Sum(h => h.EnergyRequirement) * 20 / 100;
                    break;
            }
            return energyNeeded;
        }
    }

    public double OreProduced
    {
        get { return this.oreProduced; }
        private set { this.oreProduced = value; }
    }


    public string Mode
    {
        get { return this.mode; }
        private set { this.mode = value; }
    }

    public string Register(IList<string> args)
    {
        var harvester = this.factory.GenerateHarvester(args);
        this.harvesters.Add(harvester);

        return string.Format(Constants.SuccessfullRegistration,
            harvester.GetType().Name);
    }

    public string Produce()
    {
        double currentOre = 0d;
        switch (this.Mode)
        {
            case "Full":
                currentOre = this.harvesters.Sum(h => h.Produce());
                break;
            case "Half":
                currentOre = this.harvesters.Sum(h => h.Produce()) * 50 / 100;
                break;
            case "Energy":
                currentOre = this.harvesters.Sum(h => h.Produce()) * 20 / 100;
                break;
        }
        this.OreProduced += currentOre;

        return string.Format(Constants.OreProducedToday, currentOre);
    }


    public string ChangeMode(string modeActiv)
    {
        this.Mode = modeActiv;
        List<IHarvester> reminder = new List<IHarvester>();
        foreach (IHarvester harvester in this.harvesters)
        {
            try
            {
                harvester.Broke();
            }
            catch (Exception)
            {
                reminder.Add(harvester);

            }
        }
        foreach (IHarvester harvester in reminder)
        {
            this.harvesters.Remove(harvester);
        }

        return string.Format(Constants.ModeChangedTo,
            mode);
    }
}

