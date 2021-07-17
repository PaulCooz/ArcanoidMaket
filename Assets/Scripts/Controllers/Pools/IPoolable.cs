namespace Controllers.Pools
{
    public interface IPoolable
    {
        void Activate();
        void Deactivate();
    }
}