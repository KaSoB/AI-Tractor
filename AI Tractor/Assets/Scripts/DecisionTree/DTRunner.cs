using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DTRunner {
    public static Attribute[] attributes = FileHandler.LoadAttributes("DecisionTreeData/attributes6.txt");
    public static DataTable samples = FileHandler.ImportFromCsvFile("DecisionTreeData/samples6.csv");
    public static DecisionTree id3 = new DecisionTree();
    public static TreeNode TreeNode = id3.TreeLearn(samples, attributes, "result");

    public static void Run() {
        Debug.Log(TreeNode.PrintNodes(TreeNode, ""));
        File.AppendAllText("DecisionTreeData/result.txt", TreeNode.PrintNodes(TreeNode, "").ToString());
    }
    public static bool Check(Dictionary<string, string> fullRow) {
        return TreeNode.Check(TreeNode, fullRow);
    }

}
