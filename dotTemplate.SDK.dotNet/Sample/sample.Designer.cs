namespace dotTemplate.SDK.Sample
{
    partial class sample
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnParse = new System.Windows.Forms.Button();
            this.lab = new System.Windows.Forms.Label();
            this.txtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnParse
            // 
            this.btnParse.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnParse.Location = new System.Drawing.Point(371, 12);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(103, 28);
            this.btnParse.TabIndex = 0;
            this.btnParse.Text = "生成";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // lab
            // 
            this.lab.AutoSize = true;
            this.lab.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab.Location = new System.Drawing.Point(12, 17);
            this.lab.Name = "lab";
            this.lab.Size = new System.Drawing.Size(79, 20);
            this.lab.TabIndex = 1;
            this.lab.Text = "模板文件：";
            // 
            // txtBox
            // 
            this.txtBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox.Location = new System.Drawing.Point(-1, 46);
            this.txtBox.Multiline = true;
            this.txtBox.Name = "txtBox";
            this.txtBox.ReadOnly = true;
            this.txtBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBox.Size = new System.Drawing.Size(488, 426);
            this.txtBox.TabIndex = 2;
            // 
            // sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 470);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.lab);
            this.Controls.Add(this.btnParse);
            this.Name = "sample";
            this.Text = "sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Label lab;
        private System.Windows.Forms.TextBox txtBox;
    }
}

