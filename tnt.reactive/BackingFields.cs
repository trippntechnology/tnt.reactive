using System.Runtime.CompilerServices;

namespace TNT.Reactive;

/// <summary>
/// Encapsolates a <see cref="Dictionary{TKey, TValue}"/> representing property name and values. When
/// a property name value changes <see cref="OnFieldChanged"/> is called with the corresponding property name
/// and value.
/// </summary>
public class BackingFields
{
  /// <summary>
  /// Called when a value within <see cref="BackingFields"/> changes
  /// </summary>
  public Action<string, object?> OnFieldChanged = (_, __) => { };

  /// <summary>
  /// Represents property names and values
  /// </summary>
  protected Dictionary<string, object?> _BackingFields = new();

  /// <summary>
  /// Default constructor
  /// </summary>
  public BackingFields() { }

  /// <summary>
  /// Copy constructor
  /// </summary>
  public BackingFields(BackingFields backingFields)
  {
    backingFields._BackingFields.Keys.ToList().ForEach(key => _BackingFields[key] = backingFields._BackingFields[key]);
  }

  /// <summary>
  /// Sets the property <paramref name="value"/> associated with the <paramref name="propertyName"/>.
  /// The CallerMemberName attribute is used to automatically set the <paramref name="propertyName"/> from 
  /// the calling method.
  /// </summary>
  public void Set<T>(T? value, [CallerMemberName] string propertyName = "")
  {
    var valueChanged = false;

    if (_BackingFields.TryGetValue(propertyName, out object? currentValue))
    {
      valueChanged = currentValue?.Equals(value) != true;
    }
    else
    {
      valueChanged = true;
    }

    if (valueChanged)
    {
      _BackingFields[propertyName] = value;
      OnFieldChanged(propertyName, value);
    }
  }

  /// <summary>
  /// Gets the value associated with the <paramref name="propertyName"/> if exists, otherwise, returns 
  /// <paramref name="defaultValue"/>. The CallerMemberName attribute is used to automatically set the 
  /// <paramref name="propertyName"/> from the calling method.
  /// </summary>
  public T? Get<T>(T? defaultValue = default, [CallerMemberName] string propertyName = "")
  {
    return _BackingFields.ContainsKey(propertyName) ? (T?)_BackingFields[propertyName] : defaultValue;
  }
}
