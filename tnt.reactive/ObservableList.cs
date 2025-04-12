namespace TNT.Reactive;


/// <summary>
/// A generic list that notifies subscribers when the list or its items change.
/// </summary>
/// <typeparam name="T">The type of items in the list, which must inherit from Observable.</typeparam>
public class ObservableList<T> : List<T> where T : Observable
{
  /// <summary>
  /// Event triggered whenever the list changes (add, remove, clear, or item property changes).
  /// </summary>
  public event Action OnListChanged = () => { };

  /// <summary>
  /// Adds an item to the list and subscribes to its property change notifications.
  /// </summary>
  /// <param name="item">The item to add to the list.</param>
  public new void Add(T item)
  {
    base.Add(item); // Add the item to the base list.
    item.OnPropertyChanged += OnItemChanged; // Subscribe to the item's property changes.
    OnListChanged(); // Notify subscribers that the list has changed.
  }

  /// <summary>
  /// Removes an item from the list and unsubscribes from its property change notifications.
  /// </summary>
  /// <param name="item">The item to remove from the list.</param>
  public new void Remove(T item)
  {
    if (base.Remove(item)) // Remove the item from the base list if it exists.
    {
      item.OnPropertyChanged -= OnItemChanged; // Unsubscribe from the item's property changes.
      OnListChanged(); // Notify subscribers that the list has changed.
    }
  }

  /// <summary>
  /// Clears all items from the list and unsubscribes from their property change notifications.
  /// </summary>
  public new void Clear()
  {
    foreach (var item in this) // Iterate through all items in the list.
    {
      item.OnPropertyChanged -= OnItemChanged; // Unsubscribe from each item's property changes.
    }

    base.Clear(); // Clear the base list.
    OnListChanged(); // Notify subscribers that the list has changed.
  }

  /// <summary>
  /// Handles property change notifications from items in the list.
  /// Triggers the OnListChanged event to notify subscribers.
  /// </summary>
  /// <param name="propertyName">The name of the property that changed.</param>
  /// <param name="value">The new value of the property.</param>
  private void OnItemChanged(string propertyName, object? value) => OnListChanged();
}
