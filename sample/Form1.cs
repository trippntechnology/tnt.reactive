using tnt.reactive;

namespace sample;

public partial class Form1 : Form
{

	private MutableStateFlow<int> mutableStateFlow1 = new MutableStateFlow<int>(0);
	private MutableStateFlow<int> mutableStateFlow2 = new MutableStateFlow<int>(0);

	public Form1()
	{
		InitializeComponent();

		mutableStateFlow1.collect(foo => { label1.Text = foo.ToString(); });
		mutableStateFlow2.collect(foo => { label2.Text = foo.ToString(); });
		var combinedFlow = Flow<int>.combine<int, int, int>(mutableStateFlow1, mutableStateFlow2, (f1, f2) =>
		{
			return f1 + f2;
		});

		combinedFlow.collect(foo => { label3.Text = foo.ToString(); });
	}

	private void button1_Click(object sender, EventArgs e)
	{
		mutableStateFlow1.value = mutableStateFlow1.value + 1;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		mutableStateFlow2.value = mutableStateFlow2.value + 1;
	}
}