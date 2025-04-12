using System.Diagnostics.CodeAnalysis;
using TNT.Reactive;

namespace NUnitTests;

[ExcludeFromCodeCoverage]
public class ObservableListTests
{
  private class TestObservable : Observable
  {
    public string? Property { get => Get<string?>(); set => Set(value); }
  }

  [Test]
  public void Add_Item_ShouldTriggerOnListChanged()
  {
    // Arrange
    var list = new ObservableList<TestObservable>();
    bool eventTriggered = false;
    list.OnListChanged += () => eventTriggered = true;

    var item = new TestObservable();

    // Act
    list.Add(item);

    // Assert
    Assert.That(eventTriggered, Is.True);
    Assert.That(list, Does.Contain(item));
  }

  [Test]
  public void Remove_Item_ShouldTriggerOnListChanged()
  {
    // Arrange
    var list = new ObservableList<TestObservable>();
    var item = new TestObservable();
    list.Add(item);

    bool eventTriggered = false;
    list.OnListChanged += () => eventTriggered = true;

    // Act
    list.Remove(item);

    // Assert
    Assert.That(eventTriggered, Is.True);
    Assert.That(list, Does.Not.Contain(item));
  }

  [Test]
  public void Clear_List_ShouldTriggerOnListChanged()
  {
    // Arrange
    var list = new ObservableList<TestObservable>();
    list.Add(new TestObservable());
    list.Add(new TestObservable());

    bool eventTriggered = false;
    list.OnListChanged += () => eventTriggered = true;

    // Act
    list.Clear();

    // Assert
    Assert.That(eventTriggered, Is.True);
    Assert.That(list, Is.Empty);
  }

  [Test]
  public void Item_PropertyChange_ShouldTriggerOnListChanged()
  {
    // Arrange
    var list = new ObservableList<TestObservable>();
    var item = new TestObservable();
    list.Add(item);

    bool eventTriggered = false;
    list.OnListChanged += () => eventTriggered = true;

    // Act
    item.Property = "New Value";

    // Assert
    Assert.That(eventTriggered, Is.True);
  }
}
