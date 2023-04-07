namespace tnt.reactive;

public abstract class Flow<T>
{
	protected T _value;
	protected List<Action<T>> _action = new List<Action<T>>();

	public T value
	{
		get => _value;
		protected set
		{
			_value = value;
			_action.ForEach(_action => _action(value));
		}
	}

	public void collect(Action<T> action)
	{
		_action.Add(action);
	}

	public Flow<R> map<R>(Func<T, R> func)
	{
		var resultFlow = new MutableStateFlow<R>(default(R));
		collect((value) =>
		{
			resultFlow.value = func(value);
		});

		return resultFlow;
	}
}