namespace PracaMagisterska
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.loadPictureButton = new System.Windows.Forms.Button();
			this.savePictureButton = new System.Windows.Forms.Button();
			this.calculate = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.objectListView2 = new BrightIdeasSoftware.ObjectListView();
			this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
			this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.button3 = new System.Windows.Forms.Button();
			this.userControlWithAutomaticGeneratedContent1 = new PracaMagisterska.Views.UserControlWithAutomaticGeneratedContent();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.pictureBoxControl2 = new PracaMagisterska.Views.PictureBoxControl(PictureBoxType.Calc);
			this.pictureBoxControl1 = new PracaMagisterska.Views.PictureBoxControl(PictureBoxType.Ref);
			((System.ComponentModel.ISupportInitialize)(this.objectListView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
			this.SuspendLayout();
			// 
			// loadPictureButton
			// 
			resources.ApplyResources(this.loadPictureButton, "loadPictureButton");
			this.loadPictureButton.Name = "loadPictureButton";
			this.loadPictureButton.UseVisualStyleBackColor = true;
			this.loadPictureButton.Click += new System.EventHandler(this.loadPictureButton_Click);
			// 
			// savePictureButton
			// 
			resources.ApplyResources(this.savePictureButton, "savePictureButton");
			this.savePictureButton.Name = "savePictureButton";
			this.savePictureButton.UseVisualStyleBackColor = true;
			this.savePictureButton.Click += new System.EventHandler(this.savePictureButton_Click);
			// 
			// calculate
			// 
			resources.ApplyResources(this.calculate, "calculate");
			this.calculate.Name = "calculate";
			this.calculate.UseVisualStyleBackColor = true;
			this.calculate.Click += new System.EventHandler(this.calculate_Click);
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.TabStop = false;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.TabStop = false;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button4
			// 
			resources.ApplyResources(this.button4, "button4");
			this.button4.Name = "button4";
			this.button4.TabStop = false;
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// objectListView2
			// 
			this.objectListView2.AllColumns.Add(this.olvColumn2);
			this.objectListView2.CausesValidation = false;
			this.objectListView2.CellEditUseWholeCell = false;
			this.objectListView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2});
			this.objectListView2.Cursor = System.Windows.Forms.Cursors.Default;
			resources.ApplyResources(this.objectListView2, "objectListView2");
			this.objectListView2.Name = "objectListView2";
			this.objectListView2.SelectColumnsMenuStaysOpen = false;
			this.objectListView2.SelectColumnsOnRightClick = false;
			this.objectListView2.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
			this.objectListView2.ShowGroups = false;
			this.objectListView2.UseCompatibleStateImageBehavior = false;
			this.objectListView2.View = System.Windows.Forms.View.Details;
			this.objectListView2.SelectedIndexChanged += new System.EventHandler(this.objectListView2_SelectedIndexChanged);
			// 
			// olvColumn2
			// 
			this.olvColumn2.AspectName = "Description";
			this.olvColumn2.Sortable = false;
			resources.ApplyResources(this.olvColumn2, "olvColumn2");
			// 
			// objectListView1
			// 
			this.objectListView1.AllColumns.Add(this.olvColumn1);
			this.objectListView1.CausesValidation = false;
			this.objectListView1.CellEditUseWholeCell = false;
			this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
			this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
			resources.ApplyResources(this.objectListView1, "objectListView1");
			this.objectListView1.Name = "objectListView1";
			this.objectListView1.SelectColumnsMenuStaysOpen = false;
			this.objectListView1.SelectColumnsOnRightClick = false;
			this.objectListView1.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
			this.objectListView1.ShowGroups = false;
			this.objectListView1.UseCompatibleStateImageBehavior = false;
			this.objectListView1.View = System.Windows.Forms.View.Details;
			this.objectListView1.SelectedIndexChanged += new System.EventHandler(this.objectListView1_SelectedIndexChanged);
			// 
			// olvColumn1
			// 
			this.olvColumn1.AspectName = "Description";
			resources.ApplyResources(this.olvColumn1, "olvColumn1");
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			// 
			// button3
			// 
			resources.ApplyResources(this.button3, "button3");
			this.button3.Name = "button3";
			this.button3.TabStop = false;
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// userControlWithAutomaticGeneratedContent1
			// 
			resources.ApplyResources(this.userControlWithAutomaticGeneratedContent1, "userControlWithAutomaticGeneratedContent1");
			this.userControlWithAutomaticGeneratedContent1.Name = "userControlWithAutomaticGeneratedContent1";
			this.userControlWithAutomaticGeneratedContent1.Load += new System.EventHandler(this.userControlWithAutomaticGeneratedContent1_Load);
			// 
			// checkBox1
			// 
			resources.ApplyResources(this.checkBox1, "checkBox1");
			this.checkBox1.Name = "checkBox1";
			// 
			// checkBox2
			// 
			resources.ApplyResources(this.checkBox2, "checkBox2");
			this.checkBox2.Name = "checkBox2";
			// 
			// button5
			// 
			resources.ApplyResources(this.button5, "button5");
			this.button5.Name = "button5";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click_1);
			// 
			// button6
			// 
			resources.ApplyResources(this.button6, "button6");
			this.button6.Name = "button6";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			resources.ApplyResources(this.button7, "button7");
			this.button7.Name = "button7";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// pictureBoxControl2
			// 
			this.pictureBoxControl2.BackColor = System.Drawing.Color.White;
			this.pictureBoxControl2.Border = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxControl2.Cursor = System.Windows.Forms.Cursors.Cross;
			resources.ApplyResources(this.pictureBoxControl2, "pictureBoxControl2");
			this.pictureBoxControl2.Name = "pictureBoxControl2";
			this.pictureBoxControl2.Picture = "";
			this.pictureBoxControl2.PictureImage = null;
			// 
			// pictureBoxControl1
			// 
			this.pictureBoxControl1.BackColor = System.Drawing.Color.White;
			this.pictureBoxControl1.Border = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxControl1.Cursor = System.Windows.Forms.Cursors.Cross;
			resources.ApplyResources(this.pictureBoxControl1, "pictureBoxControl1");
			this.pictureBoxControl1.Name = "pictureBoxControl1";
			this.pictureBoxControl1.Picture = "";
			this.pictureBoxControl1.PictureImage = null;
			// 
			// button8
			// 
			resources.ApplyResources(this.button8, "button8");
			this.button8.Name = "button8";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button9
			// 
			resources.ApplyResources(this.button9, "button9");
			this.button9.Name = "button9";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// Form1
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.HotTrack;
			this.Controls.Add(this.button8);
			this.Controls.Add(this.button9);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.userControlWithAutomaticGeneratedContent1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.objectListView2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.objectListView1);
			this.Controls.Add(this.calculate);
			this.Controls.Add(this.pictureBoxControl2);
			this.Controls.Add(this.pictureBoxControl1);
			this.Controls.Add(this.savePictureButton);
			this.Controls.Add(this.loadPictureButton);
			this.Name = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.objectListView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button loadPictureButton;
		private System.Windows.Forms.Button savePictureButton;
		private Views.PictureBoxControl pictureBoxControl1;
		private Views.PictureBoxControl pictureBoxControl2;
		private System.Windows.Forms.Button calculate;
		private BrightIdeasSoftware.ObjectListView objectListView1;
		private BrightIdeasSoftware.OLVColumn olvColumn1;
		private System.Windows.Forms.Button button1;
		private BrightIdeasSoftware.ObjectListView objectListView2;
		private BrightIdeasSoftware.OLVColumn olvColumn2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button4;
		private Views.UserControlWithAutomaticGeneratedContent userControlWithAutomaticGeneratedContent1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button9;
	}
}

