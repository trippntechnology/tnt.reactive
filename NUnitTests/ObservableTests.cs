using System.Diagnostics.CodeAnalysis;
using TNT.Reactive;

namespace NUnitTests;

[ExcludeFromCodeCoverage]
public class ObservableTests
{
  private class TestObservable : Observable
  {
    public string? TestProperty
    {
      get => Get<string>("DefaultValue");
      set => Set(value);
    }
  }

  [Test]
  public void Constructor_ShouldInitialize_OnFieldChanged()
  {
    // Arrange
    var observable = new TestObservable();
    string? changedPropertyName = null;
    object? changedValue = null;

    observable.OnPropertyChanged += (propertyName, value) =>
    {
      changedPropertyName = propertyName;
      changedValue = value;
    };

    // Act
    observable.TestProperty = "NewValue";

    // Assert
    Assert.That(changedPropertyName, Is.EqualTo("TestProperty"));
    Assert.That(changedValue, Is.EqualTo("NewValue"));
  }

  [Test]
  public void Set_ShouldUpdateBackingFieldAndTriggerOnFieldChanged()
  {
    // Arrange
    var observable = new TestObservable();
    string? changedPropertyName = null;
    object? changedValue = null;

    observable.OnPropertyChanged += (propertyName, value) =>
    {
      changedPropertyName = propertyName;
      changedValue = value;
    };

    // Act
    observable.TestProperty = "UpdatedValue";

    // Assert
    Assert.That(changedPropertyName, Is.EqualTo("TestProperty"));
    Assert.That(changedValue, Is.EqualTo("UpdatedValue"));
  }

  [Test]
  public void Get_ShouldReturnDefaultValueIfNotSet()
  {
    // Arrange
    var observable = new TestObservable();

    // Assert
    Assert.That(observable.TestProperty, Is.EqualTo("DefaultValue"));
  }

  [Test]
  public void Get_ShouldReturnSetValue()
  {
    // Arrange
    var observable = new TestObservable();
    observable.TestProperty = "SetValue";

    // Act
    var value = observable.TestProperty;

    // Assert
    Assert.That(value, Is.EqualTo("SetValue"));
  }
}
