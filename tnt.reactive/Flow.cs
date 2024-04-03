namespace tnt.reactive;

public class Flow<T>
{
  protected List<Action<T>> emitters = new List<Action<T>>();

  public void emit(T value)
  {
    emitters.ForEach(emitter => emitter(value));
  }

  public virtual void collect(Action<T> onEmit)
  {
    this.emitters.Add(onEmit);
  }

  public virtual Flow<R> map<R>(Func<T, R> func)
  {
    Flow<R> flow = new Flow<R>();
    collect((value) =>
    {
      R? result = func(value);
      flow.emit(result);
    });

    return flow;
  }
}