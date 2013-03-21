using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace dotTemplate.SDK.dotNet
{
    public class TextBox
    {
        public int ID;
        public string Name;
        public string value;
        public Rectangle Rect;
        public HorizontalAlignment Align;

        public TextBox()
        {
            ID=0;
            Name="";
            value="";
            Rect = new Rectangle();
            Align = HorizontalAlignment.Left;
        }
    }
}
