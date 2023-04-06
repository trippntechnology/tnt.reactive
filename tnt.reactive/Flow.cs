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

	public static Flow<R> combine<A, B, R>(Flow<A> flow1, Flow<B> flow2, Func<A, B, R> func)
	{
		var resultFlow = new MutableStateFlow<R>(default(R));

		flow1.collect(value1 => { resultFlow.value = func(value1, flow2.value); });
		flow2.collect(value2 => { resultFlow.value = func(flow1.value, value2); });

		return resultFlow;
	}
}