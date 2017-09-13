
using System;
using KingsGambit.Entities;

public class NameChangeEventArgs : EventArgs
{
   

    public NameChangeEventArgs(string name, King suveren)
    {
        this.Name = name;
        this.Suveren = suveren;
    }

    public string Name { get; set; }
    public King Suveren { get; set; }


}

