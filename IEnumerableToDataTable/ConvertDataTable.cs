using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DropDownControls.IEnumerableToDataTable
{
    //Excerpted from http://weblogs.asp.net/stevesloka/linq-to-datatable

    /// <summary>
    /// Convert IEnumerable to DataTable
    /// </summary>
    static public class ConvertDataTable
    {

        /// <summary>
        /// T
        /// </summary>
        /// <param name="varlist">the IEnumerable list</param>
        /// <param name="fn"> function to create new object such as rec => new object[] {query}</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>DataTable Object</returns>
        public static DataTable ToAdoTable<T>(this IEnumerable<T> varlist, CreateRowDelegate<T> fn)
        {

            var dtReturn = new DataTable();
            // Could add a check to verify that there is an element 0

            var enumerable = varlist as T[] ?? varlist.ToArray();
            T topRec = enumerable.ElementAt(0);

            // Use reflection to get property names, to create table

            // column names

            var oProps = topRec.GetType().GetProperties();

            foreach (var pi in oProps)
            {

                var colType = pi.PropertyType; if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {

                    colType = colType.GetGenericArguments()[0];

                }

                dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
            }

            foreach (var rec in enumerable)
            {

                var dr = dtReturn.NewRow(); foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                }
                dtReturn.Rows.Add(dr);

            }

            return (dtReturn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        public delegate object[] CreateRowDelegate<in T>(T t);
    }
}