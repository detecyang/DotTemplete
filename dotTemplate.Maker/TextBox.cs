using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace dotTemplate.Maker
{
    /// <summary>
    /// 一个文本框类，用于显示模板中绘制出的一个文本框
    /// </summary>
    public class TextBox
    {
        private int _id;
        private string _name;
        private Rectangle _rect;
        private HorizontalAlignment _align;

        /// <summary>
        /// 当前此文本框是否处于选中状态
        /// </summary>
        public bool isSelected;

        public TextBox()
        {
            this._rect = new Rectangle(0, 0, 0, 0);
            isSelected = false;
        }

        [CategoryAttribute("设计"),DescriptionAttribute("文本框的标识(只读)"),ReadOnlyAttribute(true)]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        [CategoryAttribute("设计"),DescriptionAttribute("文本框的名字")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        [CategoryAttribute("布局"), DescriptionAttribute("文本框的大小和位置"), ReadOnlyAttribute(true)]
        public Rectangle Rect
        {
            get
            {
                return _rect;
            }
            set
            {
                _rect = value;
            }
        }

        [CategoryAttribute("风格"), DescriptionAttribute("文本框内文字对齐方式")]
        public HorizontalAlignment Align
        {
            get
            {
                return _align;
            }
            set
            {
                _align = value;
            }
        }
    }
}