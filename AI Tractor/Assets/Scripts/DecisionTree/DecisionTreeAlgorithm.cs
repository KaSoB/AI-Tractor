using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTree {
    public class DecisionTreeAlgorithm {
        private DataTable Samples;
        private int Total;
        private int TotalPositives;
        private double EntropySet;
        private string TargetAttribute;

        // treelearn(przykłady, atrybuty, klasa domyślna)
        public Node TreeLearn(DataTable samples, List<Attribute> attributes, string targetAttribute) {
            Samples = samples;
            return RunID3Algorithm(Samples, attributes, targetAttribute);
        }

        private Node RunID3Algorithm(DataTable samples, List<Attribute> attributes, string targetAttribute) {
            // Jeżeli wszystkie przykłady należą do tej samej klasy, to zwróć wierzchołek oznaczony tą klasą.
            if (AllSamplesPositives(samples, targetAttribute)) {
                return new Node(new Attribute(true));
            }
            // Jeżeli wszystkie przykłady należą do tej samej klasy, to zwróć wierzchołek oznaczony tą klasą.
            if (AllSamplesNegatives(samples, targetAttribute)) {
                return new Node(new Attribute(false));
            }
            // Jeżeli zbiór atrybutów jest pusty, to zwróć wierzchołek oznaczony klasą, która jest przypisana największej liczbie przykładów
            if (!attributes.Any()) {
                return new Node(new Attribute(GetMostCommonValue(samples, targetAttribute)));
            }

            TargetAttribute = targetAttribute;

            Total = samples.Rows.Count;
            TotalPositives = CountTotalPositives(samples);
            EntropySet = CalculateEntropy(positives: TotalPositives, negatives: Total - TotalPositives);

            // Wybierz atrybut A i uczyń go korzeniem drzewa T.
            Attribute bestAttribute = GetBestAttribute(samples, attributes);

            Node root = new Node(bestAttribute);
            if (bestAttribute == null)
                return root;

            DataTable aSample = DataTable.Clone(Samples);

            // Dla każdej wartości W atrybutu A:
            foreach (string value in bestAttribute.Values) {

                aSample.Rows.Clear();
                aSample.Rows.AddRange(samples.Rows.Where(y => y.Contains(value)).ToArray());

                // nowe_przykłady = przykłady, dla których atrybut A przyjmuje wartość W.
                var aAttributes = attributes.Where(it => it.AttributeName != bestAttribute.AttributeName).ToList();

                if (aSample.Rows.Count == 0) {
                    root.AddNode(new Node(new Attribute(GetMostCommonValue(samples, targetAttribute))), value);
                } else {
                    // Dodaj do T krawędź oznaczoną etykietą W, której wierzchołek docelowy jest korzeniem
                    // drzewa zwróconego przez wywołanie
                    // treelearn(nowe przykłady, atrybuty − A, nowa klasa domyślna)
                    DecisionTreeAlgorithm dc3 = new DecisionTreeAlgorithm();
                    Node ChildNode = dc3.TreeLearn(aSample, aAttributes, targetAttribute);
                    root.AddNode(ChildNode, value);
                }
            }
            return root;
        }

        private int CountTotalPositives(DataTable samples) {
            int index = samples.ColumnPosition(TargetAttribute);
            return samples.Rows.Count(it => it[index] == "true");
        }

        // I({poz, neg}) = −P(poz)log2(P(poz)) − P(neg)log2(P(neg))
        private double CalculateEntropy(int positives, int negatives) {
            int total = positives + negatives;
            if (total == 0) {
                return 0F;
            }
            double ratioPositive = (double) positives / total;
            double ratioNegative = (double) negatives / total;

            if (ratioPositive != 0) {
                ratioPositive = -(ratioPositive) * Math.Log(ratioPositive, 2);
            }
            if (ratioNegative != 0) {
                ratioNegative = -(ratioNegative) * Math.Log(ratioNegative, 2);
            }
            return ratioPositive + ratioNegative;
        }

        // E(A) = Suma 1≤i≤k ((pi + ni)/(p + n))I(Ei)
        private double Gain(DataTable samples, Attribute attribute) {
            var values = attribute.Values;
            double sum = 0.0;
            int positives, negatives;
            for (int i = 0 ; i < values.Count ; i++) {
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
                if (aRow[index] == value)
                    if (aRow[index2] == "true")
                        positives++;
                    else
                        negatives++;
            }
        }

        private Attribute GetBestAttribute(DataTable samples, List<Attribute> attributes) {
            double maxGain = double.MinValue;
            Attribute result = null;

            foreach (Attribute attribute in attributes) {
                double aux = Gain(samples, attribute);
                if (aux > maxGain) {
                    maxGain = aux;
                    result = attribute;
                }
            }

            return result;
        }

        private bool AllSamplesPositives(DataTable samples, string targetAttribute) {
            int index = samples.ColumnPosition(targetAttribute);
            if (samples.Rows.Any(it => it[index] == "false")) {
                return false;
            }
            return true;
        }

        private bool AllSamplesNegatives(DataTable samples, string targetAttribute) {
            int index = samples.ColumnPosition(targetAttribute);
            if (samples.Rows.Any(it => it[index] == "true")) {
                return false;
            }
            return true;
        }

        private List<string> GetDistinctValues(DataTable samples, string targetAttribute) {
            var distinctValues = new List<string>(samples.Rows.Count);
            int index = samples.ColumnPosition(targetAttribute);
            foreach (var row in samples.Rows) {
                if (distinctValues.IndexOf(row[index]) == -1) {
                    distinctValues.Add(row[index]);
                }
            }
            return distinctValues;
        }

        private object GetMostCommonValue(DataTable samples, string targetAttribute) {
            var distinctValues = GetDistinctValues(samples, targetAttribute);
            int[] count = new int[distinctValues.Count];

            int ind = samples.ColumnPosition(targetAttribute);
            foreach (var row in samples.Rows) {
                int index = distinctValues.IndexOf(row[ind]);

                count[index]++;
            }

            int maxIndex = 0;
            int maxCount = 0;
            for (int i = 0 ; i < count.Length ; i++) {
                if (count[i] > maxCount) {
                    maxCount = count[i];
                    maxIndex = i;
                }
            }

            return distinctValues[maxIndex];
        }

    }

}
