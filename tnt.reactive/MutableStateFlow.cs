namespace tnt.reactive;

public class MutableStateFlow<T> : StateFlow<T>
{
  public new T value { get => base.value; set => emit(value); }

  public MutableStateFlow(T initialValue) : base(initialValue) { }
}