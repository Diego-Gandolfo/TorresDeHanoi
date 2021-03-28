public interface IStackFILO<Type>
{
    public void Initialize();

    public void Push(Type item);

    public Type Pop();

    public Type Peek();

    public bool IsEmpty();
}
