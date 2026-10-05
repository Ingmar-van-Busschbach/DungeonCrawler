using System;
using System.Collections.Generic;

namespace Buffables
{
    [Serializable]
    public class BuffableFloat
    {
        private readonly float value;
        private List<FloatBuff> buffs = new();
        public Action<FloatBuff> onBuffAdded;
        public Action<FloatBuff> onBuffRemoved;

        public float Value
        {
            get
            {
                float returnValue = value;
                foreach(FloatBuff buff in buffs)
                {
                    returnValue = buff.ApplyBuff(returnValue);
                }
                return returnValue;
            }
            private set
            {
            }
        }

        public BuffableFloat(float startingValue)
        {
            value = startingValue;
        }

        public void AddBuff(FloatBuff buff)
        {
            buffs.Add(buff);
            onBuffAdded?.Invoke(buff);
        }

        public void RemoveBuff(FloatBuff buff)
        {
           buffs.Remove(buff);
           onBuffRemoved?.Invoke(buff);
        }

        public void RemoveBuffByName(string name)
        {
            foreach (FloatBuff buff in buffs)
            {
                if(buff.name == name)
                {
                    buffs.Remove(buff);
                    onBuffRemoved?.Invoke(buff);
                }
            }
        }
    }
}

