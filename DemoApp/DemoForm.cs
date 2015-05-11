using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DropDownControls.FilteredGroupedComboBox;

namespace DemoApp
{

    /// <summary>
    /// 
    /// </summary>
    public partial class DemoForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public DemoForm()
        {
            InitializeComponent();

            // define our collection of list items
                var groupedItems = new[] { 
                    new GroupedColoredComboBoxItem{ Group = "Gases", Value = "1", Display = "Helium", Color = Color.Blue }, 
				    new GroupedColoredComboBoxItem{ Group = "Gases", Value = "2", Display = "Hydrogen", Color = Color.Blue },
				    new GroupedColoredComboBoxItem{ Group = "Gases", Value = "3", Display = "Oxygen", Color = Color.Blue },
				    new GroupedColoredComboBoxItem{ Group = "Gases", Value = "4", Display = "Argon", Color = Color.Blue },
				    new GroupedColoredComboBoxItem{ Group = "Metals", Value = "5", Display = "Iron", Color = Color.Gray },
				    new GroupedColoredComboBoxItem{ Group = "Metals", Value = "6", Display = "Lithium", Color = Color.Gray },
				    new GroupedColoredComboBoxItem{ Group = "Metals", Value = "7", Display = "Copper", Color = Color.Gray },
				    new GroupedColoredComboBoxItem{ Group = "Metals", Value = "8", Display = "Gold", Color = Color.Gray },
				    new GroupedColoredComboBoxItem{ Group = "Metals", Value = "9", Display = "Silver", Color = Color.Gray },
				    new GroupedColoredComboBoxItem{ Group = "Radioactive", Value = "10", Display = "Uranium", Color = Color.MediumPurple },
				    new GroupedColoredComboBoxItem{ Group = "Radioactive", Value = "11", Display = "Plutonium", Color = Color.MediumPurple },
				    new GroupedColoredComboBoxItem{ Group = "Radioactive", Value = "12", Display = "Americium", Color = Color.MediumPurple },
				    new GroupedColoredComboBoxItem{ Group = "Radioactive", Value = "13", Display = "Radon", Color = Color.MediumPurple } 
			 
			    };
           
            gcbEditable.FilterableGroupableDataSource(groupedItems.AsEnumerable());
      
        }
       
    }

    
}
