using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracaMagisterska.Views
{
	public partial class InputBox : Form
	{
		public InputBox()
		{
			InitializeComponent();
		}

		public MinutiaeType MinutiaeType { get; set; }

		public InputBox(MinutiaeType storeId)
		{
			InitializeComponent();
			MinutiaeType = storeId;
			this.textBox1.Text = MinutiaeType.ToString();
		}

		private void OKbutton_Click(object sender, EventArgs e)
		{
			MinutiaeType = (MinutiaeType)Convert.ToInt32(textBox1.Text);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
