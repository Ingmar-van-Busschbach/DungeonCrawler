using System.Collections.Generic;

namespace BuffableFloat
{
    public class BuffableFloat
    {
        private float value;
        private List<FloatBuff> buffs;

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
        }
    }
}

