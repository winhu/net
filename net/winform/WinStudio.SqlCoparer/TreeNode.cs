using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WinStudio.SqlComparer
{
    public class DbTreeNode : TreeNode
    {
        public string DbId { get; set; }
        public string DbName { get; set; }
        public DbTreeNode(string text, string id)
        {
            base.Text = text;
            DbId = id;
        }
        public DbTreeNode(string text, string id, string name)
        {
            base.Text = text;
            DbId = id;
            DbName = name;
        }
        public DbTreeNode SetForeground(Color color)
        {
            this.ForeColor = color;
            return this;
        }

        public void SetBackground()
        {
            this.BackColor = Color.OrangeRed;
        }
    }
}
