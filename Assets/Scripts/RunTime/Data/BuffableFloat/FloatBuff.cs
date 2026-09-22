using System.Collections.Generic;

namespace BuffableFloat
{
    public class FloatBuff
    {
        public readonly string name;
        public readonly float value;
        public readonly FloatBuffType buffType;

        public FloatBuff(string name, float value, FloatBuffType buffType)
        {
            this.name = name;
            this.value = value;
            this.buffType = buffType;
        }

        public float ApplyBuff(float buffedValue)
        {
            switch (buffType)
            {
                case FloatBuffType.Add:
                    buffedValue = buffedValue + value;
                    break;
                case FloatBuffType.Subtract:
                    buffedValue = buffedValue - value;
                    break;
                case FloatBuffType.Multiply:
                    buffedValue = buffedValue * value;
                    break;
                case FloatBuffType.Divide:
                    buffedValue = buffedValue / value;
                    break;
            }
            return buffedValue;
        }
    }

    public enum FloatBuffType
    {
        None, Add, Subtract, Multiply, Divide
    }
}

