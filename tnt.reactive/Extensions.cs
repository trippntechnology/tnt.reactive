namespace tnt.reactive;

/// <summary>
/// Flow extensions 
/// </summary>
public static class Extensions
{
  /// <summary>
  /// Converts a <see cref="Flow{T}"/> to a <see cref="StateFlow{T}"/>
  /// </summary>
  /// <returns>A <see cref="StateFlow{T}"/> that represents the <see cref="Flow{T}"/></returns>
  public static StateFlow<T?> asStateFlow<T>(this Flow<T> it)
  {
    StateFlow<T?> stateFlow = new StateFlow<T?>(default);
    it.collect(value =>
    {
      stateFlow.emit(value);
    });

    return stateFlow;
  }

  /// <summary>
  /// Combines two <see cref="Flow{T}"/> into a single <see cref="Flow{T}"/>
  /// </summary>
  /// <returns>New <see cref="Flow{T}"/> from the <paramref name="flow1"/> and <paramref name="flow2"/></returns>
  public static Flow<R> combine<F1, F2, R>(this Flow<F1> flow1, Flow<F2> flow2, Func<F1?, F2?, R> func)
  {
    F1? flowValue1 = default(F1);
    F2? flowValue2 = default(F2);
    Flow<R> flow = new Flow<R>();

    flow1.collect(value1 =>
    {
      flowValue1 = value1;
      flow.emit(func(value1, flowValue2));
    });
    flow2.collect(value2 =>
    {
      flowValue2 = value2;
      flow.emit(func(flowValue1, value2));
    });

    return flow;
  }
}
