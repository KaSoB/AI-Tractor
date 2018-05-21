using Assets.Scripts.UnityScripts;
using System.Collections.Generic;

public class DTExample {
    public void Run() {
        var example1 = LoadExample1();
        TreeNode root = new DecisionTree().TreeLearn(example1.Value, example1.Key, "result");
        Files.AppendAllText("DecisionTreeResults1.txt", TreeNode.PrintNode(root).ToString());
    }

    KeyValuePair<Attribute[], DataTable> LoadExample1() {

        Attribute[] attributes = new Attribute[] {
            new Attribute("type", new string[] { "WheatField", "CarrotField", "CornField" }),
            new Attribute("humidity", new string[] { "1-2", "3", "4-5" })
        };

        DataTable result = new DataTable("samples");

        result.Columns.Add("type");
        result.Columns.Add("humidity");
        result.Columns.Add("result");

        result.Rows.Add(new string[] { "CarrotField", "1-2", "false" });
        result.Rows.Add(new string[] { "CornField", "3", "false" });
        result.Rows.Add(new string[] { "WheatField", "1-2", "false" });
        result.Rows.Add(new string[] { "CarrotField", "4-5", "true" });
        result.Rows.Add(new string[] { "WheatField", "1-2", "false" });
        result.Rows.Add(new string[] { "CarrotField", "4-5", "true" });
        result.Rows.Add(new string[] { "CornField", "1-2", "false" });
        result.Rows.Add(new string[] { "WheatField", "3", "false" });
        result.Rows.Add(new string[] { "CornField", "1-2", "false" });
        result.Rows.Add(new string[] { "WheatField", "3", "false" });
        result.Rows.Add(new string[] { "WheatField", "4-5", "true" });
        result.Rows.Add(new string[] { "CarrotField", "4-5", "true" });
        result.Rows.Add(new string[] { "CornField", "1-2", "false" });
        result.Rows.Add(new string[] { "WheatField", "4-5", "true" });

        return new KeyValuePair<Attribute[], DataTable>(attributes, result);
    }
    KeyValuePair<Attribute[], DataTable> LoadExample2() {

        Attribute[] attributes = new Attribute[] {
            new Attribute("type", new string[] { "WheatField", "CarrotField", "CornField" }),
            new Attribute("humidity", new string[] { "1-2", "3", "4-5" }),
            new Attribute("fertility", new string[] { "1", "2-4", "5" }),
            new Attribute("acidity", new string[] { "1", "2-4", "5" }),
            new Attribute("pollution", new string[] { "1-3", "4-5" })
        };

        DataTable result = new DataTable("samples");

        result.Columns.Add("type");
        result.Columns.Add("humidity");
        result.Columns.Add("fertility");
        result.Columns.Add("acidity");
        result.Columns.Add("pollution");
        result.Columns.Add("result");

        result.Rows.Add(new string[] { "CarrotField", "1-2", "1", "2-4", "1-3", "true" });
        result.Rows.Add(new string[] { "CornField", "3", "2-4", "5", "4-5", "false" });
        result.Rows.Add(new string[] { "WheatField", "1-2", "1", "5", "4-5", "true" });
        result.Rows.Add(new string[] { "CarrotField", "4-5", "1", "2-4", "1-3", "true" });
        result.Rows.Add(new string[] { "WheatField", "1-2", "5", "5", "4-5", "true" });
        result.Rows.Add(new string[] { "CarrotField", "4-5", "2-4", "2-4", "1-3", "true" });
        result.Rows.Add(new string[] { "CornField", "1-2", "5", "2-4", "4-5", "true" });
        result.Rows.Add(new string[] { "WheatField", "3", "1", "5", "4-5", "false" });
        result.Rows.Add(new string[] { "CornField", "1-2", "2-4", "5", "4-5", "true" });
        result.Rows.Add(new string[] { "WheatField", "3", "2-4", "2-4", "4-5", "true" });
        result.Rows.Add(new string[] { "WheatField", "4-5", "1", "5", "4-5", "true" });
        result.Rows.Add(new string[] { "CarrotField", "4-5", "5", "2-4", "4-5", "true" });
        result.Rows.Add(new string[] { "CornField", "1-2", "1", "5", "4-5", "false" });
        result.Rows.Add(new string[] { "WheatField", "4-5", "2-4", "5", "1-3", "false" });

        return new KeyValuePair<Attribute[], DataTable>(attributes, result);
    }
    KeyValuePair<Attribute[], DataTable> LoadExample3() {

        Attribute[] attributes = new Attribute[] {
            new Attribute("outlook", new string[] { "sunny", "overcast", "rainy" }),
            new Attribute("temperature", new string[] { "hot", "mild", "cool" }),
            new Attribute("humidity", new string[] { "high", "normal"}),
            new Attribute("windy", new string[] { "high", "normal", "4-5" })
        };

        DataTable result = new DataTable("samples");

        result.Columns.Add("outlook");
        result.Columns.Add("temperature");
        result.Columns.Add("humidity");
        result.Columns.Add("windy");
        result.Columns.Add("result");

        result.Rows.Add(new string[] { "sunny", "hot", "high", "false", "no" });
        result.Rows.Add(new string[] { "sunny", "hot", "high", "false", "no" });
        result.Rows.Add(new string[] { "overcast", "hot", "high", "false", "yes" });
        result.Rows.Add(new string[] { "rainy", "mild", "high", "false", "yes" });
        result.Rows.Add(new string[] { "rainy", "cool", "normal", "false", "yes" });
        result.Rows.Add(new string[] { "rainy", "cool", "normal", "true", "no" });
        result.Rows.Add(new string[] { "overcast", "cool", "normal", "true", "yes" });
        result.Rows.Add(new string[] { "sunny", "mild", "high", "false", "no" });
        result.Rows.Add(new string[] { "sunny", "cool", "normal", "false", "yes" });
        result.Rows.Add(new string[] { "rainy", "mild", "normal", "false", "yes" });
        result.Rows.Add(new string[] { "sunny", "mild", "normal", "true", "yes" });
        result.Rows.Add(new string[] { "overcast", "mild", "high", "true", "yes" });
        result.Rows.Add(new string[] { "overcast", "hot", "normal", "false", "yes" });
        result.Rows.Add(new string[] { "rainy", "mild", "high", "true", "no" });


        return new KeyValuePair<Attribute[], DataTable>(attributes, result);
    }

}
