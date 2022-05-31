using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragDropTreeViewSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            if(e.Data.GetDataPresent("System.Windows.Forms.TreeNode",false))
            {
                Point apoint;
                TreeNode TragetNode;
                apoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TragetNode = ((TreeView)sender).GetNodeAt(apoint);
                aNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                TragetNode.Nodes.Add((TreeNode)aNode.Clone());
                TragetNode.Expand();
                aNode.Remove();
            }
        }
    }
}
