namespace BackingFieldsSample;

public partial class Form1 : Form
{
  public Form1()
  {
    InitializeComponent();

    var classWithBackingFields = new ClassWithBackingFields();
    classWithBackingFields.OnPropertyChanged += (propertyName, value) =>
    {
      textBox1.AppendText($"Property {propertyName} changed to {value}{Environment.NewLine}");
      textBox1.BackColor = classWithBackingFields.BackgroundColor;
    };

    propertyGrid1.SelectedObject = classWithBackingFields;
    textBox1.BackColor = classWithBackingFields.BackgroundColor;
  }
}
