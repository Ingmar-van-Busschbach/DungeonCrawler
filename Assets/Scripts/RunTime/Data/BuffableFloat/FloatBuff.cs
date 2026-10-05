namespace Buffables
{
    public struct FloatBuff
    {
        public readonly string name;
        public readonly float value;
        public readonly EFloatBuffType buffType;

        public FloatBuff(string name, float value, EFloatBuffType buffType)
        {
            this.name = name;
            this.value = value;
            this.buffType = buffType;
        }

        public float ApplyBuff(float buffedValue)
        {
            switch (buffType)
            {
                case EFloatBuffType.Add:
                    buffedValue = buffedValue + value;
                    break;
                case EFloatBuffType.Subtract:
                    buffedValue = buffedValue - value;
                    break;
                case EFloatBuffType.Multiply:
                    buffedValue = buffedValue * value;
                    break;
                case EFloatBuffType.Divide:
                    buffedValue = buffedValue / value;
                    break;
            }
            return buffedValue;
        }
    }
}

