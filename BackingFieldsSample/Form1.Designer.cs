namespace BackingFieldsSample;

partial class Form1
{
  /// <summary>
  ///  Required designer variable.
  /// </summary>
  private System.ComponentModel.IContainer components = null;

  /// <summary>
  ///  Clean up any resources being used.
  /// </summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && (components != null))
    {
      components.Dispose();
    }
    base.Dispose(disposing);
  }

  #region Windows Form Designer generated code

  /// <summary>
  ///  Required method for Designer support - do not modify
  ///  the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    propertyGrid1 = new PropertyGrid();
    textBox1 = new TextBox();
    SuspendLayout();
    // 
    // propertyGrid1
    // 
    propertyGrid1.BackColor = SystemColors.Control;
    propertyGrid1.Dock = DockStyle.Right;
    propertyGrid1.Location = new Point(402, 0);
    propertyGrid1.Name = "propertyGrid1";
    propertyGrid1.Size = new Size(372, 662);
    propertyGrid1.TabIndex = 0;
    // 
    // textBox1
    // 
    textBox1.Dock = DockStyle.Fill;
    textBox1.Location = new Point(0, 0);
    textBox1.Multiline = true;
    textBox1.Name = "textBox1";
    textBox1.Size = new Size(402, 662);
    textBox1.TabIndex = 1;
    // 
    // Form1
    // 
    AutoScaleDimensions = new SizeF(7F, 15F);
    AutoScaleMode = AutoScaleMode.Font;
    ClientSize = new Size(774, 662);
    Controls.Add(textBox1);
    Controls.Add(propertyGrid1);
    Name = "Form1";
    Text = "Form1";
    ResumeLayout(false);
    PerformLayout();
  }

  #endregion

  private PropertyGrid propertyGrid1;
  private TextBox textBox1;
}
