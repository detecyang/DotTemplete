using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace dotTemplate
{
    class XMLOperate
    {
        static public Template loadXMLToTemp(string fileName)
        {
            Template template = new Template();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);

                XmlElement root = doc.DocumentElement;
                XmlElement temp = (XmlElement)root.FirstChild;
                template.name = temp.GetAttribute("NAME");
                template.width = Convert.ToInt32(temp.GetAttribute("W"));
                template.height = Convert.ToInt32(temp.GetAttribute("H"));
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
                        template.dicTextBox.Add(textBox.ID, textBox);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("无法导入模板，{0}\r\n错误实例:{1}", ex.Message, ex.InnerException), ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return template;
        }




        static public bool saveTempToXMLFile(string fileName, Template temp)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(dec);
                XmlComment commentCh = doc.CreateComment("本文档由dotTemplate程序自动生成，请不要手动修改！");
                XmlComment commentEn = doc.CreateComment("This document was created by dotTemplate, do not modify it manual, please.");
                doc.AppendChild(commentCh);
                doc.AppendChild(commentEn);
                //创建一个根节点（一级）
                XmlElement root = doc.CreateElement("ROOT");
                doc.AppendChild(root);
                //创建节点（二级）
                XmlElement tempNode = doc.CreateElement("TEMPLATE");
                tempNode.SetAttribute("NAME", temp.name);
                tempNode.SetAttribute("W", temp.width.ToString());
                tempNode.SetAttribute("H", temp.height.ToString());

                Dictionary<int, TextBox> dic = temp.dicTextBox;
                int count = dic.Count;
                foreach (KeyValuePair<int,TextBox> obj in dic)
                {
                    XmlElement node = doc.CreateElement("TEXTBOX");
                    TextBox txt = obj.Value;
                    node.SetAttribute("ID", txt.ID.ToString());
                    node.SetAttribute("NAME", txt.Name);
                    node.SetAttribute("X", txt.Rect.X.ToString());
                    node.SetAttribute("Y", txt.Rect.Y.ToString());
                    node.SetAttribute("W", txt.Rect.Width.ToString());
                    node.SetAttribute("H", txt.Rect.Height.ToString());
                    node.SetAttribute("ALIGN", txt.Align.ToString().Substring(0,1).ToUpper());
                    tempNode.AppendChild(node);
                }

                root.AppendChild(tempNode);
                doc.Save(fileName);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("模板保存出现异常，{0}\r\n错误实例:{1}", ex.Message,ex.InnerException), ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }





    }
}
