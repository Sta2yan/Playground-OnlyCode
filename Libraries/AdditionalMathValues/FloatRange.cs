namespace Agava.AdditionalMathValues
{
    public readonly struct FloatRange
    {
        public FloatRange(float minimum, float maximum)
        {
            Min = minimum;
            Max = maximum;
        }
        
        public float Max { get; }
        public float Min { get; }
    }
}
