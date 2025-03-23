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
    backingFields.Set("value1", "Property1");
    backingFields.Set<string>(null, "Property1");

    var result = backingFields.Get<string>("defaultValue", "Property1");

    Assert.That(result, Is.EqualTo("defaultValue"));
  }
}
