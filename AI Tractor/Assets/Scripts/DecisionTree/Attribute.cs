using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DecisionTree {
    public class Attribute {
        public string AttributeName { get; private set; }
        public List<string> Values { get; private set; }
        public object Label { get; private set; }

        public Attribute(string name, List<string> values) {
            AttributeName = name;
            Values = values;
            Values.Sort();
        }

        public Attribute(object label) {
            Label = label;
            AttributeName = string.Empty;
            Values = null;
        }

        public bool IsValidValue(string value) {
            return IndexValue(value) >= 0;
        }

        public int IndexValue(string value) {
            return (Values != null) ? Values.BinarySearch(value) : -1;
        }

        public override string ToString() {
            return (AttributeName != string.Empty) ? AttributeName : Label.ToString();
        }

        public static List<Attribute> ImportFromTxtFile(string filePath) {
            Regex attributeRegex = new Regex(@"^(?i)ATTRIBUTE(?-i)\s+(?<name>.*?)\s+{(?<values>(.+))}$");
            var attributes = new List<Attribute>();
            string inputLine;
            try {
                using (var reader = new StreamReader(File.OpenRead(filePath))) {
                    while (!reader.EndOfStream) {
                        inputLine = reader.ReadLine();

                        if (attributeRegex.IsMatch(inputLine)) {
                            Match match = attributeRegex.Match(inputLine);

                            string name = match.Groups["name"].Value;

                            var values = match
                                .Groups["values"]
                                .Value
                                .Split(new char[] { ',' }, StringSplitOptions.None)
                                .ToList()
                                .Select(x => new String(x.Trim().ToCharArray()))
                                .ToList();

                            attributes.Add(new Attribute(name, values));
                        }
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e);
            } finally {
                Console.WriteLine($"{filePath}: Wczytano {attributes.Count} atrybutów.");
            }
            return attributes;
        }
    }
}