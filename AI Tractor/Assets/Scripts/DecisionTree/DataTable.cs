using System.Collections.Generic;

public class DataTable {
    public string DataTableName { get; set; }
    public List<string> Columns { get; set; } = new List<string>();
    public List<string[]> Rows { get; set; } = new List<string[]>();

    public DataTable() {

    }
    public DataTable(string name) {
        DataTableName = name;
    }

    public int ColumnPosition(string columnName) {
        return Columns.IndexOf(columnName);
    }

    public static DataTable Clone(DataTable dataTable) {
        DataTable tmp = new DataTable(dataTable.DataTableName);
        tmp.Columns = dataTable.Columns.GetRange(0, dataTable.Columns.Count);
        tmp.Rows = dataTable.Rows.GetRange(0, dataTable.Rows.Count);

        return tmp;
    }
}