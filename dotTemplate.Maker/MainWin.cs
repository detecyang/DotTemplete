using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace dotTemplate
{
    public partial class MainWin : Form
    {
        //类成员==========================================================================================================
        ToolStripStatusLabel labLocal;
        const int BLOCK_W = 7, BLOCK_H = 13, SPACE = 1;
        int TMP_WIDTH = 50, TMP_HEIGHT = 20;
        string TEMPLATE_NAME = "新建模板";
        Block[][] blocks;

        //模板的坐标
        int tmpX, tmpY, tmpW, tmpH;

        //TextBox
        int g_TxtIDStream = 0;
        Dictionary<int,TextBox> g_TextDic = new Dictionary<int,TextBox>(10);
        //当前鼠标选中的TextBox文本框的ID
        int g_currSelectTextBox = -1;

        //鼠标相关成员
        bool mouseDown;
        Point ptMouseDown = new Point();
        Rectangle txtRectWhenMdown = new Rectangle();

        //初始化方法======================================================================================================
        private void initControl()
        {
            splitContainer.SplitterMoved += (object sender, SplitterEventArgs e) =>
            {
                if (splitContainer.SplitterDistance > 250)
                {
                    splitContainer.SplitterDistance = 250;
                }
            };

            initPic();
            picBox.Controls.Add(pic);

            labLocal = new ToolStripStatusLabel();
            statusBar.Items.Add(labLocal);
            toolBtnSelector.Checked = true;
            property.PropertySort = PropertySort.NoSort;
            initTextBoxValue();
            pic.Focus();
        }

        private void initTextBoxValue()
        {
            txtTempName.Text = TEMPLATE_NAME;
            txtTempW.Text = TMP_WIDTH.ToString();
            txtTempH.Text = TMP_HEIGHT.ToString();
        }

        private void initPic()
        {
            //初始化块数量
            blocks = new Block[TMP_WIDTH][];

            //初始化模板的大小和位置
            tmpW = TMP_WIDTH * BLOCK_W + (TMP_WIDTH - 1)*SPACE + 2;
            tmpH = TMP_HEIGHT * BLOCK_H + (TMP_HEIGHT - 1)*SPACE + 2;
            tmpX = 1;
            tmpY = 1;

            pic.Width = tmpW;
            pic.Height = tmpH;

            pic.Left = picBox.Width / 2 - pic.Width / 2 - 3;
            pic.Top = picBox.Height / 2 - pic.Height / 2 - 2;
            if (pic.Left < 0)
            {
                pic.Left = 0;
            }
            if (pic.Top < 0)
            {
                pic.Top = 0;
            }

            for (int i = 0; i < TMP_WIDTH; i++)
            {
                blocks[i] = new Block[TMP_HEIGHT];
                for (int j = 0; j < TMP_HEIGHT; j++)
                {
                    blocks[i][j] = new Block();
                    blocks[i][j].rect = new Rectangle(tmpX + (BLOCK_W + SPACE) * i, tmpY + (BLOCK_H + SPACE) * j, BLOCK_W, BLOCK_H);
                }
            }

            picBox.Left = picBox.Top = 0;
            picBox.Width = splitContainer.Panel2.ClientSize.Width - vScrollBar.Width;
            picBox.Height = splitContainer.Panel2.ClientSize.Height - hScrollBar.Height;


            if (pic.Width > picBox.Width)
            {
                hScrollBar.Enabled = true;
            }
            else
            {
                hScrollBar.Enabled = false;
            }

            if (pic.Height > picBox.Height)
            {
                vScrollBar.Enabled = true;
            }
            else
            {
                vScrollBar.Enabled = false;
            }
            vScrollBar.Left = picBox.Left + picBox.Width;
            vScrollBar.Top = picBox.Top;
            vScrollBar.Height = picBox.Height;
            hScrollBar.Top = picBox.Top + picBox.Height;
            hScrollBar.Left = picBox.Left;
            hScrollBar.Width = picBox.Width;
            vScrollBar.Value = 0;
            vScrollBar.LargeChange = picBox.ClientSize.Height;
            vScrollBar.Maximum = pic.Height;
            hScrollBar.Value = 0;
            hScrollBar.LargeChange = picBox.ClientSize.Width;
            hScrollBar.Maximum = pic.Width;

            vScrollBar.Scroll -= new ScrollEventHandler(vScrollBar_Scroll);
            vScrollBar.Scroll += new ScrollEventHandler(vScrollBar_Scroll);

            hScrollBar.Scroll -= new ScrollEventHandler(hScrollBar_Scroll);
            hScrollBar.Scroll += new ScrollEventHandler(hScrollBar_Scroll);

            pic.Paint -= new PaintEventHandler(pic_Paint);
            pic.MouseMove -= new MouseEventHandler(pic_MouseMove);
            pic.MouseDown -= new MouseEventHandler(pic_MouseDown);
            pic.MouseUp -= new MouseEventHandler(pic_MouseUp);
            pic.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic.PreviewKeyDown -= new PreviewKeyDownEventHandler(pic_PreviewKeyDown);

            pic.Paint += new PaintEventHandler(pic_Paint);
            pic.MouseMove += new MouseEventHandler(pic_MouseMove);
            pic.MouseDown += new MouseEventHandler(pic_MouseDown);
            pic.MouseUp += new MouseEventHandler(pic_MouseUp);
            pic.MouseLeave += new EventHandler(pic_MouseLeave);
            pic.PreviewKeyDown += new PreviewKeyDownEventHandler(pic_PreviewKeyDown);
        }



        public MainWin()
        {
            InitializeComponent();
            this.Text = string.Format("{0} v{1}",Application.ProductName,Application.ProductVersion);
            initControl();

            splitContainer.Panel2.SizeChanged -= new EventHandler(Panel2_SizeChanged);
            splitContainer.Panel2.SizeChanged += new EventHandler(Panel2_SizeChanged);
        }

        Point getPointFromTemplate(int mousePointX, int mousePointY)
        {
            //计算鼠标所在字符块的位置
            int Lx = (mousePointX - 1) / (BLOCK_W + SPACE);
            int Ly = (mousePointY - 1) / (BLOCK_H + SPACE);

            //不要让计算出的位置超出数组范围
            if (Lx >= TMP_WIDTH)
            {
                Lx = TMP_WIDTH - 1;
            }
            else if (Lx < 0)
            {
                Lx = 0;
            }
            if (Ly >= TMP_HEIGHT)
            {
                Ly = TMP_HEIGHT - 1;
            }
            else if (Ly < 0)
            {
                Ly = 0;
            }
            return new Point(Lx, Ly);
        }

        /// <summary>
        /// 设置一个文本框的状态
        /// </summary>
        /// <param name="text"></param>
        /// <param name="toSelectIt">true:设置为选中,false设置为未选</param>
        private void setTextBoxSelectOrNot(TextBox text, bool toSelectIt)
        {
            text.isSelected = toSelectIt;
            int p1 = text.Rect.X;
            int p2 = text.Rect.X + text.Rect.Width - 1;
            int q1 = text.Rect.Y;
            int q2 = text.Rect.Y + text.Rect.Height - 1;
            for (int i = p1; i <= p2; i++)
            {
                for (int j = q1; j <= q2; j++)
                {
                    blocks[i][j].selected = toSelectIt;
                }
            }

            if (toSelectIt)
            {
                property.SelectedObject = text;
                g_currSelectTextBox = text.ID;
            }
            else
            {
                property.SelectedObject = null;
                g_currSelectTextBox = -1;
            }
        }

        private void drawSelectFrameToTextBox(Graphics g, TextBox txt)
        {
            //pic刷新显示时，如果当前有选中的文本框，则在这个文本框上四角绘制大小调整元件
            //先获得文本框四个顶角的真实坐标
            int ltX = txt.Rect.X * (BLOCK_W + SPACE);
            int ltY = txt.Rect.Y * (BLOCK_H + SPACE);

            int rtX = (txt.Rect.X + txt.Rect.Width) * (BLOCK_W + SPACE);
            int rtY = ltY;

            int lbX = ltX;
            int lbY = (txt.Rect.Y + txt.Rect.Height) * (BLOCK_H + SPACE);

            int rbX = rtX;
            int rbY = lbY;

            //绘制文本框外围的边框
            g.DrawRectangle(new Pen(Color.FromArgb(2,111,208)), ltX, ltY, rtX - ltX, lbY - ltY);
            //绘制文本框四个角的顶点
            g.FillEllipse(new SolidBrush(Color.FromArgb(2, 111, 208)), ltX - 2, ltY - 2, 5, 5);
            g.DrawEllipse(new Pen(Color.White,1), ltX - 2, ltY - 2, 5, 5);
            g.FillEllipse(new SolidBrush(Color.FromArgb(2, 111, 208)), lbX - 2, lbY - 3, 5, 5);
            g.DrawEllipse(new Pen(Color.White,1), lbX - 2, lbY - 3, 5, 5);
            g.FillEllipse(new SolidBrush(Color.FromArgb(2, 111, 208)), rtX - 3, rtY - 2, 5, 5);
            g.DrawEllipse(new Pen(Color.White,1), rtX - 3, rtY - 2, 5, 5);
            g.FillEllipse(new SolidBrush(Color.FromArgb(2, 111, 208)), rbX - 3, rbY - 3, 5, 5);
            g.DrawEllipse(new Pen(Color.White,1), rbX - 3, rbY - 3, 5, 5);

            int lmX = ltX;
            int lmY = ltY + (lbY - ltY) / 2;

            int tmX = ltX + (rtX - ltX) / 2;
            int tmY = ltY;

            int rmX = rtX;
            int rmY = rtY + (rbY - rtY) / 2;

            int bmX = lbX + (rbX - lbX) / 2;
            int bmY = lbY;

            //绘制四边中点圆点
            g.FillEllipse(new SolidBrush(Color.FromArgb(2,111,208)), lmX - 2, lmY - 2, 5, 5);
            g.DrawEllipse(new Pen(Color.White,1), lmX - 2, lmY - 2, 5, 5);
            g.FillEllipse(new SolidBrush(Color.FromArgb(2,111,208)), rmX - 3, rmY - 2, 5, 5);
            g.DrawEllipse(new Pen(Color.White, 1), rmX - 3, rmY - 2, 5, 5);
            if (txt.Rect.Width > 1)
            {
                g.FillEllipse(new SolidBrush(Color.FromArgb(2,111,208)), tmX - 3, tmY - 2, 5, 5);
                g.DrawEllipse(new Pen(Color.White, 1), tmX - 3, tmY - 2, 5, 5);
                g.FillEllipse(new SolidBrush(Color.FromArgb(2,111,208)), bmX - 3, bmY - 3, 5, 5);
                g.DrawEllipse(new Pen(Color.White, 1), bmX - 3, bmY - 3, 5, 5);
            }
        }

        //鼠标事件========================================================================================================
        void pic_MouseMove(object sender, MouseEventArgs e)
        {
            labLocal.Text = string.Format("X={0}, Y={1}", e.X, e.Y);
            //计算鼠标按下时所在的块位置
            Point Locate = getPointFromTemplate(ptMouseDown.X, ptMouseDown.Y);
            //计算鼠标所在字符块的位置
            Point LocateNow = getPointFromTemplate(e.X, e.Y);

            //当前使用的是选择工具时，不显示块光标
            if (toolBtnSelector.Checked)
            {
                if (blocks[LocateNow.X][LocateNow.Y].selected)
                {
                    pic.Cursor = Cursors.SizeAll;
                }
                else
                {
                    if (toolBtnSelector.Checked) { pic.Cursor = Cursors.Default; }
                    else if (toolBtnTextBox.Checked) { pic.Cursor = Cursors.Arrow; }
                }
            }




            pic.Refresh();


            if (!blocks[LocateNow.X][LocateNow.Y].selected)
            {
                //设置标志位：鼠标在当前块上
                blocks[LocateNow.X][LocateNow.Y].isIn = true;
            }

            
            if (mouseDown)
            {
                //鼠标按下时，当前使用的是选择工具时，并且按下时选中了一个文本框
                //此时移动鼠标
                /*if (toolBtnSelector.Checked && g_currSelectTextBox >= 0)
                {
                    if(LocateNow.X==Locate.X && LocateNow.Y==Locate.Y)
                    {
                        return; //位置没变，不移动文本框
                    }

                    //先获取鼠标在选中文本框中的偏移
                    TextBox text = g_TextDic[g_currSelectTextBox];
                    int inPicOffsetLeft = Locate.X - txtRectWhenMdown.X;
                    int inPicOffsetRight = (TMP_WIDTH - 1) - (txtRectWhenMdown.X + txtRectWhenMdown.Width - 1) + Locate.X;
                    int inPicOffsetTop = Locate.Y - txtRectWhenMdown.Y;
                    int inPicOffsetBottom = (TMP_HEIGHT - 1) - (txtRectWhenMdown.Y + txtRectWhenMdown.Height - 1) + Locate.Y;

                    int movedW = LocateNow.X - Locate.X;
                    int movedH = LocateNow.Y - Locate.Y;
                    if (inPicOffsetLeft <= LocateNow.X+movedW && LocateNow.X +movedW <= inPicOffsetRight &&
                        inPicOffsetTop <= LocateNow.Y+movedH && LocateNow.Y+movedH <= inPicOffsetBottom)
                    {
                        int x1 = txtRectWhenMdown.X;
                        int x2 = txtRectWhenMdown.X + txtRectWhenMdown.Width - 1;
                        int y1 = txtRectWhenMdown.Y;
                        int y2 = txtRectWhenMdown.Y + txtRectWhenMdown.Height - 1;
                        //  北
                        //西  东
                        //  南
                        //POS:位置
                        //向东北方向移动
                        if (movedW>=0 && movedH<=0)
                        {
                                for (int j = y1; j <= y2; j++)
                                {
                                    //X轴：从最大POS开始，左一项赋值给右一项  Y轴：从最小POS开始，下面一项赋值给上面一项
                                    blocks[x2 + movedW][j - movedH].UsingID = blocks[x2][j].UsingID;
                                    blocks[x2 + movedW][j - movedH].selected = blocks[x2][j].selected;
                                }
                                for (int j = y1; j <= y2; j++)
                                {
                                    //X轴：从最大POS开始，左一项赋值给右一项  Y轴：从最小POS开始，下面一项赋值给上面一项
                                    blocks[x1][j - movedH].UsingID = blocks[x1 - movedW][j].UsingID;
                                    blocks[x1][j - movedH].selected = blocks[x1 - movedW][j].selected;
                                }
                        }
                        //向东南方向移动
                        else if (movedW>=0 && movedH>=0)
                        {
                            for (int i = x2; i >= x1; i--)
                            {
                                for (int j = y2; j >= y1; j--)
                                {
                                    //X轴：从最大POS开始，左一项赋值给右一项  Y轴：从最大POS开始，上面一项赋值给下面一项
                                    blocks[i + movedW][j + movedH].UsingID = blocks[i][j].UsingID;
                                    blocks[i + movedW][j + movedH].selected = blocks[i][j].selected;
                                }
                            }
                        }
                        //向西南方向移动
                        else if (movedW<=0 && movedH>=0)
                        {
                            for (int i = x1; i <= x2; i++)
                            {
                                for (int j = y2; j >= y1; j--)
                                {
                                    //X轴：从最小POS开始，右一项赋值给左一项  Y轴：从最大POS开始，上面一项赋值给下面一项
                                    blocks[i - movedW][j + movedH].UsingID = blocks[i][j].UsingID;
                                    blocks[i - movedW][j + movedH].selected = blocks[i][j].selected;
                                }
                            }
                        }
                        //向西北方向移动
                        else if (movedW<=0 && movedH<=0)
                        {
                            for (int i = x1; i <= x2; i++)
                            {
                                for (int j = y2; j >= y1; j--)
                                {
                                    //X轴：从最小POS开始，右一项赋值给左一项  Y轴：从最小POS开始，下面一项赋值给上面一项
                                    blocks[i - movedW][j - movedH].UsingID = blocks[i + movedW][j].UsingID;
                                    blocks[i - movedW][j - movedH].selected = blocks[i + movedW][j].selected;
                                }
                            }
                        }
                        text.Rect = new Rectangle(txtRectWhenMdown.X + movedW, txtRectWhenMdown.Y + movedH, txtRectWhenMdown.Width, txtRectWhenMdown.Height);
                    }
                    return;
                }
                */


                if (toolBtnTextBox.Checked)
                {
                    //鼠标在按下的状态时移动，则呈现矩形框
                    if (Locate.X >= LocateNow.X && Locate.Y >= LocateNow.Y)
                    {
                        for (int i = LocateNow.X; i <= Locate.X; i++)
                        {
                            for (int j = LocateNow.Y; j <= Locate.Y; j++) { blocks[i][j].isIn = true; }
                        }
                    }
                    else if (Locate.X >= LocateNow.X && Locate.Y <= LocateNow.Y)
                    {
                        for (int i = LocateNow.X; i <= Locate.X; i++)
                        {
                            for (int j = Locate.Y; j <= LocateNow.Y; j++) { blocks[i][j].isIn = true; }
                        }
                    }
                    else if (Locate.X <= LocateNow.X && Locate.Y >= LocateNow.Y)
                    {
                        for (int i = Locate.X; i <= LocateNow.X; i++)
                        {
                            for (int j = LocateNow.Y; j <= Locate.Y; j++) { blocks[i][j].isIn = true; }
                        }
                    }
                    else if (Locate.X <= LocateNow.X && Locate.Y <= LocateNow.Y)
                    {
                        for (int i = Locate.X; i <= LocateNow.X; i++)
                        {
                            for (int j = Locate.Y; j <= LocateNow.Y; j++) { blocks[i][j].isIn = true; }
                        }
                    }
                }
            }

        }

        void pic_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            pic.Focus();
            //记录鼠标按下时的XY坐标
            ptMouseDown.X = e.X;
            ptMouseDown.Y = e.Y;
            if (toolBtnSelector.Checked && g_currSelectTextBox >= 0)
            {
                TextBox txt = g_TextDic[g_currSelectTextBox];
                txtRectWhenMdown.X = txt.Rect.X;
                txtRectWhenMdown.Y = txt.Rect.Y;
                txtRectWhenMdown.Width = txt.Rect.Width;
                txtRectWhenMdown.Height = txt.Rect.Height;
            }


            //计算鼠标松开时所在字符块的位置
            Point LocateNow = getPointFromTemplate(e.X, e.Y);

            //当使用的是选择工具，点击时判断是否在文本框范围中
            if (toolBtnSelector.Checked)
            {
                //鼠标按下时，如果之前选中了一个文本框，则设置为未选状态
                if (g_currSelectTextBox >= 0)
                {
                    TextBox text = g_TextDic[g_currSelectTextBox];
                    setTextBoxSelectOrNot(text, false);
                    pic.Refresh();
                }

                //如果点击在文本框上时，设置其为选中状态
                if (blocks[LocateNow.X][LocateNow.Y].UsingID >= 0)
                {
                    TextBox text = g_TextDic[blocks[LocateNow.X][LocateNow.Y].UsingID];
                    setTextBoxSelectOrNot(text, true);
                    pic.Refresh();
                }

                return;
            }
            
        }
        void pic_MouseLeave(object sender, EventArgs e)
        {
            mouseDown = false;
            labLocal.Text = "";
            pic.Refresh();
            pic.Refresh();
        }
        void pic_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            if (toolBtnTextBox.Checked)
            {
                bool canUse = true;
                //计算鼠标按下时所在的块位置
                Point Locate = getPointFromTemplate(ptMouseDown.X, ptMouseDown.Y);
                //计算鼠标松开时所在字符块的位置
                Point LocateNow = getPointFromTemplate(e.X, e.Y);
                //鼠标松开时，如果选中的范围内没有被使用的块，则设为占用

                #region  blocks[][]判断是否占用
                if (Locate.X >= LocateNow.X && Locate.Y >= LocateNow.Y)
                {
                    for (int i = LocateNow.X; i <= Locate.X; i++)
                    {
                        for (int j = LocateNow.Y; j <= Locate.Y; j++)
                        {
                            //遍历选择的每一块，如果块都没被用，则设置为占用
                            if (blocks[i][j].UsingID >= 0)
                            {
                                canUse = false;
                                pic.Refresh();
                                return;
                            }
                        }
                    }
                }
                else if (Locate.X >= LocateNow.X && Locate.Y <= LocateNow.Y)
                {
                    for (int i = LocateNow.X; i <= Locate.X; i++)
                    {
                        for (int j = Locate.Y; j <= LocateNow.Y; j++)
                        {
                            //遍历选择的每一块，如果块都没被用，则设置为占用
                            if (blocks[i][j].UsingID >= 0)
                            {
                                canUse = false;
                                pic.Refresh();
                                return;
                            }
                        }
                    }
                }
                else if (Locate.X <= LocateNow.X && Locate.Y >= LocateNow.Y)
                {
                    for (int i = Locate.X; i <= LocateNow.X; i++)
                    {
                        for (int j = LocateNow.Y; j <= Locate.Y; j++)
                        {
                            //遍历选择的每一块，如果块都没被用，则设置为占用
                            if (blocks[i][j].UsingID >= 0)
                            {
                                canUse = false;
                                pic.Refresh();
                                return;
                            }
                        }
                    }
                }
                else if (Locate.X <= LocateNow.X && Locate.Y <= LocateNow.Y)
                {
                    for (int i = Locate.X; i <= LocateNow.X; i++)
                    {
                        for (int j = Locate.Y; j <= LocateNow.Y; j++)
                        {
                            //遍历选择的每一块，如果块都没被用，则设置为占用
                            if (blocks[i][j].UsingID >= 0)
                            {
                                canUse = false;
                                pic.Refresh();
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region 设置block[][]占用
                if (canUse)
                {
                    if (Locate.X >= LocateNow.X && Locate.Y >= LocateNow.Y)
                    {
                        for (int i = LocateNow.X; i <= Locate.X; i++)
                        {
                            for (int j = LocateNow.Y; j <= Locate.Y; j++)
                            {
                                //遍历选择的每一块，如果块都没被用，则设置为占用
                                blocks[i][j].UsingID = g_TxtIDStream;
                            }
                        }
                    }
                    else if (Locate.X >= LocateNow.X && Locate.Y <= LocateNow.Y)
                    {
                        for (int i = LocateNow.X; i <= Locate.X; i++)
                        {
                            for (int j = Locate.Y; j <= LocateNow.Y; j++) { blocks[i][j].UsingID = g_TxtIDStream; }
                        }
                    }
                    else if (Locate.X <= LocateNow.X && Locate.Y >= LocateNow.Y)
                    {
                        for (int i = Locate.X; i <= LocateNow.X; i++)
                        {
                            for (int j = LocateNow.Y; j <= Locate.Y; j++) { blocks[i][j].UsingID = g_TxtIDStream; }
                        }
                    }
                    else if (Locate.X <= LocateNow.X && Locate.Y <= LocateNow.Y)
                    {
                        for (int i = Locate.X; i <= LocateNow.X; i++)
                        {
                            for (int j = Locate.Y; j <= LocateNow.Y; j++) { blocks[i][j].UsingID = g_TxtIDStream; }
                        }
                    }
                }
                #endregion

                //将创建出的文本框保存到全局字典中
                TextBox txtBox = new TextBox();
                txtBox.ID = g_TxtIDStream;
                txtBox.Name = string.Format("新建文本框{0}", g_TxtIDStream);
                txtBox.Rect = new Rectangle(Locate.X, Locate.Y, LocateNow.X - Locate.X + 1, LocateNow.Y - Locate.Y + 1);
                g_TextDic.Add(txtBox.ID, txtBox);
                g_TxtIDStream++;

                //如果再次之前选中的其他的文本框，则使其为未选中状态
                if (g_currSelectTextBox >= 0)
                {
                    setTextBoxSelectOrNot(g_TextDic[g_currSelectTextBox], false);
                }
                //设置文本框为选中，并将对象绑定到property控件中
                setTextBoxSelectOrNot(txtBox, true);
            }
        }

        void pic_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //按下删除和退格键时删除TextBox
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                //如果按键时当前有选中的文本框
                if (g_currSelectTextBox >= 0)
                {
                    TextBox txt = g_TextDic[g_currSelectTextBox];
                    int p1 = txt.Rect.X;
                    int p2 = txt.Rect.X + txt.Rect.Width - 1;
                    int q1 = txt.Rect.Y;
                    int q2 = txt.Rect.Y + txt.Rect.Height - 1;
                    for (int i = p1; i <= p2; i++)
                    {
                        for (int j = q1; j <= q2; j++)
                        {
                            blocks[i][j].UsingID = -1;
                            blocks[i][j].isIn = false;
                        }
                    }
                    //删除这个文本框对象
                    g_TextDic.Remove(g_currSelectTextBox);
                    g_currSelectTextBox = -1;
                    property.SelectedObject = null;
                    pic.Refresh();
                }
            }
        }

        void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            pic.Width = tmpW;
            pic.Height = tmpH;
            for (int i = 0; i < TMP_WIDTH; i++)
            {
                for (int j = 0; j < TMP_HEIGHT; j++)
                {
                    //如果块没有被textBox使用，则清除其在pic中的绘制
                    g.FillRectangle(new SolidBrush(blocks[i][j].color), blocks[i][j].rect);
                    if (blocks[i][j].UsingID < 0)
                    {
                        blocks[i][j].isIn = false;
                    }
                }
            }

            if (g_currSelectTextBox >= 0)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                TextBox txt = g_TextDic[g_currSelectTextBox];
                drawSelectFrameToTextBox(g, txt);
            }
        }


        //事件============================================================================================================
        protected override void OnSizeChanged(EventArgs e)
        {
            splitContainer.Height = this.ClientSize.Height - menu.Height - toolBar.Height - statusBar.Height;
            base.OnSizeChanged(e);
        }

        void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (pic.Height > picBox.Height)
            {
                pic.Top -= (e.NewValue - e.OldValue);
            }
        }

        void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {

            if (pic.Width > picBox.Width)
            {
                pic.Left -= (e.NewValue - e.OldValue);
            }

        }

        void Panel2_SizeChanged(object sender, EventArgs e)
        {
            picBox.Left = picBox.Top = 0;
            picBox.Width = splitContainer.Panel2.ClientSize.Width - vScrollBar.Width;
            picBox.Height = splitContainer.Panel2.Height - hScrollBar.Height;


            pic.Width = tmpW;
            pic.Height = tmpH;
            pic.Left = picBox.Width / 2 - pic.Width / 2 - 3;
            pic.Top = picBox.Height / 2 - pic.Height / 2 - 2;
            if (pic.Left < 0)
            {
                pic.Left = 0;
            }
            if (pic.Top < 0)
            {
                pic.Top = 0;
            }
            pic.Refresh();

            if (pic.Width > picBox.Width)
            {
                hScrollBar.Enabled = true;
            }
            else
            {
                hScrollBar.Enabled = false;
            }

            if (pic.Height > picBox.Height)
            {
                vScrollBar.Enabled = true;
            }
            else
            {
                vScrollBar.Enabled = false;
            }

            vScrollBar.Left = picBox.Left + picBox.Width;
            vScrollBar.Top = picBox.Top;
            vScrollBar.Height = picBox.Height;
            hScrollBar.Top = picBox.Top + picBox.Height;
            hScrollBar.Left = picBox.Left;
            hScrollBar.Width = picBox.Width;
            vScrollBar.Value = 0;
            vScrollBar.LargeChange = picBox.ClientSize.Height;
            vScrollBar.Maximum = pic.Height;
            hScrollBar.Value = 0;
            hScrollBar.LargeChange = picBox.ClientSize.Width;
            hScrollBar.Maximum = pic.Width;
        }

        private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show(
                "确定要退出吗？未保存的数据将会丢失，是否继续？", "确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }




        //菜单按钮动作 ============================================================================================================
        private void toolBtnSelector_Click(object sender, EventArgs e)
        {
            pic.Cursor = System.Windows.Forms.Cursors.Default;
            toolBtnSelector.Checked = true;
            toolBtnTextBox.Checked = false;
        }

        private void toolBtnTextBox_Click(object sender, EventArgs e)
        {
            pic.Cursor = System.Windows.Forms.Cursors.Cross;
            toolBtnSelector.Checked = false;
            toolBtnTextBox.Checked = true;
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("dotTemplate\r\n版本:" + Application.ProductVersion + "\r\n作者:detecyang\r\nCopyright @detecyang 2013", "关于软件", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            pic.Width = tmpW;
            pic.Height = tmpH;
            int newWidth=Convert.ToInt32(txtTempW.Text);
            int newHeight=Convert.ToInt32(txtTempH.Text);
            if (newWidth <= 0 || newHeight <= 0)
            {
                MessageBox.Show("请输入大于0的正整数", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult res = MessageBox.Show(
                "重新设置模板大小会删除当前所有已创建的文本框，是否继续？", "确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //提示用户文本框要清空
            if (res == DialogResult.Yes)
            {
                TMP_WIDTH = newWidth;
                TMP_HEIGHT = newHeight;
                //重新初始化模板的大小和位置
                tmpW = TMP_WIDTH * BLOCK_W + (TMP_WIDTH - 1) * SPACE + 2;
                tmpH = TMP_HEIGHT * BLOCK_H + (TMP_HEIGHT - 1) * SPACE + 2;
                tmpX = 1;
                tmpY = 1;

                pic.Width = tmpW;
                pic.Height = tmpH;

                pic.Left = picBox.Width / 2 - pic.Width / 2 - 3;
                pic.Top = picBox.Height / 2 - pic.Height / 2 - 2;
                if (pic.Left < 0)
                {
                    pic.Left = 0;
                }
                if (pic.Top < 0)
                {
                    pic.Top = 0;
                }

                for (int i = 0; i < TMP_WIDTH; i++)
                {
                    blocks[i] = new Block[TMP_HEIGHT];
                    for (int j = 0; j < TMP_HEIGHT; j++)
                    {
                        blocks[i][j] = new Block();
                        blocks[i][j].rect = new Rectangle(tmpX + (BLOCK_W + SPACE) * i, tmpY + (BLOCK_H + SPACE) * j, BLOCK_W, BLOCK_H);
                    }
                }
                g_TextDic.Clear();
                g_currSelectTextBox = -1;
                property.SelectedObject = null;
                pic.Refresh();
                pic.Focus();
            }
        }

        private void menuNew_Click(object sender, EventArgs e)
        {
            if (g_TextDic.Count > 0)
            {
                DialogResult res = MessageBox.Show(
                "确认要清空工作区吗？这将会删除当前未保存的模板，是否继续？", "确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res != DialogResult.Yes)
                {
                    return;
                }
            }

            //删除所有内容
            TEMPLATE_NAME = "新建模板";
            TMP_WIDTH = 50;
            TMP_HEIGHT = 20;
            txtTempName.Text = TEMPLATE_NAME;
            txtTempW.Text = TMP_WIDTH.ToString();
            txtTempH.Text = TMP_HEIGHT.ToString();
            btnApply.PerformClick();
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "dotTemplate模板文件(*.dtl)|*.dtl|所有文件(*.*)|*.*";
            dlg.InitialDirectory = Application.StartupPath;
            dlg.FileName = TEMPLATE_NAME;
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                string fileName = dlg.FileName;
                Template temp = new Template();
                temp.name = TEMPLATE_NAME;
                temp.width = TMP_WIDTH;
                temp.height = TMP_HEIGHT;
                temp.dicTextBox = g_TextDic;
                bool isSucc = XMLOperate.saveTempToXMLFile(fileName, temp);
                if (!isSucc)
                {
                    MessageBox.Show(string.Format("模板文件 \"{0}\" 保存失败！", fileName), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "dotTemplate模板文件(*.dtl)|*.dtl|所有文件(*.*)|*.*";
            dlg.InitialDirectory = Application.StartupPath;
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                Template temp = XMLOperate.loadXMLToTemp(dlg.FileName);
                TEMPLATE_NAME = temp.name;
                TMP_WIDTH = Convert.ToInt32(temp.width);
                TMP_HEIGHT = Convert.ToInt32(temp.height);
                g_TextDic = temp.dicTextBox;
                g_TxtIDStream = 0;
                g_currSelectTextBox = -1;
                initPic();
                initTextBoxValue();
                foreach (KeyValuePair<int, TextBox> obj in g_TextDic)
                {
                    TextBox txt = obj.Value;
                    if (txt.ID >= g_TxtIDStream)
                    {
                        g_TxtIDStream = txt.ID;
                    }
                    if (txt.isSelected)
                    {
                        g_currSelectTextBox = txt.ID;
                    }

                    int xID = txt.Rect.X + txt.Rect.Width - 1;
                    int yID = txt.Rect.Y + txt.Rect.Height - 1;
                    for (int i = txt.Rect.X; i <= xID; i++)
                    {
                        for (int j = txt.Rect.Y; j <= yID; j++)
                        {
                            blocks[i][j].isIn = true;
                            blocks[i][j].UsingID = txt.ID;
                        }
                    }
                }
                g_TxtIDStream++;
                pic.Refresh();
            }
        }









    }
}
