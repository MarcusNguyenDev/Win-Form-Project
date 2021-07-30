namespace GUI_ItemsRental
{
    partial class FrmReport
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
            this.btnCheckedOut = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnActiveTool = new System.Windows.Forms.Button();
            this.btnActiveToolByBrand = new System.Windows.Forms.Button();
            this.btnRetiredTool = new System.Windows.Forms.Button();
            this.btnRetiredToolsByBrand = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCheckedOut
            // 
            this.btnCheckedOut.AutoEllipsis = true;
            this.btnCheckedOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckedOut.Location = new System.Drawing.Point(16, 41);
            this.btnCheckedOut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCheckedOut.Name = "btnCheckedOut";
            this.btnCheckedOut.Size = new System.Drawing.Size(283, 37);
            this.btnCheckedOut.TabIndex = 4;
            this.btnCheckedOut.Text = "All Checked Out Tools";
            this.btnCheckedOut.UseVisualStyleBackColor = true;
            this.btnCheckedOut.Click += new System.EventHandler(this.btnCheckedOut_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(16, 320);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(283, 37);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnActiveTool
            // 
            this.btnActiveTool.AutoEllipsis = true;
            this.btnActiveTool.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActiveTool.Location = new System.Drawing.Point(16, 85);
            this.btnActiveTool.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnActiveTool.Name = "btnActiveTool";
            this.btnActiveTool.Size = new System.Drawing.Size(283, 37);
            this.btnActiveTool.TabIndex = 10;
            this.btnActiveTool.Text = "All Active Tools";
            this.btnActiveTool.UseVisualStyleBackColor = true;
            this.btnActiveTool.Click += new System.EventHandler(this.btnActiveTool_Click);
            // 
            // btnActiveToolByBrand
            // 
            this.btnActiveToolByBrand.AutoEllipsis = true;
            this.btnActiveToolByBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActiveToolByBrand.Location = new System.Drawing.Point(16, 129);
            this.btnActiveToolByBrand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnActiveToolByBrand.Name = "btnActiveToolByBrand";
            this.btnActiveToolByBrand.Size = new System.Drawing.Size(283, 37);
            this.btnActiveToolByBrand.TabIndex = 11;
            this.btnActiveToolByBrand.Text = "All Active Tools by Brand";
            this.btnActiveToolByBrand.UseVisualStyleBackColor = true;
            this.btnActiveToolByBrand.Click += new System.EventHandler(this.btnActiveToolByBrand_Click);
            // 
            // btnRetiredTool
            // 
            this.btnRetiredTool.AutoEllipsis = true;
            this.btnRetiredTool.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetiredTool.Location = new System.Drawing.Point(16, 174);
            this.btnRetiredTool.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRetiredTool.Name = "btnRetiredTool";
            this.btnRetiredTool.Size = new System.Drawing.Size(283, 37);
            this.btnRetiredTool.TabIndex = 12;
            this.btnRetiredTool.Text = "All Retired Tools";
            this.btnRetiredTool.UseVisualStyleBackColor = true;
            this.btnRetiredTool.Click += new System.EventHandler(this.btnRetiredTool_Click);
            // 
            // btnRetiredToolsByBrand
            // 
            this.btnRetiredToolsByBrand.AutoEllipsis = true;
            this.btnRetiredToolsByBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetiredToolsByBrand.Location = new System.Drawing.Point(16, 218);
            this.btnRetiredToolsByBrand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRetiredToolsByBrand.Name = "btnRetiredToolsByBrand";
            this.btnRetiredToolsByBrand.Size = new System.Drawing.Size(283, 37);
            this.btnRetiredToolsByBrand.TabIndex = 13;
            this.btnRetiredToolsByBrand.Text = "All Retired Tools by Brand";
            this.btnRetiredToolsByBrand.UseVisualStyleBackColor = true;
            this.btnRetiredToolsByBrand.Click += new System.EventHandler(this.btnRetiredToolsByBrand_Click);
            // 
            // FrmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 418);
            this.Controls.Add(this.btnRetiredToolsByBrand);
            this.Controls.Add(this.btnRetiredTool);
            this.Controls.Add(this.btnActiveToolByBrand);
            this.Controls.Add(this.btnActiveTool);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCheckedOut);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmReport";
            this.Text = "Get Report";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmReport_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheckedOut;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnActiveTool;
        private System.Windows.Forms.Button btnActiveToolByBrand;
        private System.Windows.Forms.Button btnRetiredTool;
        private System.Windows.Forms.Button btnRetiredToolsByBrand;
    }
}