using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DragDropTreeViewSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            treeView1.ExpandAll();
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            treeView1.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode aNode;
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point apoint;
                TreeNode TragetNode;
                apoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TragetNode = ((TreeView)sender).GetNodeAt(apoint);
                int TargetNodeIndex = TragetNode.Parent.Nodes.IndexOf(TragetNode);

                aNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                TreeNode parentNode = treeView1.Nodes.Find(aNode.Parent.Text, true)[0];
                if (TragetNode.Parent == aNode.Parent)
                {
                    //Take backup of parentnode
                    TreeNode parentNodes = (TreeNode)aNode.Parent.Clone();
                    TreeNodeCollection treeNodeCollection = parentNodes.Nodes;
                    parentNode.Nodes.Clear();

                    List<TreeNode> testlist = new List<TreeNode>();
                    foreach (TreeNode item in treeNodeCollection)
                    {
                        if (item.Text != aNode.Text)
                            testlist.Add(item);
                    }
                    testlist.Insert(TargetNodeIndex, aNode);

                    foreach (var item in testlist)
                    {
                        parentNode.Nodes.Add(item);
                    }



                    //if (TragetNode != null && TragetNode != aNode)
                    //{
                    //    TragetNode.Nodes.Add((TreeNode)aNode.Clone());
                    //    TragetNode.Expand();
                    //    aNode.Remove();
                    //}
                }

            }
        }

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            var selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {

                selectedNode.Nodes.Add(txtNode.Text);
                var childs = selectedNode.Nodes;
                //List<string> treeNodes = new List<string>();
                //foreach (TreeNode item in childs)
                //{
                //    treeNodes.Add(item.Text);
                //}
                //treeNodes.Sort();
                string[] treeNodes = new string[childs.Count];
                for (int i = 0; i < childs.Count; i++)
                {
                    treeNodes[i] = childs[i].Text;
                }
                Array.Sort(treeNodes, new AlphanumComparatorFast());
                selectedNode.Nodes.Clear();
                foreach (var item in treeNodes)
                {
                    selectedNode.Nodes.Add(item);
                }


                selectedNode.Expand();

            }
            else
            {
                MessageBox.Show("Please selecte node");
            }
        }
    }
}
