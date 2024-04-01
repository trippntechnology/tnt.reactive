namespace tnt.reactive;

public class StateFlow<T> : Flow<T>
{
  protected T value;

  public StateFlow(T initialValue) : base()
  {
    value = initialValue;
    collect((value) => this.value = value);
  }
}