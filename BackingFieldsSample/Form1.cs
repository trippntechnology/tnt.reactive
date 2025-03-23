namespace BackingFieldsSample;

public partial class Form1 : Form
{
  public Form1()
  {
    InitializeComponent();

    var classWithBackingFields = new ClassWithBackingFields();
    classWithBackingFields.OnFieldChanged = (propertyName, value) =>
    {
      textBox1.AppendText($"Property {propertyName} changed to {value}{Environment.NewLine}");
      propertyGrid1.Refresh();
    };

    propertyGrid1.SelectedObject = classWithBackingFields;
  }
}
