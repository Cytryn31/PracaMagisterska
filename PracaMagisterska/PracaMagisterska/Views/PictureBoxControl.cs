using System;
using System.Drawing;
using System.Windows.Forms;

namespace PracaMagisterska.Views
{
	public partial class PictureBoxControl : UserControl
	{
		private bool _wasFirstClick;
		private Position _lastClickPosition;

		public PictureBox PictureBox => pictureBox1;

		public PictureBoxControl()
		{
			InitializeComponent();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			var mouseEventArgs = e as MouseEventArgs;
			if (mouseEventArgs != null)
			{

				var clickPosition = new Position
				{
					X = mouseEventArgs.X,
					Y = mouseEventArgs.Y
				};

				if (_wasFirstClick)
				{
					_wasFirstClick = false;
					using (Graphics g = pictureBox1.CreateGraphics())
					{
						g.DrawLine(Pens.Red,
							_lastClickPosition.X,
							_lastClickPosition.Y,
							clickPosition.X,
							clickPosition.Y);

						MinutiaeManager.Instance.Container.Add(
							new Minutiae
							{
								Position = clickPosition,
								Orientation = (int)Math.Atan2(clickPosition.Y - _lastClickPosition.Y,
								clickPosition.X - _lastClickPosition.X),
								Shape = new[,]
								{
								   { false,false,false},
								   { false,true,false},
								   { false,false,false}
								},
								Type = MinutiaeType.Unknown
							});
					}
				}
				else
				{
					_wasFirstClick = true;
				}

				_lastClickPosition = clickPosition;
			}
		}
	}
}
