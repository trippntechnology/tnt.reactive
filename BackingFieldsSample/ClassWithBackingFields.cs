using System.ComponentModel;
using TNT.Reactive;

namespace BackingFieldsSample;

internal class ClassWithBackingFields
{
  private BackingFields _BackingFields = new BackingFields();

  [Browsable(false)]
  public Action<string, object?> OnFieldChanged
  {
    get => _BackingFields.OnFieldChanged;
    set => _BackingFields.OnFieldChanged = value;
  }

  public string? NullStringProperty
  {
    get => _BackingFields.Get<string?>(null);
    set => _BackingFields.Set(value);
  }

  public string? StringProperty
  {
    get => _BackingFields.Get("Initial Value");
    set => _BackingFields.Set(value);
  }


  public int IntProperty
  {
    get => _BackingFields.Get(0);
    set
    {
      if (value < 0) NullStringProperty = null;
      _BackingFields.Set(value);
    }
  }
}
