using System;
using System.Collections.Generic;
using System.IO;

namespace DecisionTree {
    public class DataTable {
        public string DataTableName { get; set; }
        public List<string> Columns { get; set; } = new List<string>();
        public List<string[]> Rows { get; set; } = new List<string[]>();

        public DataTable() {
            DataTableName = "Table";
        }

        public DataTable(string name) {
            DataTableName = name;
        }

        public int ColumnPosition(string columnName) {
            return Columns.IndexOf(columnName);
        }

        public static DataTable Clone(DataTable dataTable) {
            return new DataTable(dataTable.DataTableName) {
                Columns = dataTable.Columns.GetRange(0, dataTable.Columns.Count),
                Rows = dataTable.Rows.GetRange(0, dataTable.Rows.Count)
            };
        }

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
            } finally {
                Console.WriteLine($"{filePath}: Wczytano {data.Rows.Count} wierszy i {data.Columns.Count} kolumn.");
            }

            return data?.Rows.Count > 0 ? data : null;
        }
    }
}