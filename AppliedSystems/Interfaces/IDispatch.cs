namespace AppliedSystems.Interfaces
{
    public interface IDispatch<in T>
    {
        void Dispatch(T instance);
    }
}