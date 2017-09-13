using System.Collections.Generic;
using LambdaCore_Skeleton.Attributes;
using LambdaCore_Skeleton.Interfaces;

namespace LambdaCore_Skeleton.Commands
{
   public abstract class Command : ICommandType
    {
        [Inject]
        protected IDictionary<string, ICore> aec;
        [Inject]
        protected ICore selectedCore;


        private string[] parameters;

        protected Command(string[] parameters)
        {
            this.Parameters = parameters;
        }
        public string[] Parameters
        {
            get { return this.parameters; }
            protected set { this.parameters = value; }
        }
        public abstract string Execute();
    }
}
