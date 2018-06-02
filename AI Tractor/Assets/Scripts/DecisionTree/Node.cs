using System;
using System.Collections.Generic;
using System.Text;
namespace DecisionTree {
    public class Node {
        private List<Node> Childs = new List<Node>();
        public Attribute Attribute { get; set; }
        public Node(Attribute attribute) {
            if (attribute.Values != null) {
                for (int i = 0 ; i < attribute.Values.Count ; i++) {
                    Childs.Add(null);
                }
            } else {
                Childs.Add(null);
            }
            Attribute = attribute;
        }

        public void AddNode(Node node, string valueName) {
            int index = Attribute.IndexValue(valueName);
            Childs[index] = node;
        }

        public int TotalChilds {
            get {
                return Childs.Count;
            }
        }

        public Node GetChild(int index) {
            return Childs[index];
        }

        public Node GetChildByBranchName(string branchName) {
            int index = Attribute.IndexValue(branchName);
            return Childs[index];
        }

        public static bool GetDecision(Node treeNode, Dictionary<string, string> info) {
            int totalChilds = treeNode.TotalChilds;
            string ret;
            for (int i = 0 ; i < totalChilds ; i++) {
                if (info.TryGetValue(treeNode.Attribute.ToString(), out ret)) {
                    Node child = treeNode.GetChildByBranchName(ret);
                    if (child != null) {
                        if (child.Attribute.ToString().ToLower() == "true") {
                            return true;
                        } else if (child.Attribute.ToString().ToLower() == "false") {
                            return false;
                        } else {
                            return GetDecision(child, info);
                        }
                    }
                }
            }
            return false;
        }

        public static String PrintNodes(Node root, string separator = "") {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{separator} [{root.Attribute}]");
            if (root.Attribute.Values != null) {
                foreach (var item in root.Attribute.Values) {
                    builder.AppendLine($"{separator}  {item}");
                    builder.Append(PrintNodes(root.GetChildByBranchName(item), $"    {separator}"));
                }
            }
            return builder.ToString();
        }

    }
}
