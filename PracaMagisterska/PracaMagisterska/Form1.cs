﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

namespace PracaMagisterska
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_logger.Info("Click!");
		}
		private static ILog _logger = LogManager.GetLogger(typeof(Form1));

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
