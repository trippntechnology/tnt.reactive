namespace tnt.reactive;

public class StateFlow<T> : Flow<T>
{
	public T value { get => base.value; protected set => base.value = value; }

	public StateFlow(T value)
	{
		base.value = value;
	}
}