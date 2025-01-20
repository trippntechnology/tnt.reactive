namespace TNT.Reactive;

/// <summary>
/// <see cref="Flow{T}"/> that maintains state of last emitted value
/// </summary>
public class StateFlow<T> : Flow<T>
{
  /// <summary>
  /// Keeps state of last emitted value
  /// </summary>
  public T? value { get; protected set; }

  /// <summary>
  /// Initialization constructor
  /// </summary>
  /// <param name="initialValue">Initial <see cref="StateFlow{T}"/> value</param>
  public StateFlow(T initialValue) : base()
  {
    value = initialValue;
    collect((value) => this.value = value);
  }

  /// <summary>
  /// Called to observe values being emitted by the <see cref="Flow{T}"/> and emits the <see cref="value"/>
  /// </summary>
  /// <param name="onEmit">Lambda that is called by the <see cref="Flow{T}"/></param>
  public override void collect(Action<T?> onEmit)
  {
    base.collect(onEmit);
    onEmit(this.value);
  }
}