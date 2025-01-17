namespace tnt.reactive;

/// <summary>
/// Object that can be observed for changes that are emitted
/// </summary>
public class Flow<T>
{
  /// <summary>
  /// <see cref="List{T}"/> of <see cref="Action"/> that are called when the <see cref="Flow{T}"/>
  /// emits a change
  /// </summary>
  protected List<Action<T?>> emitters = new List<Action<T?>>();

  /// <summary>
  /// Called to emit a new value
  /// </summary>
  /// <param name="value">Value to emit</param>
  public void emit(T value)
  {
    emitters.ForEach(emitter => emitter(value));
  }

  /// <summary>
  /// Called to observe values being emitted by the <see cref="Flow{T}"/>
  /// </summary>
  /// <param name="onEmit">Lambda that is called by the <see cref="Flow{T}"/></param>
  public virtual void collect(Action<T?> onEmit)
  {
    this.emitters.Add(onEmit);
  }

  /// <summary>
  /// Maps a <see cref="Flow{T}"/> to another <see cref="Flow{T}"/>
  /// </summary>
  /// <param name="func"><see cref="Func{TResult}"/> that is returns a new value</param>
  /// <returns>A new <see cref="Flow{T}"/> based on the value of this <see cref="Flow{T}"/></returns>
  public virtual Flow<R?> map<R>(Func<T?, R?> func)
  {
    Flow<R?> flow = new Flow<R?>();
    collect((value) =>
    {
      R? result = func(value);
      flow.emit(result);
    });

    return flow;
  }
}