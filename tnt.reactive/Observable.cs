using System.Runtime.CompilerServices;

namespace TNT.Reactive;

/// <summary>
/// Represents an abstract base class for observable objects. It provides mechanisms to track and notify
/// changes to property values using a backing field system.
/// </summary>
public abstract class Observable
{
  /// <summary>
  /// Encapsulates the backing fields for property values and handles change notifications.
  /// </summary>
  private BackingFields _BackingFields = new BackingFields();

  /// <summary>
  /// An action invoked whenever a field value changes. The action provides the property name and its new value.
  /// </summary>
  public event Action<string, object?> OnPropertyChanged = (propertyName, value) => { };

  /// <summary>
  /// Sets the value of a property and triggers the change notification if the value changes.
  /// </summary>
  /// <typeparam name="T">The type of the property value.</typeparam>
  /// <param name="value">The new value to set for the property.</param>
  /// <param name="propertyName">The name of the property. Automatically provided by the CallerMemberName attribute.</param>
  protected void Set<T>(T? value, [CallerMemberName] string propertyName = "") => _BackingFields.Set(value, propertyName);

  /// <summary>
  /// Gets the value of a property. If the property does not exist, returns the specified default value.
  /// </summary>
  /// <typeparam name="T">The type of the property value.</typeparam>
  /// <param name="defaultValue">The default value to return if the property does not exist.</param>
  /// <param name="propertyName">The name of the property. Automatically provided by the CallerMemberName attribute.</param>
  /// <returns>The value of the property or the default value if the property does not exist.</returns>
  protected T? Get<T>(T? defaultValue = default, [CallerMemberName] string propertyName = "") => _BackingFields.Get(defaultValue, propertyName);

  /// <summary>
  /// Initializes a new instance of the <see cref="Observable"/> class and sets up the change notification mechanism.
  /// </summary>
  public Observable()
  {
    _BackingFields.OnFieldChanged += (propertyName, value) => { OnPropertyChanged(propertyName, value); };
  }
}
