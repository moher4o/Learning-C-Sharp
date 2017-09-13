using System.Collections.Generic;
using System.Linq;
using KingsGambit.Interfaces;
using KingsGambit.IO;

namespace KingsGambit.Entities
{
    public class King : ISubject
    {
        private List<IObserver> defenders;
        private IWriter writer;

        public King()
        {
            this.defenders = new List<IObserver>();
            this.writer = new ConsoleWriter();
        }

        public King(string name) : this()
        {
            this.Name = name;
        }
        public string Name { get; set; }

        public void Register(IObserver observer)
        {
            this.defenders.Add(observer);
        }

        public void Kill(string observer)
        {
            this.defenders.FirstOrDefault(d => d.Name == observer).HitObserver();
            
        }

        public void Remove(string soldier)
        {
            this.defenders.Remove(this.defenders.FirstOrDefault(d => d.Name == soldier));
        }

        public void Attack()
        {
            this.writer.WriteLine($"King {this.Name} is under attack!");
            foreach (var defender in this.defenders)
            {
                defender.Update();
            }
        }
    }
}
