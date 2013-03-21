using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dotTemplate.SDK.dotNet;

namespace dotTemplate.SDK.Sample
{
    public partial class sample : Form
    {
        dotTemplateParser parser;

        public sample()
        {
            InitializeComponent();
            parser = new dotTemplateParser(Application.StartupPath + "\\示例模板.dtl");
            lab.Text = "模板文件：" + parser.name;
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            foreach (dotTemplate.SDK.dotNet.TextBox txt in parser.textBoxArray.Values)
            {
                txt.value = "中华人民共和国。“A quick brown fox jump over the lazy dog.”此句包含了26个英文字母";
            }
            Dictionary<int, string> txtDic = parser.ParseStringsWithTextBox();
            foreach (string str in txtDic.Values)
            {
                txtBox.Text += str + "\r\n";
            }
        }
    }
}
