using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using ImageProcessing;
using log4net;
using PracaMagisterska.Properties;

namespace PracaMagisterska
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			InitializeList();
		}


		private static ILog _logger = LogManager.GetLogger(typeof(Form1));

		private void InitializeList()
		{
			foreach (var item in Algorithms.Instance.Items)
			{
				objectListView1.AddObject (item);
			}
		}
		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void loadPictureButton_Click(object sender, EventArgs e)
		{
			try
			{
				var pictureBox = pictureBoxControl1.PictureBox;
				// Wrap the creation of the OpenFileDialog instance in a using statement,
				// rather than manually calling the Dispose method to ensure proper disposal
				using (OpenFileDialog dlg = new OpenFileDialog())
				{
					dlg.Title = Resources.Form1_loadPictureButton_Click_Open_Image;
					dlg.Filter = Resources.Form1_loadPictureButton_Click____bmp____jpg____jpeg___png_____BMP____JPG____JPEG____PNG;

					if (dlg.ShowDialog() == DialogResult.OK)
					{
						var img = new Bitmap(dlg.FileName);
						img = ImageProcessingHelper.ResizeImage(img, pictureBox.Height, pictureBox.Width);
						pictureBox.Image = img;
						pictureBoxControl2.PictureBox.Image = (Image)img.Clone();
						_logger.Info("Opening a file successfully completed");
					}
				}
			}
			catch (Exception exception)
			{
				_logger.Error(exception.Message);
			}
		}

		private void savePictureButton_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "Images|*.png;*.bmp;*.jpg";
				ImageFormat format = ImageFormat.Png;
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					string ext = Path.GetExtension(sfd.FileName);
					switch (ext)
					{
						case ".jpg":
							format = ImageFormat.Jpeg;
							break;
						case ".bmp":
							format = ImageFormat.Bmp;
							break;
					}
					pictureBoxControl2.PictureBox.Image.Save(sfd.FileName, format);
					_logger.Info("Image saved");
				}
			}
			catch (Exception exception)
			{
				_logger.Error(exception.Message);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			objectListView2.AddObject(objectListView1.SelectedObject);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			objectListView2.RemoveObject(objectListView2.SelectedObject);
		}

		private void button3_Click(object sender, EventArgs e)
		{
		}

		private void button4_Click(object sender, EventArgs e)
		{
		}

		

	}
	
}
