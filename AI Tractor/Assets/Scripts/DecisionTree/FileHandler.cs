using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


public static class FileHandler {
    private static Regex attributeRegex = new Regex(@"^@(?i)ATTRIBUTE(?-i)\s+(?<name>.*?)\s+{(?<values>(.+))}$");

    public static DataTable ImportFromCsvFile(string filePath) {
        var columnLineLoaded = false;
        var data = new DataTable("samples");

        try {
            using (var reader = new StreamReader(File.OpenRead(filePath))) {
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    var values = line.Substring(0, line.Length).Split(';');


                    if (columnLineLoaded) {
                        data.Rows.Add(values);
                    }
                    if (!columnLineLoaded) {
                        foreach (var item in values) {
                            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item)) {
                                throw new Exception("Value can't be empty");
                            }
                            data.Columns.Add(item);
                        }
                        columnLineLoaded = true;
                    }

                    if (values.Length != data.Columns.Count) {
                        throw new Exception("Row is shorter or longer than title row");
                    }
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"\n{ex}\n");
            data = null;
        }

        return data?.Rows.Count > 0 ? data : null;
    }
    public static Attribute[] LoadAttributes(string filePath) {
        var attributes = new List<Attribute>();
        string line = null;
        try {
            using (var reader = new StreamReader(File.OpenRead(filePath))) {
                while ((line = reader.ReadLine()) != null) {

                    if (attributeRegex.IsMatch(line)) {
                        Match match = attributeRegex.Match(line);

                        string name = match.Groups["name"].Value;

                        string[] values = match
                            .Groups["values"]
                            .Value
                            .Split(new char[] { ',' }, StringSplitOptions.None)
                            .ToList()
                            .Select(x => new String(x.Trim().ToCharArray()))
                            .ToArray();

                        attributes.Add(new Attribute(name, values));
                    }
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"\n{ex}\n");
        }
        return attributes.ToArray();
    }
}