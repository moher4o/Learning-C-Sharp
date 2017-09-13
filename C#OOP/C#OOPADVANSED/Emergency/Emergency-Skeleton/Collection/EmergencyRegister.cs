using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Emergency_Skeleton.Interfaces;
using Emergency_Skeleton.Models;

namespace Emergency_Skeleton.Collection
{
    public class EmergencyRegister : IEmergencyRegister, IEnumerable<BaseEmergency>
    {

        private const int INITIAL_SIZE = 16;

        private BaseEmergency[] emergencyQueue;

        private int currentSize;

        private int nextIndex;

        public EmergencyRegister()
        {
            this.emergencyQueue = new BaseEmergency[INITIAL_SIZE];
            this.currentSize = 0;
            this.nextIndex = 0;
        }

        private void incrementNextIndex()
        {
            this.nextIndex++;
        }

        private void DecrementNextIndex()
        {
            this.nextIndex--;
        }

        private void IncrementCurrentSize()
        {
            this.currentSize++;
        }

        private void DecrementCurrentSize()
        {
            this.currentSize--;
        }

        private void CheckIfResizeNeeded()
        {
            if (this.currentSize == this.emergencyQueue.Length)
            {
                this.Resize();
            }
        }

        private void Resize()
        {
            BaseEmergency[] newArray = new BaseEmergency[2 * this.currentSize];

            for (int i = 0; i < this.currentSize; i++)
            {
                newArray[i] = this.emergencyQueue[i];
            }

            this.emergencyQueue = newArray;
        }

        public void EnqueueEmergency(BaseEmergency emergency)
        {
            this.CheckIfResizeNeeded();

            this.emergencyQueue[this.nextIndex] = emergency;
            this.incrementNextIndex();

            this.IncrementCurrentSize();
        }

        public BaseEmergency DequeueEmergency()
        {
            BaseEmergency removedElement = this.emergencyQueue[0];
            int length = 0;
            if (this.currentSize < this.emergencyQueue.Length)
            {
                length = this.currentSize;
            }
            else
            {
                length = this.currentSize - 1;
            }

            for (int i = 0; i < length; i++)
            {
                this.emergencyQueue[i] = this.emergencyQueue[i + 1];
            }

            this.DecrementNextIndex();
            this.DecrementCurrentSize();

            return removedElement;
        }

        public BaseEmergency PeekEmergency()
        {
            BaseEmergency peekedElement = this.emergencyQueue[0];
            return peekedElement;
        }

        public bool IsEmpty()
        {
            return this.currentSize == 0;
        }

        public int Count()
        {
            return this.nextIndex;
        }

        public int GetEmergencyOfType(Type type)
        {
            int counter = 0;
            for (int i = 0; i < this.emergencyQueue.Count(); i++)
            {
                if (this.emergencyQueue[i]!=null && this.emergencyQueue[i].GetType() == type)
                {
                    counter++;
                }
            }
            return counter;
        }

        public IEnumerator<BaseEmergency> GetEnumerator()
        {
            for (int i = 0; i < this.emergencyQueue.Length; i++)
            {
                if (this.emergencyQueue[i] != null)
                {
                    yield return this.emergencyQueue[i];
                }
              
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
