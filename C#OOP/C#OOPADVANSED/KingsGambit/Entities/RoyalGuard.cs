using KingsGambit.Interfaces;
using KingsGambit.IO;

namespace KingsGambit.Entities
{
    public class RoyalGuard : IObserver
    {
        private IWriter writer;
        private int numberOfWounds = 0;
        private King suveren;


        public RoyalGuard(string name, King suveren)
        {
            this.writer = new ConsoleWriter();
            this.Name = name;
            this.suveren = suveren;
        }
        public string Name { get; private set; }
        public int NumberOfWounds {
            get { return this.numberOfWounds; }
            private set
            {
                this.numberOfWounds++;
                OnWoundChange();
            }
        }

        private void OnWoundChange()
        {
            if (this.numberOfWounds == 3)
            {
                if (WoundChange != null)
                {
                    WoundChange(this, new NameChangeEventArgs(this.Name, this.suveren));
                }
            }
        }

        public event WoundChangeEventHandler WoundChange;

        public void Update()
        {
            this.writer.WriteLine($"Royal Guard {this.Name} is defending!");
        }

        public void HitObserver()
        {
            this.NumberOfWounds++;
        }
    }
}
