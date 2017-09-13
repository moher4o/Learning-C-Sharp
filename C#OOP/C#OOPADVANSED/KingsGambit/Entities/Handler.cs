
   using System;
   using KingsGambit.Entities;

public class Handler
    {
        public void OnWoundCountChange(object sender, NameChangeEventArgs args)
        {
            King king = args.Suveren;
            string nameOfSoldier = args.Name;
            king.Remove(nameOfSoldier);
        }
    }

