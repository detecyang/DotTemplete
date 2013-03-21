namespace dotTemplate.Maker
{
    partial class MainWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.pic = new System.Windows.Forms.PictureBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTempH = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTempW = new System.Windows.Forms.TextBox();
            this.txtTempName = new System.Windows.Forms.TextBox();
            this.property = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolBtnSelector = new System.Windows.Forms.ToolStripButton();
            this.toolBtnTextBox = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.menu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.Location = new System.Drawing.Point(38, 35);
            this.pic.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(436, 336);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 484);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(766, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(766, 25);
            this.menu.TabIndex = 2;
            this.menu.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.toolStripSeparator1,
            this.menuOpen,
            this.menuSave,
            this.menuQuit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(58, 21);
            this.menuFile.Text = "文件(&F)";
            // 
            // menuNew
            // 
            this.menuNew.Image = global::dotTemplate.Properties.Resources.DocumentHS;
            this.menuNew.Name = "menuNew";
            this.menuNew.Size = new System.Drawing.Size(142, 22);
            this.menuNew.Text = "新建(&N)";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // menuOpen
            // 
            this.menuOpen.Image = global::dotTemplate.Properties.Resources.openfolderHS;
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(142, 22);
            this.menuOpen.Text = "打开模板(&O)";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // menuSave
            // 
            this.menuSave.Image = global::dotTemplate.Properties.Resources.saveHS;
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(142, 22);
            this.menuSave.Text = "保存模板(&S)";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // menuQuit
            // 
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(142, 22);
            this.menuQuit.Text = "退出(&Q)";
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(61, 21);
            this.menuHelp.Text = "帮助(&H)";
            // 
            // menuAbout
            // 
            this.menuAbout.Image = global::dotTemplate.Properties.Resources.Alert;
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(129, 22);
            this.menuAbout.Text = " 关于...(&A)";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTempH);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTempW);
            this.groupBox1.Controls.Add(this.txtTempName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 134);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模板设置";
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnApply.Location = new System.Drawing.Point(3, 108);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(225, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(166, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "(单位: 块)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(8, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "高度";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(166, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "(单位: 块)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(8, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "宽度";
            // 
            // txtTempH
            // 
            this.txtTempH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTempH.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTempH.Location = new System.Drawing.Point(67, 80);
            this.txtTempH.Name = "txtTempH";
            this.txtTempH.Size = new System.Drawing.Size(93, 23);
            this.txtTempH.TabIndex = 0;
            this.txtTempH.Text = "300";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "模板名称";
            // 
            // txtTempW
            // 
            this.txtTempW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTempW.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTempW.Location = new System.Drawing.Point(67, 51);
            this.txtTempW.Name = "txtTempW";
            this.txtTempW.Size = new System.Drawing.Size(93, 23);
            this.txtTempW.TabIndex = 0;
            this.txtTempW.Text = "400";
            // 
            // txtTempName
            // 
            this.txtTempName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTempName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTempName.Location = new System.Drawing.Point(67, 22);
            this.txtTempName.Name = "txtTempName";
            this.txtTempName.Size = new System.Drawing.Size(158, 23);
            this.txtTempName.TabIndex = 0;
            this.txtTempName.Text = "新建模板";
            // 
            // property
            // 
            this.property.Dock = System.Windows.Forms.DockStyle.Fill;
            this.property.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.property.Location = new System.Drawing.Point(3, 187);
            this.property.Name = "property";
            this.property.Size = new System.Drawing.Size(231, 241);
            this.property.TabIndex = 4;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.property, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.pictureBox2, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(237, 431);
            this.tableLayoutPanel.TabIndex = 5;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(3, 143);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(231, 38);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 53);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanel);
            this.splitContainer.Panel1MinSize = 20;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.vScrollBar);
            this.splitContainer.Panel2.Controls.Add(this.hScrollBar);
            this.splitContainer.Panel2.Controls.Add(this.pic);
            this.splitContainer.Panel2.Controls.Add(this.picBox);
            this.splitContainer.Panel2MinSize = 20;
            this.splitContainer.Size = new System.Drawing.Size(766, 431);
            this.splitContainer.SplitterDistance = 237;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 6;
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar.Location = new System.Drawing.Point(505, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(18, 410);
            this.vScrollBar.TabIndex = 3;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.Location = new System.Drawing.Point(3, 413);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(500, 18);
            this.hScrollBar.TabIndex = 2;
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBox.Location = new System.Drawing.Point(3, 0);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(500, 410);
            this.picBox.TabIndex = 1;
            this.picBox.TabStop = false;
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnSelector,
            this.toolBtnTextBox});
            this.toolBar.Location = new System.Drawing.Point(0, 25);
            this.toolBar.Name = "toolBar";
            this.toolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolBar.Size = new System.Drawing.Size(766, 25);
            this.toolBar.TabIndex = 7;
            this.toolBar.Text = "toolStrip1";
            // 
            // toolBtnSelector
            // 
            this.toolBtnSelector.Image = global::dotTemplate.Properties.Resources.PointerHS;
            this.toolBtnSelector.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSelector.Name = "toolBtnSelector";
            this.toolBtnSelector.Size = new System.Drawing.Size(76, 22);
            this.toolBtnSelector.Text = "选择工具";
            this.toolBtnSelector.Click += new System.EventHandler(this.toolBtnSelector_Click);
            // 
            // toolBtnTextBox
            // 
            this.toolBtnTextBox.Image = global::dotTemplate.Properties.Resources.Control_TextBox;
            this.toolBtnTextBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnTextBox.Name = "toolBtnTextBox";
            this.toolBtnTextBox.Size = new System.Drawing.Size(68, 22);
            this.toolBtnTextBox.Text = "文本框 ";
            this.toolBtnTextBox.Click += new System.EventHandler(this.toolBtnTextBox_Click);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 506);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "MainWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "dotTemplate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PropertyGrid property;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTempName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTempW;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTempH;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton toolBtnSelector;
        private System.Windows.Forms.ToolStripButton toolBtnTextBox;
    }
}

