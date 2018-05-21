using System;
using System.Collections;
using System.Text;

public class TreeNode {
    private ArrayList mChilds = null;
    private Attribute mAttribute;

    public TreeNode(Attribute attribute) {
        try {
            if (attribute.Values != null) {
                mChilds = new ArrayList(attribute.Values.Length);
                for (int i = 0 ; i < attribute.Values.Length ; i++)
                    mChilds.Add(null);
            } else {
                mChilds = new ArrayList(1) {
                null
            };
            }
            mAttribute = attribute;
        } catch (Exception e) {
            Console.WriteLine(e.StackTrace);
        }
    }

    public void AddTreeNode(TreeNode treeNode, string ValueName) {
        int index = mAttribute.IndexValue(ValueName);
        mChilds[index] = treeNode;
    }

    public int totalChilds {
        get {
            return mChilds.Count;
        }
    }

    public TreeNode getChild(int index) {
        return (TreeNode) mChilds[index];
    }

    public Attribute attribute {
        get {
            return mAttribute;
        }
    }

    public TreeNode getChildByBranchName(string branchName) {
        int index = mAttribute.IndexValue(branchName);
        return (TreeNode) mChilds[index];
    }

    public static StringBuilder PrintNode(TreeNode root, string tabs = "") {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine(tabs + root.attribute);
        if (root.attribute.Values != null) {
            foreach (var item in root.attribute.Values) {
                builder.AppendLine(tabs + "\t" + item);
                builder.Append(PrintNode(root.getChildByBranchName(item), "\t\t" + tabs));
            }
        }
        return builder;
    }

}