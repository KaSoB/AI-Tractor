using System.Collections.Generic;

public class DataTable {
    public string DataTableName { get; set; }
    public List<string> Columns { get; set; }
    public List<string[]> Rows { get; set; }

    public DataTable(string name) {
        DataTableName = name;
        Columns = new List<string>();
        Rows = new List<string[]>();
    }

    public int ColumnPosition(string columnName) {
        return Columns.IndexOf(columnName);
    }

    public DataTable Clone() {
        DataTable tmp = new DataTable(DataTableName);
        tmp.Columns.AddRange(Columns);
        tmp.Rows.AddRange(Rows);
        return tmp;
    }
}