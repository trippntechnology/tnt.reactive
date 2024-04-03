namespace tnt.reactive;

public class StateFlow<T> : Flow<T>
{
  public T value { get; protected set; }

  public StateFlow(T initialValue) : base()
  {
    value = initialValue;
    collect((value) => this.value = value);
  }

  public override void collect(Action<T> onEmit)
  {
    base.collect(onEmit);
    onEmit(this.value);
  }

  public override Flow<R> map<R>(Func<T, R> func)
  {
    try
    {
      return base.map(func);
    }
    finally
    {
      //emit(value);
    }
  }
}