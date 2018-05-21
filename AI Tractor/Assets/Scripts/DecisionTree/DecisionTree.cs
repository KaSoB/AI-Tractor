using System;
using System.Collections;
using System.Linq;

public class DecisionTree {
    public DataTable Samples { get; set; }
    private int TotalPositives = 0;
    private int Total = 0;

    private string TargetAttribute = "result";
    private double EntropySet = 0.0F;

    private TreeNode InternalTreeLearn(DataTable samples, Attribute[] attributes, string targetAttribute) {
        // Jeżeli wszystkie przykłady należą do tej samej klasy, to zwróć wierzchołek oznaczony tą klasą.
        if (AllSamplesPositives(samples, targetAttribute) == true)
            return new TreeNode(new Attribute(true));
        // Jeżeli wszystkie przykłady należą do tej samej klasy, to zwróć wierzchołek oznaczony tą klasą.
        if (AllSamplesNegatives(samples, targetAttribute) == true)
            return new TreeNode(new Attribute(false));
        // Jeżeli zbiór atrybutów jest pusty, to zwróć wierzchołek oznaczony klasą, która jest przypisana największej liczbie przykładów
        if (attributes.Length == 0)
            return new TreeNode(new Attribute(GetMostCommonValue(samples, targetAttribute)));

        // Wybierz atrybut A i uczyń go korzeniem drzewa T.
        Total = samples.Rows.Count;
        TargetAttribute = targetAttribute;
        TotalPositives = CountTotalPositives(samples);

        EntropySet = CalculateEntropy(TotalPositives, Total - TotalPositives);

        Attribute bestAttribute = GetBestAttribute(samples, attributes);

        TreeNode root = new TreeNode(bestAttribute);

        DataTable aSample = samples.Clone();
        // Dla każdej wartości W atrybutu A:
        foreach (string value in bestAttribute.Values) {

            aSample.Rows.Clear();

            string[][] rows = samples.Rows.Where(y => y.Contains(value)).ToArray(); // to
            aSample.Rows.AddRange(rows);

            // nowe_przykłady = przykłady, dla których atrybut A przyjmuje wartość W.
            ArrayList aAttributes = new ArrayList(attributes.Length - 1);
            for (int i = 0 ; i < attributes.Length ; i++) {
                if (attributes[i].AttributeName != bestAttribute.AttributeName)
                    aAttributes.Add(attributes[i]);
            }


            if (aSample.Rows.Count == 0) {
                return new TreeNode(new Attribute(GetMostCommonValue(aSample, targetAttribute)));
            } else {
                DecisionTree dc3 = new DecisionTree();
                // Dodaj do T krawędź oznaczoną etykietą W, której wierzchołek docelowy jest korzeniem
                // drzewa zwróconego przez wywołanie
                // treelearn(nowe przykłady, atrybuty − A, nowa klasa domyślna)
                TreeNode ChildNode = dc3.TreeLearn(aSample, (Attribute[]) aAttributes.ToArray(typeof(Attribute)), targetAttribute);
                root.AddTreeNode(ChildNode, value);
            }
        }
        return root;
    }

    // treelearn(przykłady, atrybuty, klasa domyślna)
    public TreeNode TreeLearn(DataTable samples, Attribute[] attributes, string targetAttribute) {
        Samples = samples;
        return InternalTreeLearn(Samples, attributes, targetAttribute);
    }

    private int CountTotalPositives(DataTable samples) {
        int result = 0;

        int index = samples.ColumnPosition(TargetAttribute);
        foreach (var aRow in samples.Rows) {
            if (aRow[index] == "true") // to
                result++;
        }

        return result;
    }
    // I({poz, neg}) = −P(poz)log2(P(poz)) − P(neg)log2(P(neg))
    private double CalculateEntropy(int positives, int negatives) {
        int total = positives + negatives;
        double ratioPositive = (double) positives / total;
        double ratioNegative = (double) negatives / total;

        if (ratioPositive != 0)
            ratioPositive = -(ratioPositive) * System.Math.Log(ratioPositive, 2);
        if (ratioNegative != 0)
            ratioNegative = -(ratioNegative) * System.Math.Log(ratioNegative, 2);

        double result = ratioPositive + ratioNegative;

        return result;
    }

    // E(A) = Suma 1≤i≤k ((pi + ni)/(p + n))I(Ei)
    private double Gain(DataTable samples, Attribute attribute) {
        string[] values = attribute.Values;
        double sum = 0.0;

        for (int i = 0 ; i < values.Length ; i++) {
            int positives, negatives;

            positives = negatives = 0;

            GetValuesToAttribute(samples, attribute, values[i], out positives, out negatives);

            double entropy = CalculateEntropy(positives, negatives);
            sum += -(double) (positives + negatives) / Total * entropy;
        }
        return EntropySet + sum;
    }

    private void GetValuesToAttribute(DataTable samples, Attribute attribute, string value, out int positives, out int negatives) {
        positives = 0;
        negatives = 0;


        int index = samples.ColumnPosition(attribute.AttributeName);
        int index2 = samples.ColumnPosition(TargetAttribute);
        foreach (var aRow in samples.Rows) {
            if (aRow[index] == value) // to
                if (aRow[index2] == "true") // to
                    positives++;
                else
                    negatives++;
        }
    }

    private Attribute GetBestAttribute(DataTable samples, Attribute[] attributes) {
        double maxGain = 0.0;
        Attribute result = null;

        foreach (Attribute attribute in attributes) {
            double aux = Gain(samples, attribute);
            if (aux > maxGain) {
                maxGain = aux;
                result = attribute;
            }
        }

        if (result == null) {
            Console.WriteLine(":(");
            result = attributes[0];
        }
        return result;
    }

    private bool AllSamplesPositives(DataTable samples, string targetAttribute) {
        int index = samples.ColumnPosition(targetAttribute);
        foreach (var row in samples.Rows) {
            if (row[index] == "false")
                return false;
        }

        return true;
    }

    private bool AllSamplesNegatives(DataTable samples, string targetAttribute) {
        int index = samples.ColumnPosition(targetAttribute);
        foreach (var row in samples.Rows) {
            if (row[index] == "true")
                return false;
        }

        return true;
    }

    private ArrayList GetDistinctValues(DataTable samples, string targetAttribute) {
        ArrayList distinctValues = new ArrayList(samples.Rows.Count);


        int index = samples.ColumnPosition(targetAttribute);
        foreach (var row in samples.Rows) {
            if (distinctValues.IndexOf(row[index]) == -1) // to
                distinctValues.Add(row[index]); // to
        }

        return distinctValues;
    }

    private object GetMostCommonValue(DataTable samples, string targetAttribute) {
        ArrayList distinctValues = GetDistinctValues(samples, targetAttribute);
        int[] count = new int[distinctValues.Count];

        int ind = samples.ColumnPosition(targetAttribute);
        foreach (var row in samples.Rows) {
            int index = distinctValues.IndexOf(row[ind]); // to

            count[index]++;
        }

        int MaxIndex = 0;
        int MaxCount = 0;

        for (int i = 0 ; i < count.Length ; i++) {
            if (count[i] > MaxCount) {
                MaxCount = count[i];
                MaxIndex = i;
            }
        }

        return distinctValues[MaxIndex];
    }

}
