using TNT.Reactive;

namespace BackingFieldsSample;

internal class ClassWithBackingFields : Observable
{
  public string? NullStringProperty { get => Get<string?>(null); set => Set(value); }

  public string? StringProperty { get => Get("Initial Value"); set => Set(value); }

  public int IntProperty
  {
    get => Get(0);
    set
    {
      if (value < 0) NullStringProperty = null;
      Set(value);
    }
  }

  public Color BackgroundColor { get => Get(Color.Blue); set => Set(value); }
}
