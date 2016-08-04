using System.ComponentModel;
using System.Windows.Forms;

namespace PracaMagisterska.Views
{
	partial class ImageProcessingItemsListView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.algorithmName = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// algorithmName
			// 
			this.algorithmName.AutoSize = true;
			this.algorithmName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.algorithmName.Location = new System.Drawing.Point(3, 0);
			this.algorithmName.Name = "algorithmName";
			this.algorithmName.Size = new System.Drawing.Size(52, 18);
			this.algorithmName.TabIndex = 0;
			this.algorithmName.Text = "label1";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(0, 21);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(177, 20);
			this.textBox1.TabIndex = 1;
			// 
			// ImageProcessingItemsListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.algorithmName);
			this.Name = "ImageProcessingItemsListView";
			this.Size = new System.Drawing.Size(177, 44);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label algorithmName;
		private TextBox textBox1;
	}
}
