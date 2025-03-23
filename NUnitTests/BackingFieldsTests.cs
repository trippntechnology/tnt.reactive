using System.Diagnostics.CodeAnalysis;
using TNT.Reactive;

namespace NUnitTests;

[ExcludeFromCodeCoverage]
public class BackingFieldsTests
{
  [Test]
  public void SetAndGetTest()
  {
    var backingFields = new BackingFields();
    backingFields.Set("value1", "Property1");
    var result = backingFields.Get<string>("Default value", "Property1");

    Assert.That(result, Is.EqualTo("value1"));
  }

  [Test]
  public void SetAndGetDefaultValueTest()
  {
    var backingFields = new BackingFields();
    var result = backingFields.Get("defaultValue", "NonExistentProperty");

    Assert.That(result, Is.EqualTo("defaultValue"));
  }

  [Test]
  public void OnFieldChangedTest()
  {
    var backingFields = new BackingFields();
    string? changedProperty = null;
    object? changedValue = null;

    backingFields.OnFieldChanged = (propertyName, value) =>
    {
      changedProperty = propertyName;
      changedValue = value;
    };

    backingFields.Set("newValue", "Property1");

    Assert.That(changedProperty, Is.EqualTo("Property1"));
    Assert.That(changedValue, Is.EqualTo("newValue"));
  }

  [Test]
  public void CopyConstructorTest()
  {
    var original = new BackingFields();
    original.Set("value1", "Property1");

    var copy = new BackingFields(original);
    var result = copy.Get<string>("Default Value", "Property1");

    Assert.That(result, Is.EqualTo("value1"));
  }

  [Test]
  public void SetNullValueRemovesPropertyTest()
  {
    var backingFields = new BackingFields();
    backingFields.Set<string?>("value1", "Property1");
    var result = backingFields.Get<string>("value1", "Property1");
    backingFields.Set<string?>(null, "Property1");
    result = backingFields.Get<string>("defaultValue", "Property1");
    Assert.That(result, Is.Null);
    backingFields.Set<string?>("value2", "Property1");
    result = backingFields.Get<string>("defaultValue", "Property1");
    Assert.That(result, Is.EqualTo("value2"));
  }

  [Test]
  public void ClassWithBackingFieldsTests()
  {
    var classWithBackingFields = new ClassWithBackingFields();
    string? changedProperty = null;
    object? changedValue = null;
    classWithBackingFields.OnFieldChanged = (propertyName, value) =>
    {
      changedProperty = propertyName;
      changedValue = value;
    };

    Assert.That(classWithBackingFields.StringProperty, Is.EqualTo("Initial Value"));
    classWithBackingFields.StringProperty = "newValue";
    Assert.That(classWithBackingFields.StringProperty, Is.EqualTo("newValue"));
    Assert.That(changedProperty, Is.EqualTo("StringProperty"));
    Assert.That(changedValue, Is.EqualTo("newValue"));
  }

  private class ClassWithBackingFields
  {
    private readonly BackingFields _backingFields = new();

    public Action<string, object?> OnFieldChanged
    {
      get => _backingFields.OnFieldChanged;
      set => _backingFields.OnFieldChanged = value;
    }

    public string StringProperty
    {
      get => _backingFields.Get("Initial Value") ?? string.Empty;
      set => _backingFields.Set(value);
    }
  }
}
