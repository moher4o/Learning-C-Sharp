using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaCore_Skeleton.Collection
{
    using LambdaCore_Skeleton.Models.Cores;

    class LStack
    {
        private LinkedList<SystemCore> innerList;

        public LStack()
        {
            this.innerList = new LinkedList<SystemCore>();
        }

        public int Count()
        {
            return this.innerList.Count;
        }

        public SystemCore Push(SystemCore item)
        {
            this.innerList.AddLast(item);
            return item;
        }

        public void Pop()
        {
            this.innerList.RemoveLast();
        }

        public SystemCore Peek()
        {
            SystemCore peekedItem = this.innerList.First();
            return peekedItem;
        }

        public Boolean IsEmpty()
        {
            return this.innerList.Count > 0;
        }
    }
}
