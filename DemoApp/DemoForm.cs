using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DemoApp
{

    public partial class DemoForm : Form
    {
        private readonly object[] _groupedItems;
        private DataView _dv;
        private bool _textChanged = false;

        public DemoForm()
        {
            InitializeComponent();

            rbPlain.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            rbVS.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);

            // define our collection of list items
            var groupedItems = new[] { 
				new { Group = "Gases", Value = 1, Display = "Helium" }, 
				new { Group = "Gases", Value = 2, Display = "Hydrogen" },
				new { Group = "Gases", Value = 3, Display = "Oxygen" },
				new { Group = "Gases", Value = 4, Display = "Argon" },
				new { Group = "Metals", Value = 5, Display = "Iron" },
				new { Group = "Metals", Value = 6, Display = "Lithium" },
				new { Group = "Metals", Value = 7, Display = "Copper" },
				new { Group = "Metals", Value = 8, Display = "Gold" },
				new { Group = "Metals", Value = 9, Display = "Silver" },
				new { Group = "Radioactive", Value = 10, Display = "Uranium" },
				new { Group = "Radioactive", Value = 11, Display = "Plutonium" },
				new { Group = "Radioactive", Value = 12, Display = "Americium" },
				new { Group = "Radioactive", Value = 13, Display = "Radon" },
				new { Group = "", Value = 99, Display = "" }
			};
            _groupedItems = groupedItems;
            // anonymous method delegate for transforming the above into nodes
            Action<ComboTreeBox> addNodes = ctb =>
            {
                foreach (var grp in groupedItems.GroupBy(x => x.Group))
                {
                    ComboTreeNode parent = ctb.Nodes.Add(grp.Key);
                    foreach (var item in grp)
                    {
                        parent.Nodes.Add(item.Display);
                    }
                }
                ctb.Sort();
                ctb.SelectedNode = ctb.Nodes[0].Nodes[0];
            };

            // normal combobox
            cmbNormal.ValueMember = "Value";
            cmbNormal.DisplayMember = "Display";
            cmbNormal.DataSource = new BindingSource(groupedItems, String.Empty);

            // grouped comboboxes
            gcbList.ValueMember = "Value";
            gcbList.DisplayMember = "Display";
            gcbList.GroupMember = "Group";
            gcbList.DataSource = new BindingSource(groupedItems, String.Empty);


            var query = from gi in groupedItems.AsEnumerable() select gi;
          
            gcbEditable.ValueMember = "Value";
            gcbEditable.DisplayMember = "Display";
            gcbEditable.GroupMember = "Group";
        
            var table = query.ToADOTable(rec => new object[] {query});
            var dv = table.AsDataView();
            _dv = dv;
            gcbEditable.DataSource = new BindingSource(dv, String.Empty);
           

            // combotreeboxes
            addNodes(ctbNormal);
            addNodes(ctbImages);

            addNodes(ctbCheckboxes);
            ctbCheckboxes.CheckedNodes = new ComboTreeNode[] { 
				ctbCheckboxes.Nodes[1].Nodes[0], 
				ctbCheckboxes.Nodes[1].Nodes[1] 
			};

            foreach (var item in groupedItems)
            {
                ctbFlatChecks.Nodes.Add(item.Display);
            }

            ctbFlatChecks.CheckedNodes = new ComboTreeNode[] { 
				ctbFlatChecks.Nodes[0], 
				ctbFlatChecks.Nodes[1] 
			};
        }
        private void gcbEditable_TextChanged(object sender, EventArgs e)
        {
            if (_textChanged) return;
            _textChanged = true;
            var text = gcbEditable.Text;
            Console.WriteLine(text);
            if (gcbEditable.SelectedItem == null)
            {
                _dv.RowFilter = "[Display] like '%" + text + "%' OR [Value]= 99 ";
                Console.WriteLine(_dv.RowFilter);
            }
            else _dv.RowFilter = "1=1";
            gcbEditable.SelectedItem = null;
            gcbEditable.Text = text;
            gcbEditable.Select(gcbEditable.Text.Length, 0);
            _textChanged = false;
        }
        void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            ctbNormal.DrawWithVisualStyles = rbVS.Checked;
            ctbImages.DrawWithVisualStyles = rbVS.Checked;
            ctbCheckboxes.DrawWithVisualStyles = rbVS.Checked;
            ctbFlatChecks.DrawWithVisualStyles = rbVS.Checked;
        }

        private void gcbEditable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gcbEditable.SelectedIndex != -1)
            {
                var value = gcbEditable.SelectedValue;
                _dv.RowFilter = "1=1";
                gcbEditable.SelectedValue = value;
            }
        }


    }

    public class GroupItem
    {
        public string Group { get; set; }
        public int Value { get; set; }
        public string Display { get; set; }
    }
    static public class ConvertDataTable
    {

        public static DataTable ToADOTable<T>(this IEnumerable<T> varlist, CreateRowDelegate<T> fn)
        {

            DataTable dtReturn = new DataTable();
            // Could add a check to verify that there is an element 0

            T TopRec = varlist.ElementAt(0);

            // Use reflection to get property names, to create table

            // column names

            PropertyInfo[] oProps =
            ((Type)TopRec.GetType()).GetProperties();

            foreach (PropertyInfo pi in oProps)
            {

                Type colType = pi.PropertyType; if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {

                    colType = colType.GetGenericArguments()[0];

                }

                dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
            }

            foreach (T rec in varlist)
            {

                DataRow dr = dtReturn.NewRow(); foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                }
                dtReturn.Rows.Add(dr);

            }

            return (dtReturn);
        }

        public delegate object[] CreateRowDelegate<T>(T t);
    }
}
