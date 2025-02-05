namespace HistogramApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.txtHistogramPath = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnSaveHistogram = new System.Windows.Forms.Button();
            this.btnGenerateHistogram = new System.Windows.Forms.Button();
            this.btnAsmDLL = new System.Windows.Forms.Button();
            this.btnCDLL = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // txtImagePath
            this.txtImagePath.Location = new System.Drawing.Point(12, 50);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(300, 27);
            this.txtImagePath.TabIndex = 0;

            // txtHistogramPath
            this.txtHistogramPath.Location = new System.Drawing.Point(12, 88);
            this.txtHistogramPath.Name = "txtHistogramPath";
            this.txtHistogramPath.Size = new System.Drawing.Size(300, 27);
            this.txtHistogramPath.TabIndex = 1;

            // btnSelectImage
            this.btnSelectImage.Location = new System.Drawing.Point(330, 48);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(150, 30);
            this.btnSelectImage.TabIndex = 2;
            this.btnSelectImage.Text = "Select Image";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);

            // btnSaveHistogram
            this.btnSaveHistogram.Location = new System.Drawing.Point(330, 86);
            this.btnSaveHistogram.Name = "btnSaveHistogram";
            this.btnSaveHistogram.Size = new System.Drawing.Size(150, 30);
            this.btnSaveHistogram.TabIndex = 3;
            this.btnSaveHistogram.Text = "Save Histogram";
            this.btnSaveHistogram.UseVisualStyleBackColor = true;
            this.btnSaveHistogram.Click += new System.EventHandler(this.btnSaveHistogram_Click);

            // btnGenerateHistogram
            this.btnGenerateHistogram.Location = new System.Drawing.Point(12, 128);
            this.btnGenerateHistogram.Name = "btnGenerateHistogram";
            this.btnGenerateHistogram.Size = new System.Drawing.Size(300, 30);
            this.btnGenerateHistogram.TabIndex = 4;
            this.btnGenerateHistogram.Text = "Generate Histogram";
            this.btnGenerateHistogram.UseVisualStyleBackColor = true;
            this.btnGenerateHistogram.Click += new System.EventHandler(this.btnGenerateHistogram_Click);

            // btnAsmDLL
            this.btnAsmDLL.Location = new System.Drawing.Point(12, 12);
            this.btnAsmDLL.Name = "btnAsmDLL";
            this.btnAsmDLL.Size = new System.Drawing.Size(150, 30);
            this.btnAsmDLL.TabIndex = 5;
            this.btnAsmDLL.Text = "Use ASM DLL";
            this.btnAsmDLL.UseVisualStyleBackColor = true;
            this.btnAsmDLL.Click += new System.EventHandler(this.btnAsmDLL_Click);

            // btnCDLL
            this.btnCDLL.Location = new System.Drawing.Point(180, 12);
            this.btnCDLL.Name = "btnCDLL";
            this.btnCDLL.Size = new System.Drawing.Size(150, 30);
            this.btnCDLL.TabIndex = 6;
            this.btnCDLL.Text = "Use C DLL";
            this.btnCDLL.UseVisualStyleBackColor = true;
            this.btnCDLL.Click += new System.EventHandler(this.btnCDLL_Click);
            // lblExecutionTime
            this.lblExecutionTime = new System.Windows.Forms.Label();
            this.lblExecutionTime.Location = new System.Drawing.Point(12, 170);
            this.lblExecutionTime.Name = "lblExecutionTime";
            this.lblExecutionTime.Size = new System.Drawing.Size(300, 27);
            this.lblExecutionTime.TabIndex = 7;
            this.lblExecutionTime.Text = "Execution Time: 0 ms";

            
            

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 220);
            this.Controls.Add(this.lblExecutionTime);
            this.Controls.Add(this.btnCDLL);
            this.Controls.Add(this.btnAsmDLL);
            this.Controls.Add(this.btnGenerateHistogram);
            this.Controls.Add(this.btnSaveHistogram);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.txtHistogramPath);
            this.Controls.Add(this.txtImagePath);
            this.Name = "Form1";
            this.Text = "Histogram Generator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.TextBox txtHistogramPath;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnSaveHistogram;
        private System.Windows.Forms.Button btnGenerateHistogram;
        private System.Windows.Forms.Button btnAsmDLL;
        private System.Windows.Forms.Button btnCDLL;
        private System.Windows.Forms.Label lblExecutionTime;

    }
}