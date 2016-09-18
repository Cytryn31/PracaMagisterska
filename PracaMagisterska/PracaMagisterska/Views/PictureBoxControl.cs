using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PracaMagisterska.Views
{
	public partial class PictureBoxControl : PictureBoxZoom
	{
		private bool _wasFirstClick = false;
		private Position _lastClickPosition;
		private PictureBoxType type;
		private MinutiaeType[] MinutaeTypes = new[] { (MinutiaeType)1, (MinutiaeType)2, (MinutiaeType)3,
			(MinutiaeType)4, (MinutiaeType)5, (MinutiaeType)6,
			(MinutiaeType)7,(MinutiaeType) 8,(MinutiaeType) 9,
			(MinutiaeType)10,(MinutiaeType) 11,(MinutiaeType) 12};
		public PictureBox PictureBox => PicBox;
		public Graphics gf;
		public int factor = 1;

		public PictureBoxControl(PictureBoxType str) : base()
		{
			type = str;
			InitializeComponent();
			gf = PictureBox.CreateGraphics();
			PicBox.Click += new EventHandler(PictureBox_Click);
		}

		private int Angle(Position p1, Position p2)
		{
			float xDiff = p1.X - p2.X;
			float yDiff = p1.Y - p2.Y;
			return (int)(Math.Atan2(yDiff, xDiff) * (float)(180 / Math.PI));
		}

		public void DrawEclipse(Position clickPosition, int orient)
		{
			if (ZoomInAllowed == 0) factor = 1;
			else factor = 5;
			gf.DrawEllipse(new Pen(Color.Red, 2), new Rectangle(factor *clickPosition.X, factor * clickPosition.Y, 10, 10));
			gf.DrawLine(new Pen(Color.GreenYellow, 2), CalculatePositionOfOrientationVector(clickPosition, orient), new Point(factor * clickPosition.X + 5, factor * clickPosition.Y + 5));
		}

		private Point CalculatePositionOfOrientationVector(Position currentPosition, int orient, int distance = 10)
		{
			var x = distance * Math.Cos(orient * Math.PI / 180);
			var y = distance * Math.Sin(orient * Math.PI / 180);
			return new Point(factor * currentPosition.X + 5 + (int)x, factor * currentPosition.Y + 5 + (int)y);
		}

		private void PictureBox_Click(object sender, EventArgs e)
		{
			if (mode.Equals(PictureBoxMode.Zoom))return;
			var mouseEventArgs = e as MouseEventArgs;
			if (mouseEventArgs != null)
			{
				var clickPosition = new Position
				{
					X = mouseEventArgs.X,
					Y = mouseEventArgs.Y
				};
				if (mouseEventArgs.Button == MouseButtons.Left)
				{

					if (!_wasFirstClick)
					{

						var point = this.PointToScreen(Point.Empty);
						Cursor.Clip = new Rectangle(point.X, point.Y, this.Width, this.Height);
						_wasFirstClick = true;
						gf.DrawEllipse(new Pen(Color.Red, 2), new Rectangle(clickPosition.X, clickPosition.Y, 10, 10));
					}
					else
					{
						var minutiaeType = MinutiaeType.Unknown;
						var regStoreForm = new InputBox();
						Cursor.Clip = Rectangle.Empty;
						if (regStoreForm.ShowDialog(this) == DialogResult.OK)
						{
							minutiaeType = regStoreForm.MinutiaeType;
						};
						if(!MinutaeTypes.Contains(minutiaeType)) minutiaeType = MinutiaeType.Unknown;
						regStoreForm.Dispose();
						Add(_lastClickPosition.X, _lastClickPosition.Y, Angle(clickPosition, _lastClickPosition), minutiaeType);
						
						_wasFirstClick = false;
						MinutiaeManager.Instance.CallDrawEclipses(this, type);
					}
				}

				if (mouseEventArgs.Button == MouseButtons.Right && !_wasFirstClick)
				{
					Remove(clickPosition.X, clickPosition.Y, 0);
					UpdateImage();
					MinutiaeManager.Instance.CallDrawEclipses(this, type);
				}
				_lastClickPosition = clickPosition;
			}
		}

		private void Add(int x, int y, int orientation, MinutiaeType minutaeTypee)
		{
			if (type.Equals(PictureBoxType.Ref))
			{
				if (MinutiaeManager.Instance.ReferenceImage == null) return;
				MinutiaeManager.Instance.ReferenceImage.Minutiaes.Add(new Minutiae()
				{
					Orientation = orientation,
					Position = new Position()
					{
						X = x,
						Y = y
					},
					Type = minutaeTypee
				});
			}
			else
			{
				if (MinutiaeManager.Instance.CalculatedImage == null) return;
				MinutiaeManager.Instance.CalculatedImage.Minutiaes.Add(new Minutiae()
				{
					Orientation = orientation,
					Position = new Position()
					{
						X = x,
						Y = y
					},
					Type = minutaeTypee
				});
			}
		}

		public void UpdateImage()
		{
			if (type.Equals(PictureBoxType.Ref))
			{
				if (MinutiaeManager.Instance.ReferenceImage != null)
				{
					if (MinutiaeManager.Instance.ReferenceImage.ImagePath != null)
					{
						var img = ImageProcessing.ImageProcessingHelper.ResizeImage(MinutiaeManager.Instance.ReferenceImage.ImagePath, PictureBox.Height, PictureBox.Width);
						PictureBox.Image = img;
					}
					else
					{
						PictureBox.Image = null;
					}
				}
			}
			else
			{
				if (MinutiaeManager.Instance.CalculatedImage != null)
				{
					if (MinutiaeManager.Instance.CalculatedImage.ImagePath != null)
					{
						var img = ImageProcessing.ImageProcessingHelper.ResizeImage(MinutiaeManager.Instance.CalculatedImage.ImagePath, PictureBox.Height, PictureBox.Width);
						PictureBox.Image = img;
					}
					else
					{
						PictureBox.Image = null;
					}
				}
			}

		}

		private void Remove(int x, int y, int orientation)
		{
			if (type.Equals(PictureBoxType.Ref))
			{
				if (MinutiaeManager.Instance.ReferenceImage == null) return;
				for (int i = 0; i < MinutiaeManager.Instance.ReferenceImage.Minutiaes.Count; i++)
				{
					var minutiae = MinutiaeManager.Instance.ReferenceImage.Minutiaes[i];
					var point = new Point(minutiae.Position.X, minutiae.Position.Y);
					var rect = new Rectangle(x - 15, y - 15, 20, 20);
					if (rect.Contains(point))
					{
						MinutiaeManager.Instance.ReferenceImage.Minutiaes.RemoveAt(i);
					}
				}
			}
			else
			{
				if (MinutiaeManager.Instance.CalculatedImage == null) return;
				for (int i = 0; i < MinutiaeManager.Instance.CalculatedImage.Minutiaes.Count; i++)
				{
					var minutiae = MinutiaeManager.Instance.CalculatedImage.Minutiaes[i];
					var point = new Point(minutiae.Position.X, minutiae.Position.Y);
					var rect = new Rectangle(x - 15, y - 15, 20, 20);

					if (rect.Contains(point))
					{
						MinutiaeManager.Instance.CalculatedImage.Minutiaes.RemoveAt(i);
					}
				}
			}
		}

		private void PictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			MinutiaeManager.Instance.CallDrawEclipses(this, type);
			if (_wasFirstClick)
			{
				var mouseEventArgs = e as MouseEventArgs;
				if (mouseEventArgs != null)
				{

				}
			}
		}
	}
}
