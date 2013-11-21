using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace WinStudio.SqlComparer
{
    public static class NeedExtension
    {
        public static bool IsValid(this DataSet ds)
        {
            if (ds == null) return false;
            if (ds.Tables.Count == 0) return false;
            if (ds.Tables[0].Rows.Count == 0) return false;
            return true;
        }

        public static DataRow GetRow(this DataTable dt, string cname, string cvalue)
        {
            if (dt.Rows.Count == 0) return null;
            foreach (DataRow r in dt.Rows)
            {
                if (r[cname].ToString() == cvalue) return r;
            }
            return null;
        }

        public static bool IsEquals(this DataRow rs, DataRow rt, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (rs[i].ToString() != rt[i].ToString()) return false;
            }
            return true;
        }

        public static DataTable GetTable(this DataSet ds)
        {
            if (ds == null) return null;
            if (ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }
        public static string GetDesc(this DataRow row)
        {
            return string.Format("{0}-{1}-{2}", row["tname"], row["ml"], row["an"]);
        }
        public static string GetTableName(this DataRow row)
        {
            return row["Name"].ToString();
        }

        public static DbTreeNode GetTableNode(this DataRow row)
        {
            return new DbTreeNode(row["cname"].ToString(), row["object_id"].ToString(), row["cname"].ToString());
        }
        public static DbTreeNode GetColumnNode(this DataRow row, Color color)
        {
            return new DbTreeNode(string.Format("{0}  [{1}] : [{2}] : [{3}] : [{4}]", row["cname"], row["tname"], row["ml"], row["isnl"], row["isid"]), row["object_id"].ToString(), row["cname"].ToString()).SetForeground(color);
        }

        public static List<Diff> IsSame(this DataSet ds, DataSet dataset)
        {
            List<Diff> diffs = new List<Diff>();
            if (!ds.IsValid() ||
                !dataset.IsValid())
                return diffs;
            else
            {
                for (int i = 0; i < ds.GetTable().Rows.Count; i++)
                {
                    DataRow rs = ds.GetTable().Rows[i];
                    DataRow rt = dataset.GetTable().GetRow("cname", rs["cname"].ToString());
                    if (rt == null)
                    {
                        diffs.Add(new Diff(rs.GetColumnNode(Color.Blue), null));
                        continue;
                    }
                    if (!rs.IsEquals(rt, 6))
                        diffs.Add(new Diff(rs.GetColumnNode(Color.Red), rt.GetColumnNode(Color.Red)));
                    dataset.GetTable().Rows.Remove(rt);
                }
                foreach (DataRow r in dataset.GetTable().Rows)
                {
                    diffs.Add(new Diff(null, r.GetColumnNode(Color.Gray)));
                }
            }
            return diffs;

        }

        public static DbTreeNode FindNode(this TreeView tree, string dbname)
        {
            foreach (DbTreeNode node in tree.Nodes)
            {
                if (node.DbName == dbname) return node;
            }
            return null;
        }

        public static void Reset(this TreeView tree)
        {
            foreach (DbTreeNode node in tree.Nodes)
            {
                if (node.BackColor != Color.GreenYellow)
                {
                    node.NodeFont = null;
                    node.BackColor = Color.White;
                    node.Nodes.Clear();
                }
            }
        }

        public static void SetNodesDifferent(this TreeView tree)
        {
            foreach (DbTreeNode node in tree.Nodes)
            {
                node.BackColor = Color.GreenYellow;
            }
        }

        public static void CheckTableNodes(this TreeView tree, TreeView target)
        {
            if (tree.Nodes.Count == 0 || target.Nodes.Count == 0) return;
            target.SetNodesDifferent();
            foreach (TreeNode node in tree.Nodes)
            {
                DbTreeNode n = target.FindNode(node.Text);
                if (n != null)
                {
                    n.BackColor = Color.White;
                    node.BackColor = Color.White;
                }
                else node.BackColor = Color.GreenYellow;
            }
        }
    }

    public struct Diff
    {
        public Diff(DbTreeNode a, DbTreeNode b)
        { _a = a; _b = b; }
        private DbTreeNode _a, _b;
        public DbTreeNode A { get { return _a; } }
        public DbTreeNode B { get { return _b; } }
    }
}
