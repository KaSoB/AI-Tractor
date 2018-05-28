using System;
using System.Collections;
using System.Collections.Generic;
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

    public int TotalChilds {
        get {
            return mChilds.Count;
        }
    }

    public TreeNode GetChild(int index) {
        return (TreeNode) mChilds[index];
    }

    public Attribute Attribute {
        get {
            return mAttribute;
        }
    }

    public TreeNode GetChildByBranchName(string branchName) {
        int index = mAttribute.IndexValue(branchName);
        return (TreeNode) mChilds[index];
    }

    public static bool Check(TreeNode treeNode, Dictionary<string, string> info) {
        int totalChilds = treeNode.TotalChilds;
        string ret;
        for (int i = 0 ; i < totalChilds ; i++) {
            if (info.TryGetValue(treeNode.Attribute.ToString(), out ret)) {
                TreeNode child = treeNode.GetChildByBranchName(ret);
                if (child != null) {
                    if (child.Attribute.ToString().ToLower() == "true") {
                        return true;
                    } else if (child.Attribute.ToString().ToLower() == "false") {
                        return false;
                    } else {
                        return Check(child, info);
                    }
                }
            }
        }
        return false;
    }

    public static StringBuilder PrintNodes(TreeNode root, string tabs = "") {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine(tabs + root.Attribute);
        if (root.Attribute.Values != null) {
            foreach (var item in root.Attribute.Values) {
                builder.AppendLine(tabs + " " + item);
                builder.Append(PrintNodes(root.GetChildByBranchName(item), "  " + tabs));
            }
        }
        return builder;
    }

}