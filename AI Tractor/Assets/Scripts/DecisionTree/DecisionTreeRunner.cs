using System;
using System.Collections.Generic;
using System.IO;

namespace DecisionTree {
    /// <summary>
    /// Facade + Singleton
    /// </summary>
    public class DecisionTreeRunner {
        private static readonly Lazy<DecisionTreeRunner> instance = new Lazy<DecisionTreeRunner>(
            () => new DecisionTreeRunner("DecisionTreeData/attributes2.txt", "DecisionTreeData/samples5.csv", "DecisionTreeData/result.txt"));

        public static DecisionTreeRunner Instance => instance.Value;

        private DecisionTreeRunner(string attributeFileName, string sampleFileName, string resultsFileName) {
            attributes = Attribute.ImportFromTxtFile(attributeFileName);
            samples = DataTable.ImportFromCsvFile(sampleFileName);
            decisionTree = new DecisionTreeAlgorithm();
            treeNode = decisionTree.TreeLearn(samples, attributes, "Result");
            this.resultsFileName = resultsFileName;
            SaveLog();
        }


        private List<Attribute> attributes;
        private DataTable samples;
        private DecisionTreeAlgorithm decisionTree;
        private Node treeNode;
        private string resultsFileName;


        public void SaveLog() {
            var printedNodes = Node.PrintNodes(treeNode, "");

            Console.WriteLine(printedNodes);
            File.AppendAllText(resultsFileName, DateTime.Now + Environment.NewLine + printedNodes);
        }


        public bool GetDecision(Dictionary<string, string> fullRow) {
            return Node.GetDecision(treeNode, fullRow);
        }

    }

}
