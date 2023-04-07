namespace tnt.reactive;

public static class FlowHelpers
{
	public static Flow<R> combine<A, B, R>(Flow<A> flow1, Flow<B> flow2, Func<A, B, R> func)
	{
		var resultFlow = new MutableStateFlow<R>(default(R));

		flow1.collect(value1 => { resultFlow.value = func(value1, flow2.value); });
		flow2.collect(value2 => { resultFlow.value = func(flow1.value, value2); });

		return resultFlow;
	}
}
