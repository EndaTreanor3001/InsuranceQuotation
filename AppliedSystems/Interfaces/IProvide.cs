namespace AppliedSystems.Interfaces
{
    public interface IProvide<out T>
    {
        T Get();
    }
}