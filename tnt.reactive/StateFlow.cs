namespace tnt.reactive;

public class StateFlow<T> : Flow<T>
{
  public T value { get; protected set; }

  public StateFlow(T initialValue) : base()
  {
    value = initialValue;
    collect((value) => this.value = value);
  }
}