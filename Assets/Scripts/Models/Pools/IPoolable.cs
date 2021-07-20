namespace Models.Pools
{
    public interface IPoolable
    {
        void Activate();
        void Deactivate();
    }
}