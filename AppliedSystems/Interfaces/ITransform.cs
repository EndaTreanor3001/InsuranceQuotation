namespace AppliedSystems.Interfaces
{
    public interface ITransform<in T, out TU>
    {
        TU Transform(T instance);
    }
}