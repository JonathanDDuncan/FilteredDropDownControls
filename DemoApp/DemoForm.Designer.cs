using DropDownControls.GroupedComboBox;

namespace DemoApp {
	partial class DemoForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoForm));
            this.label1 = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.gcbEditable = new DropDownControls.GroupedComboBox.FilteredGroupedComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "FilteredGroupedComboBox (editable)";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Closed");
            this.imageList.Images.SetKeyName(1, "Open");
            // 
            // gcbEditable
            // 
            this.gcbEditable.DataSource = null;
            this.gcbEditable.Location = new System.Drawing.Point(224, 21);
            this.gcbEditable.Name = "gcbEditable";
            this.gcbEditable.Size = new System.Drawing.Size(200, 23);
            this.gcbEditable.TabIndex = 5;
            
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 90);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gcbEditable);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoForm";
            this.Text = "DropDownControls Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private FilteredGroupedComboBox gcbEditable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList;
	}
}