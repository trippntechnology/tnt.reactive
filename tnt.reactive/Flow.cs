namespace tnt.reactive;

public abstract class Flow<T>
{
	protected T _value;
	protected Action<T>? _action;

	public T value
	{
		get => _value;
		protected set
		{
			_value = value;
			_action?.Invoke(_value);
		}
	}

	public void collect(Action<T> action)
	{
		_action = action;
	}
}