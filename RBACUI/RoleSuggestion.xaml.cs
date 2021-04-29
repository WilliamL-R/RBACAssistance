using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RBACUI
{
    /// <summary>
    /// Interaction logic for RoleSuggestion.xaml
    /// </summary>
    public partial class RoleSuggestion : Window
    {
        Dictionary<string, string> roleDictionary;
        public RoleSuggestion(Dictionary<string,string> roleDict)
        {
            this.roleDictionary = roleDict;
            InitializeComponent();
            InitializeLists();
        }

        private void InitializeLists()
        {
            foreach (var item in roleDictionary)
            {
                SuggestionListBox.Items.Add("The role " + item.Key + " and " + item.Value + " have the exact same permissions. Consider merging the two roles.");
            }
        }
    }
}
