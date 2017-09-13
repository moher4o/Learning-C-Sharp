using System;
using LambdaCore_Skeleton.Enums;
using LambdaCore_Skeleton.Interfaces;
using LambdaCore_Skeleton.Models.Fragments;

namespace LambdaCore_Skeleton.Commands
{
    public class AttachFragmentCommand : Command
    {
        public AttachFragmentCommand(string[] parameters) : base(parameters)
        {
        }

        public override string Execute()
        {
            IFragment fragment;
            FragmentType typeOfFragment;
            string name = this.Parameters[1];
            int pressureAffection = int.Parse(this.Parameters[2]);

            if (this.selectedCore.Name == "No")
            {
                return $"Failed to attach Fragment {name}!";
            }

            if ((Enum.TryParse(this.Parameters[0], out typeOfFragment)) && (0 <= pressureAffection))
            {
                fragment = new Fragment(typeOfFragment, name, pressureAffection);
            }
            else
            {
                return $"Failed to attach Fragment {name}!";
            }

            if (!this.aec.ContainsKey(this.selectedCore.Name))
            {
                return $"Failed to attach Fragment {name}!";
            }
            else
            {
                if (this.aec[this.selectedCore.Name].AddFragment(fragment))
                {
                    return $"Successfully attached Fragment {fragment.Name} to Core {this.selectedCore.Name}!";
                }

                return $"Failed to attach Fragment {name}!";

            }
            
        }
    }
}
