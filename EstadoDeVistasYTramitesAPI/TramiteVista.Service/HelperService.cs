
using System.Data;
using System.Reflection;
using System.Xml;

namespace EstadoDeVistasyTramites.Service
{
    public static class HelperService
    {

        public static DataTable ConvertXmlElementToDataTable(XmlElement xmlElement, string tagName)
        {
            XmlNodeList xmlNodeList = xmlElement.GetElementsByTagName(tagName);

            DataTable dt = new DataTable();
            int TempColumn = 0;
            foreach (XmlNode node in xmlNodeList.Item(0).ChildNodes)
            {
                TempColumn++;
                DataColumn dc = new DataColumn(node.Name.Replace("-", "_"), node.InnerText.GetType());
                if (dt.Columns.Contains(node.Name))
                {
                    dt.Columns.Add(dc.ColumnName = dc.ColumnName + TempColumn.ToString());
                }
                else
                {
                    dt.Columns.Add(dc);
                }
            }
            int ColumnsCount = dt.Columns.Count;
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < ColumnsCount; j++)
                {
                    if (xmlNodeList.Item(i).ChildNodes[j] != null)
                        dr[j] = xmlNodeList.Item(i).ChildNodes[j].InnerText;
                    else
                        dr[j] = "";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }


        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }
}
