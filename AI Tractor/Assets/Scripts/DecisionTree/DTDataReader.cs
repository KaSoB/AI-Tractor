
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class DTDataReader {
    public string FileName { get; set; }

    public string DataTableName { get; set; }
    public DataTable DataTable { get; set; }
    public Attribute[] Attributes { get; set; }

    public DTDataReader(string fileName) {
        FileName = fileName;
    }

    private Regex dataTableNameRegex = new Regex(@"^(?i)TABLENAME(?-i)\s+(?<name>.*?)$");
    private Regex attributeRegex = new Regex(@"^(?i)ATTRIBUTE(?-i)\s+(?<name>.*?)\s+{(?<values>(.+))}$");
    private Regex dataTableColumnRegex = new Regex(@"^(?i)TABLECOLUMN(?-i)$");
    private Regex dataTableRowRegex = new Regex(@"^(?i)TABLEROW(?-i)$");

    public void Read() {
        var fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8)) {
            string line;
            while ((line = streamReader.ReadLine()) != null) {
                //   Debug.Log(line);

            }
        }
    }
}

