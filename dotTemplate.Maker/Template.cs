using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotTemplate.Maker
{
    public class Template
    {
        public string name;
        public int width;
        public int height;
        public Dictionary<int, TextBox> dicTextBox;

        public Template()
        {
            name = "";
            width = 0;
            height = 0;
            dicTextBox = new Dictionary<int, TextBox>(10);
        }
    }

}
