using System.ComponentModel;
using TNT.Reactive;

namespace BackingFieldsSample;

internal class ClassWithBackingFields
{
  private BackingFields _BackingFields = new BackingFields();

  [Browsable(false)]
  public event Action<string, object?> OnFieldChanged = (_, __) => { };

  public string? NullStringProperty { get => _BackingFields.Get<string?>(null); set => _BackingFields.Set(value); }

  public string? StringProperty { get => _BackingFields.Get("Initial Value"); set => _BackingFields.Set(value); }

  public int IntProperty
  {
    get => _BackingFields.Get(0);
    set
    {
      if (value < 0) NullStringProperty = null;
      _BackingFields.Set(value);
    }
  }

  public Color BackgroundColor { get => _BackingFields.Get(Color.Blue); set => _BackingFields.Set(value); }
}
