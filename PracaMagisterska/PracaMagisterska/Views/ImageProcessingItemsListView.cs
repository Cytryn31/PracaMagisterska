using System.Windows.Forms;

namespace PracaMagisterska.Views
{
	public partial class ImageProcessingItemsListView : UserControl
	{

		public ImageProcessingItemsListView(Algorithm algorithm)
		{
			InitializeComponent();
			algorithmName.Text = algorithm.Description;
//			AutoScaleDSC(algorithm.Description);
		}

		private void AutoScaleDSC(string dsc)
		{
			// amount of padding to add
			const int padding = 3;
			// get number of lines (first line is 0, so add 1)
			int numLines = textBox1.GetLineFromCharIndex(dsc.Length) + 1;
			// get border thickness
			int border = textBox1.Height - textBox1.ClientSize.Height;
			// set height (height of one line * number of lines + spacing)
			textBox1.Height = textBox1.Font.Height * numLines + padding + border;
		}
	}
}
