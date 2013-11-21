using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinStudio.SqlComparer;

namespace WinStudio.SqlCoparer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbConnector dbA, dbB;
        [Import(typeof(IPrinter))]
        public Importer printer { get; set; }
        private void btnConnectA_Click(object sender, EventArgs e)
        {
            printer.Log("Connecting Database...");
            try
            {
                dbA = new DbConnector(this.tbServerA.Text, this.tbUsernameA.Text, this.tbPasswordA.Text.Trim());
                bool ret = dbA.Open();
                if (!ret) ShowMsgBox("Connected database A failed!");

                DataSet ds = dbA.GetDatabases();
                if (!ds.IsValid()) ShowMsgBox("none table in database A!");
                cbDbListA.Items.Clear();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cbDbListA.Items.Add(row.GetTableName());
                }
            }
            catch (Exception exp)
            {
                ShowMsgBox(exp.Message);
            }
        }

        private void btnConnectB_Click(object sender, EventArgs e)
        {
            try
            {
                dbB = new DbConnector(this.tbServerB.Text, this.tbUsernameB.Text, this.tbPasswordB.Text.Trim());
                bool ret = dbB.Open();
                if (!ret) ShowMsgBox("Connected database B failed!");

                DataSet ds = dbB.GetDatabases();
                if (!ds.IsValid()) ShowMsgBox("none table in database B！");
                cbDbListB.Items.Clear();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cbDbListB.Items.Add(row.GetTableName());
                }
            }
            catch (Exception exp)
            {
                ShowMsgBox(exp.Message);
            }
        }

        private void cbDbListA_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                treeA.Nodes.Clear();
                lbATableCount.Text = "";
                string database = cbDbListA.SelectedItem.ToString();
                bool ret = dbA.Open();
                DataSet ds = dbA.GetDataTables(database);
                if (!ds.IsValid()) ShowMsgBox("none table in {0}!", new object[] { database });
                lbATableCount.Text = ds.Tables[0].Rows.Count + "tables";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    treeA.Nodes.Add(row.GetTableNode());
                }
                treeA.CheckTableNodes(treeB);
                chbAAll.Checked = false;
            }
            catch (Exception exp)
            {
                ShowMsgBox(exp.Message);
            }
        }
        private void cbDbListB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                treeB.Nodes.Clear();
                lbBTableCount.Text = "";
                string database = cbDbListB.SelectedItem.ToString();
                bool ret = dbB.Open();
                DataSet ds = dbB.GetDataTables(database);
                if (!ds.IsValid()) ShowMsgBox("none table in {0}!", new object[] { database });
                lbBTableCount.Text = ds.Tables[0].Rows.Count + " tables";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    treeB.Nodes.Add(row.GetTableNode());
                }
                treeB.CheckTableNodes(treeA);
            }
            catch (Exception exp)
            {
                ShowMsgBox(exp.Message);
            }
        }

        private void chbAAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in treeA.Nodes)
            {
                node.Checked = chbAAll.Checked;
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDbListA.SelectedIndex == -1 || cbDbListB.SelectedIndex == -1)
                {
                    ShowMsgBox("Please select 2 Databases from A and B to compare!");
                    return;
                }

                string databaseA = cbDbListA.SelectedItem.ToString();
                string databaseB = cbDbListB.SelectedItem.ToString();
                int counter = 0;
                treeA.Reset();
                treeB.Reset();
                foreach (DbTreeNode node in treeA.Nodes)
                {
                    if (node.Checked)
                    {
                        DbTreeNode n = node as DbTreeNode;
                        DataSet tableA = dbA.GetDataTableColumsInfo(databaseA, n.DbName);
                        DataSet tableB = dbB.GetDataTableColumsInfo(databaseB, n.DbName);
                        List<Diff> diffs = tableA.IsSame(tableB);
                        if (diffs.Count == 0) continue;
                        counter += diffs.Count;
                        node.SetBackground();
                        foreach (var diff in diffs)
                        {
                            if (diff.A != null)
                                node.Nodes.Add(diff.A);
                        }
                        node.Expand();

                        DbTreeNode nb = treeB.FindNode(n.DbName);
                        if (nb != null)
                        {
                            nb.SetBackground();
                            foreach (var diff in diffs)
                            {
                                if (null != diff.B)
                                    nb.Nodes.Add(diff.B);
                            }
                            nb.Expand();
                        }
                    }
                }
                ShowMsgBox("Finished, and find  " + counter + " different!");
            }
            catch (Exception exp)
            {
                ShowMsgBox(exp.Message);
            }
        }

        private void ShowMsgBox(string msg)
        {
            MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void ShowMsgBox(string msg, object[] args)
        {
            MessageBox.Show(string.Format(msg, args), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
