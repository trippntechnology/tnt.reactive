using tnt.reactive;

namespace sample;

public partial class Form1 : Form
{

	private MutableStateFlow<int> mutableStateFlow = new MutableStateFlow<int>(0);

	public Form1()
	{
		InitializeComponent();

		mutableStateFlow.collect(foo => { label1.Text = foo.ToString(); });
	}

	private void button1_Click(object sender, EventArgs e)
	{
		mutableStateFlow.value = mutableStateFlow.value + 1;
	}
}