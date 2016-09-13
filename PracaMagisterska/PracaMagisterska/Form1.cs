using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ImageProcessing;
using log4net;
using PracaMagisterska.Properties;
using System.Globalization;
using Newtonsoft.Json;

namespace PracaMagisterska
{
	public partial class Form1 : Form
	{
		private static ILog _logger = LogManager.GetLogger(typeof(Form1));
		private static string FileName;
		public Form1()
		{
			InitializeComponent();
			InitializeList();
			objectListView2.Sorting = SortOrder.None;
		}

		private void InitializeList()
		{
			foreach (var item in Algorithms.Instance.Items)
			{
				objectListView1.AddObject(item);
			}
		}

		private void loadPictureButton_Click(object sender, EventArgs e)
		{
			try
			{
				var pictureBox = pictureBoxControl1.PictureBox;
				// Wrap the creation of the OpenFileDialog instance in a using statement,
				// rather than manually calling the Dispose method to ensure proper disposal
				using (var dlg = new OpenFileDialog())
				{
					dlg.Title = Resources.Form1_loadPictureButton_Click_Open_Image;
					dlg.Filter = Resources.Form1_loadPictureButton_Click____bmp____jpg____jpeg___png_____BMP____JPG____JPEG____PNG;

					if (dlg.ShowDialog() == DialogResult.OK)
					{
						var img = FromFile(dlg.FileName);
						FileName = dlg.FileName;
						img = ImageProcessingHelper.ResizeImage(img, pictureBox.Height, pictureBox.Width);
						pictureBox.Image = img;
						MinutiaeManager.Instance.ReferenceImage.ImagePath = img;
						MinutiaeManager.Instance.ReferenceImage.Minutiaes.Clear();
						//						pictureBoxControl2.PictureBox.Image = (Image)img.Clone();
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
				var sfd = new SaveFileDialog();
				sfd.Filter = "Images|*.png;*.bmp;*.jpg";
				var format = ImageFormat.Png;
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					var ext = Path.GetExtension(sfd.FileName);
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
			MoveItem(objectListView2, -1);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			MoveItem(objectListView2, 1);
		}

		private void MoveItem(ObjectListView list, int direction)
		{
			// Checking selected item
			if (list.SelectedItem == null || list.SelectedIndex < 0)
				return; // No selected item - nothing to do

			// Calculate new index using move direction
			var newIndex = list.SelectedIndex + direction;

			// Checking bounds of the range
			if (newIndex < 0 || newIndex >= list.Items.Count)
				return; // Index out of range - nothing to do

			ListViewItem selected = list.SelectedItem;

			// Removing removable element
			list.Items.Remove(selected);
			// Insert it in new position
			list.Items.Insert(newIndex, selected);

			list.SelectedIndex = newIndex;
		}

		private void userControlWithAutomaticGeneratedContent1_Load(object sender, EventArgs e)
		{
		}

		private void objectListView2_SelectedIndexChanged(object sender, EventArgs e)
		{
			userControlWithAutomaticGeneratedContent1.RefreshVals();
			var firstOrDefault = Algorithms.Instance.Items.FirstOrDefault(it =>
			{
				var algorithm = objectListView2.SelectedObject as Algorithm;
				return algorithm != null && it.Description == algorithm.Description;
			});
			if (firstOrDefault != null)
				foreach (var algorithmParameter in firstOrDefault.Parameters)
				{
					userControlWithAutomaticGeneratedContent1.AddField(algorithmParameter.Name, algorithmParameter.ParameterType, firstOrDefault.Description);
				}
		}

		private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			userControlWithAutomaticGeneratedContent1.Controls.Clear();
		}

		private void calculate_Click(object sender, EventArgs e)
		{
			if (FileName != null && !FileName.Any()) return;
			userControlWithAutomaticGeneratedContent1.FiillVals();
			userControlWithAutomaticGeneratedContent1.RefreshVals();
			var algorithms = new List<Algorithm>();
			if (objectListView2.Objects == null) return;
			foreach (var item in objectListView2.Objects)
			{
				var tmp = Algorithms.Instance.Items.First(p => p.Description.Equals((item as Algorithm).Description));
				algorithms.Add(tmp);
			}

			//DisableControls(this);
			var processor = new Processor();
			foreach (var algorithm in userControlWithAutomaticGeneratedContent1.GetAlgorithms(algorithms))
			{
				foreach (var algorithmParameter in algorithm.Parameters)
				{
					if (!Validate(algorithmParameter.Value, algorithmParameter.ParameterType))
					{
						if (algorithmParameter.ParameterType == ParameterType.NzDouble ||
							algorithmParameter.ParameterType == ParameterType.NzInt)
							MessageBox.Show(algorithmParameter.Description + " " + Resources.Form1_calculate_Click__nie_moze_byc_zerem);
						MessageBox.Show(algorithmParameter.Description + " " + Resources.Form1_calculate_Click_jest_niepoprawny);
						return;
					}
				}
				processor.Queue.Enqueue(algorithm);
			}
			processor.Process(FileName);

			var img = FromFile(Processor.FileName + ".tiff");
			img = ImageProcessingHelper.ResizeImage(img, pictureBoxControl2.PictureBox.Height, pictureBoxControl2.PictureBox.Width);
			pictureBoxControl2.PictureBox.Image = img;
			MinutiaeManager.Instance.CalculatedImage.ImagePath = img;

			_logger.Info("Opening a file successfully completed");
			//EnableControls(this);
		}

		public static Image FromFile(string path)
		{
			var bytes = File.ReadAllBytes(path);
			var ms = new MemoryStream(bytes);
			var img = Image.FromStream(ms);
			return img;
		}

		public bool Validate(string text, ParameterType type)
		{
			if (type == ParameterType.Double || type == ParameterType.Int)
			{
				return text.IsNumeric();
			}
			if (type == ParameterType.NzDouble || type == ParameterType.NzInt)
			{
				return decimal.Parse(text, CultureInfo.InvariantCulture) != 0;
			}
			return true;
		}
		private void DisableControls(Control con)
		{
			foreach (Control c in con.Controls)
			{
				DisableControls(c);
			}
			con.Enabled = false;
		}

		private void button5_Click_1(object sender, EventArgs e)
		{
			try
			{
				var pictureBox = pictureBoxControl2.PictureBox;
				// Wrap the creation of the OpenFileDialog instance in a using statement,
				// rather than manually calling the Dispose method to ensure proper disposal
				using (var dlg = new OpenFileDialog())
				{
					dlg.Title = Resources.Form1_loadPictureButton_Click_Open_Image;
					dlg.Filter = Resources.Form1_loadPictureButton_Click____bmp____jpg____jpeg___png_____BMP____JPG____JPEG____PNG;

					if (dlg.ShowDialog() == DialogResult.OK)
					{
						var img = FromFile(dlg.FileName);
						FileName = dlg.FileName;
						img = ImageProcessingHelper.ResizeImage(img, pictureBox.Height, pictureBox.Width);
						pictureBox.Image = img;
						MinutiaeManager.Instance.CalculatedImage.ImagePath = img;
						MinutiaeManager.Instance.CalculatedImage.Minutiaes.Clear();

						_logger.Info("Opening a file successfully completed");
					}
				}
			}
			catch (Exception exception)
			{
				_logger.Error(exception.Message);
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			try
			{
				var sfd = new SaveFileDialog();
				sfd.Filter = "Minucje|*.txt;*";
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					var output = "";
					if (MinutiaeManager.Instance.ReferenceImage.Minutiaes.Any())
					{
						output = JsonConvert.SerializeObject(MinutiaeManager.Instance.ReferenceImage);
					}
					File.WriteAllText(sfd.FileName, output);
					
					_logger.Info("Minucje saved");
				}
			}
			catch (Exception exception)
			{
				_logger.Error(exception.Message);
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			try
			{
				using (var dlg = new OpenFileDialog())
				{
					dlg.Title = "Plik z minucjami";

					if (dlg.ShowDialog() == DialogResult.OK)
					{
						FileName = dlg.FileName;
						var output = File.ReadAllText(FileName);
						MinutiaeManager.Instance.ReferenceImage =  JsonConvert.DeserializeObject< MinutiaeWithImage<Minutiae>>(output);
						_logger.Info("Opening a file successfully completed");
						MinutiaeManager.Instance.CallDrawEclipses(pictureBoxControl1,PictureBoxType.Ref);
					}
				}
			}
			catch (Exception exception)
			{
				_logger.Error(exception.Message);
			}
		}

		private void button9_Click(object sender, EventArgs e)
		{
			try
			{
				var sfd = new SaveFileDialog();
				sfd.Filter = "Minucje|*.txt;*";
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					var output = "";
					if (MinutiaeManager.Instance.CalculatedImage.Minutiaes.Any())
					{
						output = JsonConvert.SerializeObject(MinutiaeManager.Instance.CalculatedImage);
					}
					File.WriteAllText(sfd.FileName, output);

					_logger.Info("Minucje saved");
				}
			}
			catch (Exception exception)
			{
				_logger.Error(exception.Message);
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			try
			{
				using (var dlg = new OpenFileDialog())
				{
					dlg.Title = "Plik z minucjami";

					if (dlg.ShowDialog() == DialogResult.OK)
					{
						FileName = dlg.FileName;
						var output = File.ReadAllText(FileName);
						MinutiaeManager.Instance.CalculatedImage = JsonConvert.DeserializeObject<MinutiaeWithImage<Minutiae>>(output);
						_logger.Info("Opening a file successfully completed");
						MinutiaeManager.Instance.CallDrawEclipses(pictureBoxControl2, PictureBoxType.Calc);
					}
				}
			}
			catch (Exception exception)
			{
				_logger.Error(exception.Message);
			}
		}
	}

	public static class Extensions
	{
		public static bool IsNumeric(this string s)
		{
			foreach (char c in s)
			{
				if (!char.IsDigit(c) && c != '.')
				{
					return false;
				}
			}

			return true;
		}
	}
}