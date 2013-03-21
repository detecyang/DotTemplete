using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;

namespace dotTemplate.SDK.dotNet
{
    public class dotTemplateParser
    {
        private const string NODE_TEMPLATE = "TEMPLATE";
        private const string NODE_TEXTBOX = "TEXTBOX";

        private const string ATTRIBUTE_ID = "ID";
        private const string ATTRIBUTE_NAME = "NAME";
        private const string ATTRIBUTE_ALIGN = "ALIGN";
        private const string ATTRIBUTE_X = "X";
        private const string ATTRIBUTE_Y = "Y";
        private const string ATTRIBUTE_W = "W";
        private const string ATTRIBUTE_H = "H";

        private const string ATTRIBUTE_VALUE_L = "L";
        private const string ATTRIBUTE_VALUE_C = "C";
        private const string ATTRIBUTE_VALUE_R = "R";

        private const char FLAG_DISABLE = char.MaxValue;

        public string name;
        public int width;
        public int height;
        public Dictionary<int,TextBox> textBoxArray;

        public dotTemplateParser()
        {
            name="";
            width=0;
            height=0;
            textBoxArray = new Dictionary<int,TextBox>();
        }

        public dotTemplateParser(string filePath)
        {
            name = "";
            width = 0;
            height = 0;
            textBoxArray = new Dictionary<int,TextBox>();
            loadXmlFile(filePath);
        }

        public bool loadXmlFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            bool ret;
            try
            {
                StreamReader objReader = new StreamReader(filePath,true);
                string strContent = objReader.ReadToEnd();
                objReader.Close();
                ret = loadXmlString(strContent);
            }
            catch
            {
            	ret = false;
            }
            
            return ret;
        }


        public bool loadXmlString(string xmlString)
        {
            if (string.IsNullOrEmpty(xmlString))
            {
                return false;
            }
            
            Template template = new Template();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);

                XmlElement root = doc.DocumentElement;
                XmlElement temp = (XmlElement)root.FirstChild;
                this.name = temp.GetAttribute("NAME");
                this.width = Convert.ToInt32(temp.GetAttribute("W"));
                this.height = Convert.ToInt32(temp.GetAttribute("H"));
                if (temp.HasChildNodes)
                {
                    for (int i=0; i<temp.ChildNodes.Count; i++)
                    {
                        XmlElement text = (XmlElement)temp.ChildNodes[i];
                        TextBox textBox = new TextBox();
                        textBox.ID = Convert.ToInt32(text.GetAttribute("ID"));
                        textBox.Name = text.GetAttribute("NAME");
                        string strAlign = text.GetAttribute("ALIGH");
                        if (strAlign == HorizontalAlignment.Left.ToString())
                        {
                             textBox.Align = HorizontalAlignment.Left;
                        }
                        else if (strAlign == HorizontalAlignment.Right.ToString())
                        {
                             textBox.Align = HorizontalAlignment.Right;
                        }
                        else if (strAlign == HorizontalAlignment.Center.ToString())
                        {
                             textBox.Align = HorizontalAlignment.Center;
                        }
                        int x = Convert.ToInt32(text.GetAttribute("X"));
                        int y = Convert.ToInt32(text.GetAttribute("Y"));
                        int w = Convert.ToInt32(text.GetAttribute("W"));
                        int h = Convert.ToInt32(text.GetAttribute("H"));
                        textBox.Rect = new Rectangle(x,y,w,h);
                        this.textBoxArray.Add(textBox.ID, textBox);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }


        public Dictionary<int,string> ParseStringsWithTextBox()
        {
            if (textBoxArray == null || textBoxArray.Count == 0)
            {
                return null;
            }
            
            //初始化模板上的所有块，并赋初值空格
            char [][]symbolBlocks = new char [width][];
            for (int j=0; j<this.height; j++)
            {
                symbolBlocks[j] = new char[height];
                for (int i=0; i<this.width; i++)
                {
                    symbolBlocks[j][i] = ' ';
                }
            }
            
            
            foreach (TextBox txt in textBoxArray.Values)
            {
                int posValue = 0;  //字符串中遍历到单个字符的位置
                int valueLen = txt.value.Length;
                int y2 = (txt.Rect.Y + txt.Rect.Height - 1);
                int x2 = (txt.Rect.X + txt.Rect.Width - 1);
                
                //遍历所有的文本框，将其填充到symbolBlocks中
                for (int q = txt.Rect.Y; q <= y2; q++)
                {
                    for (int p = txt.Rect.X; p <= x2; p++)
                    {
                        //如果value遍历到当前位置的字符串长度大于文本框的宽度，说明文本框存满了，退出循环。
                        if (posValue+1 > valueLen) { break; }
                        
                        if (dotTemplateParser.isChSymbol(txt.value[posValue]))
                        {
                            if (p == x2)
                            {
                                break;  //如果当前一个汉字要存入文本框中的其中一行的最后一个一个存储位置，则不存，直接存下一行中去
                            }
                            //正常存入一个汉字，设标志位为占用两个块的位置
                            symbolBlocks[q][p] = txt.value[posValue];
                            symbolBlocks[q][p+1] = FLAG_DISABLE;
                            p++;  //文本框中当前存储位置走到下一个
                        }
                        else
                        {
                            symbolBlocks[q][p]=txt.value[posValue];
                        }
                        posValue++;
                    }
                }
                
            }
            
            //将每一行的symbolBlocks块中的字符存入NSString，标记为FLAG_DISABLE的跳过不存。
            Dictionary<int,string> array  = new Dictionary<int,string>(this.height);
            char [][]blocks = new char [width][];
            for (int j=0; j<this.height; j++)
            {
                blocks[j] = new char[height];
                for (int i=0; i<this.width; i++)
                {
                    blocks[j][i] = ' ';
                }
            }

            for (int m = 0; m <= this.height - 1; m++)
            {
                int posNewBlock = 0;
                int posOldBlock = 0;
                while(posOldBlock<this.width)
                {
                    if (symbolBlocks[m][posOldBlock] != FLAG_DISABLE)
                    {
                        blocks[m][posNewBlock] = symbolBlocks[m][posOldBlock];
                        posOldBlock++;  //将原字符块中的字一个个存入新区域，如果
                        posNewBlock++;  //遇到FLAG_DISABLE占位的标记，则跳过。
                    }
                    else
                    {
                        blocks[m][posNewBlock] = '\n';
                        posOldBlock++;
                    }
                }

                //整理好了整个模板的一行数据，存入NSString中。
                string tmpStr = new string(blocks[m],0,posNewBlock);
                array.Add(m,tmpStr);
            }

            return array;
        }



        public static bool isChSymbol(char chr)
        {
            if ((int)chr > 127)
            /*if (chr > 0x4e00 && chr < 0x9fff)*/
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int getCountOfString(string str)
        {
            int num=0;

            for (int i=0,cnt=str.Length;i<cnt;i++)
            {
                if (true == dotTemplateParser.isChSymbol(str[i]))
                {
                    num+=2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }
    }
}
