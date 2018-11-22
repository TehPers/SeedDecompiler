namespace GUI {
    partial class Form1 {
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
            this.rtbBefunge = new System.Windows.Forms.RichTextBox();
            this.tbSeed = new System.Windows.Forms.TextBox();
            this.btnSeed = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBefunge = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudInit = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInit)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbBefunge
            // 
            this.rtbBefunge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbBefunge.Location = new System.Drawing.Point(13, 34);
            this.rtbBefunge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtbBefunge.MaxLength = 624;
            this.rtbBefunge.Name = "rtbBefunge";
            this.rtbBefunge.Size = new System.Drawing.Size(693, 389);
            this.rtbBefunge.TabIndex = 0;
            this.rtbBefunge.Text = "";
            // 
            // tbSeed
            // 
            this.tbSeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSeed.Location = new System.Drawing.Point(13, 499);
            this.tbSeed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbSeed.Name = "tbSeed";
            this.tbSeed.Size = new System.Drawing.Size(469, 26);
            this.tbSeed.TabIndex = 1;
            // 
            // btnSeed
            // 
            this.btnSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeed.Location = new System.Drawing.Point(602, 495);
            this.btnSeed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSeed.Name = "btnSeed";
            this.btnSeed.Size = new System.Drawing.Size(104, 35);
            this.btnSeed.TabIndex = 2;
            this.btnSeed.Text = "To Seed";
            this.btnSeed.UseVisualStyleBackColor = true;
            this.btnSeed.Click += new System.EventHandler(this.btnSeed_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Befunge:";
            // 
            // btnBefunge
            // 
            this.btnBefunge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBefunge.Location = new System.Drawing.Point(490, 495);
            this.btnBefunge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBefunge.Name = "btnBefunge";
            this.btnBefunge.Size = new System.Drawing.Size(104, 35);
            this.btnBefunge.TabIndex = 2;
            this.btnBefunge.Text = "To Befunge";
            this.btnBefunge.UseVisualStyleBackColor = true;
            this.btnBefunge.Click += new System.EventHandler(this.btnBefunge_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 463);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Key Width";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 431);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Key[0]";
            // 
            // nudWidth
            // 
            this.nudWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudWidth.Location = new System.Drawing.Point(98, 461);
            this.nudWidth.Maximum = new decimal(new int[] {
            624,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(60, 26);
            this.nudWidth.TabIndex = 6;
            this.nudWidth.Value = new decimal(new int[] {
            624,
            0,
            0,
            0});
            // 
            // nudInit
            // 
            this.nudInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudInit.Location = new System.Drawing.Point(98, 431);
            this.nudInit.Name = "nudInit";
            this.nudInit.Size = new System.Drawing.Size(120, 26);
            this.nudInit.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 544);
            this.Controls.Add(this.nudInit);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBefunge);
            this.Controls.Add(this.btnSeed);
            this.Controls.Add(this.tbSeed);
            this.Controls.Add(this.rtbBefunge);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Seed Decompiler";
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbBefunge;
        private System.Windows.Forms.TextBox tbSeed;
        private System.Windows.Forms.Button btnSeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBefunge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudInit;
    }
}

