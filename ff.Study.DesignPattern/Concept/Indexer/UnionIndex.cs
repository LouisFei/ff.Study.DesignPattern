using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ff.Study.DesignPattern.Concept.Indexer
{
    /*
     联合索引
     */

    /// <summary>
    /// 具胡联合索引特点的实体类型
    /// </summary>
    public struct Employee
    {
        public string FirstName; // PK Field
        public string FamilyName; // PK Field
        public string Title;

        public Employee(DataRow row)
        {
            this.FirstName = row["FirstName"] as string;
            this.FamilyName = row["FamilyName"] as string;
            this.Title = row["Title"] as string;
        }
    }

    public class Staff
    {
        static DataTable data = new DataTable();

        /// <summary>
        /// 数据准备
        /// </summary>
        /// <remarks>
        /// 实际数据应该会从数据库等持久层渠道获得
        /// </remarks>
        static Staff()
        {
            data.Columns.Add("FirstName");
            data.Columns.Add("FamilyName");
            data.Columns.Add("Title");

            // pk : familyname + firstname
            data.PrimaryKey = new DataColumn[] { data.Columns[0], data.Columns[1] };

            data.Rows.Add("Jane", "Doe", "Sales Manager");
            data.Rows.Add("John", "Doe", "Vice President");
            data.Rows.Add("Rupert", "Muck", "President");
            data.Rows.Add("John", "Smith", "Logistics Engineer");
        }

        /// <summary>
        /// 基于联合PK检索
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="familyName"></param>
        /// <returns></returns>
        public Employee this[string firstName, string familyName]
        {
            get
            {
                DataRow row = data.Rows.Find(new object[] { firstName, familyName });
                return new Employee(row);
            }
        }
    }
}
