namespace tnt.reactive;

/// <summary>
/// A <see cref="StateFlow{T}"/> that is mutable
/// </summary>
public class MutableStateFlow<T> : StateFlow<T>
{
  /// <summary>
  /// Value of the <see cref="MutableStateFlow{T}"/>
  /// </summary>
  public new T value { get => base.value; set => emit(value); }

  /// <summary>
  /// Initialization constructor
  /// </summary>
  /// <param name="initialValue">Initial value of the <see cref="MutableStateFlow{T}"/></param>
  public MutableStateFlow(T initialValue) : base(initialValue) { }
}