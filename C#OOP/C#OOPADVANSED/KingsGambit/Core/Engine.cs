using System;
using KingsGambit.Entities;
using KingsGambit.Interfaces;

namespace KingsGambit.Core
{
   public class Engine
   {
       private ICommandInterpreter commandInterpreter;
       Handler handler = new Handler();
        private IReader reader;
       private IWriter writer;

        public Engine(ICommandInterpreter commandInterpreter, IReader reader, IWriter writer)
       {
           this.commandInterpreter = commandInterpreter;
           this.reader = reader;
           this.writer = writer;
       }

       public void Run()
        {
            var kingName = this.reader.ReadLine();
            var kingGuards = this.reader.ReadLine().Split();
            var kingFootmens = this.reader.ReadLine().Split();

            King king = new King(kingName);
            foreach (var guard in kingGuards)
            {
                IObserver kingGuard = new RoyalGuard(guard, king);
                kingGuard.WoundChange += this.handler.OnWoundCountChange;
                king.Register(kingGuard);
            }
            foreach (var footmen in kingFootmens)
            {
                IObserver kingFootman = new Footman(footmen, king);
                kingFootman.WoundChange += this.handler.OnWoundCountChange;
                king.Register(kingFootman);
            }

            this.commandInterpreter.King = king;
            var input = string.Empty;
            while ((input = this.reader.ReadLine())!="End")
            {
                try
                {
                    var commandTokens = input.Split();
                    string command = commandTokens[0];
                    string name = commandTokens[1];
                    if (name != "King")
                    {
                        this.commandInterpreter.Name = name;
                    }
                    this.commandInterpreter.InterpretCommand(command).Execute();
                }
                catch (Exception e)
                {
                    this.writer.WriteLine(e.Message);
                }
            }


        }
    }
}
