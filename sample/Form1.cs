using tnt.reactive;
using static tnt.reactive.FlowHelpers;

namespace sample;

public partial class Form1 : Form
{

	private MutableStateFlow<int> mutableStateFlow1 = new MutableStateFlow<int>(0);
	private MutableStateFlow<int> mutableStateFlow2 = new MutableStateFlow<int>(0);
	private Flow<int> flow1;

	public Form1()
	{
		InitializeComponent();

		flow1 = mutableStateFlow1.map(value =>
		{
			return value * 2;
		});

		mutableStateFlow1.collect(foo => { label1.Text = foo.ToString(); });
		mutableStateFlow2.collect(foo => { label2.Text = foo.ToString(); });
		var combinedFlow = combine<int, int, int>(mutableStateFlow1, mutableStateFlow2, (f1, f2) =>
		{
			return f1 + f2;
		});

		flow1.collect(value => label4.Text = value.ToString());

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