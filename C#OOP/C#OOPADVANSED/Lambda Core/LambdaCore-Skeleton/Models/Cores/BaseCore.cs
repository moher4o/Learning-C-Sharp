using System.Collections.Generic;
using System.Linq;
using LambdaCore_Skeleton.Interfaces;

namespace LambdaCore_Skeleton.Models.Cores
{
    public abstract class BaseCore : ICore
    {
        private List<IFragment> fragments;
        private string name;
        private double durability;

        protected BaseCore(string name, double durability)
        {
            this.fragments = new List<IFragment>();
            this.Name = name;
            this.Durability = durability;
        }

        public double Durability
        {
            get
            {
                return this.durability - (this.Fragments.Sum(f => f.PressureAffection) <= 0 ? 0 : this.Fragments.Sum(f => f.PressureAffection));
            }
            protected set { this.durability = value; }
        }


        public string Name
        {
            get { return this.name; }
            protected set { this.name = value; }
        }

        public IReadOnlyList<IFragment> Fragments => this.fragments.AsReadOnly();

        public bool AddFragment(IFragment fragment)
        {
            this.fragments.Add(fragment);
            if (this.CheckDurability())
            {
                return true;
            }
            else
            {
                this.fragments.Remove(fragment);
                return false;
            }
            
        }

        private bool CheckDurability()
        {
            if (this.fragments.Sum(f => f.PressureAffection) > this.Durability)
            {
                return false;
            }
            return true;
        }

        public string RemoveFragment()
        {
            if (this.fragments.Count > 0)
            {
                // IFragment fragment =  this.fragments.RemoveAt(this.fragments.Count-1);
                IFragment fragment = this.fragments.LastOrDefault();
                this.fragments.Remove(fragment);
                return fragment.Name;
            }
            else
            {
                return null;
            }
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }
    }
}
