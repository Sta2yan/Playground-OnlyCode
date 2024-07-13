namespace Agava.Playground3D.Items
{
    public interface IPick : IItem
    {
        public float DigDelay { get; }
        public int Damage { get; }
    }
}
