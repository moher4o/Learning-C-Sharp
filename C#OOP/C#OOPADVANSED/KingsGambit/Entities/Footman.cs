using System.Runtime.CompilerServices;
using KingsGambit.Interfaces;
using KingsGambit.IO;

namespace KingsGambit.Entities
{
    public delegate void WoundChangeEventHandler(object sender, NameChangeEventArgs e);
    public class Footman : IObserver
   {
       private IWriter writer;
       private int numberOfWounds = 0;
       private King suveren;

       public Footman(string name, King suveren)
       {
           this.writer = new ConsoleWriter();
           this.Name = name;
           this.suveren = suveren;
       }
        public string Name { get; private set; }

       public int NumberOfWounds
       {
           get { return this.numberOfWounds; }
           private set
           {
               this.numberOfWounds++;
                   OnWoundChange();
           }
       }

       public event WoundChangeEventHandler WoundChange;

       public void OnWoundChange()
       {
           if (this.numberOfWounds == 2)
           {
               if (WoundChange != null)
               {
                   WoundChange(this, new NameChangeEventArgs(this.Name, this.suveren));
               }
            }
        }

        public void Update()
        {
            this.writer.WriteLine($"Footman {this.Name} is panicking!");
        }

       public void HitObserver()
       {
           this.NumberOfWounds++;
       }
   }
}
