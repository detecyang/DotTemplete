using System.Drawing;

namespace dotTemplate.Maker
{
    /// <summary>
    /// 一个字符块类，用于显示一个矩形ASCII字块
    /// </summary>
    public class Block
    {
        private bool _isIn;
        private bool _select;

        /// <summary>
        /// 获取或设置当前块被哪个文本框占用；为-1时表示不被任何框占用，>-1表示被占用的文本框的ID
        /// </summary>
        public int UsingID;

        /// <summary>
        /// 字符块的颜色
        /// </summary>
        public Color color;

        /// <summary>
        /// 字符块在画板上的大小和坐标
        /// </summary>
        public Rectangle rect;

        /// <summary>
        /// 鼠标是否在此字符块的范围
        /// </summary>
        public bool isIn
        {
            get { return _isIn; }
            set
            {
                _isIn = value;

                if (value == true)
                {
                    this.color = Color.FromArgb(95, 150, 190);  //鼠标经过时的颜色
                }
                else
                {
                    this.color = Color.FromArgb(220, 220, 220);  //字符块的颜色
                }
            }
        }

        /// <summary>
        /// 字符块处于选中状态
        /// </summary>
        public bool selected
        {
            get { return _select; }
            set
            {
                _select = value;

                if (_select == true)
                {
                    this.color = Color.FromArgb(181,230,29);  //选中时的颜色
                }
                else
                {
                    if (UsingID >= 0) { this.color = Color.FromArgb(95, 150, 190); }
                    else { this.color = Color.FromArgb(220, 220, 220); }
                }
            }
        }

        public Block()
        {
            UsingID = -1;
            rect = new Rectangle(0, 0, 7, 13);
            isIn = false;
            selected = false;
        }


    }
}
