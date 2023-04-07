namespace tnt.reactive;

public class MutableStateFlow<T> : StateFlow<T>
{

	public T value { get => base.value; set => base.value = value; }

	public MutableStateFlow(T initialValue) : base(initialValue) { }
}