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
        object[] roleList;
        List<string> roleListOne;
        List<string> roleListTwo;
        public RoleSuggestion(object[] roleLists)
        {
            this.roleList = roleLists;
            this.roleListOne = (List<string>) roleList[0];
            this.roleListTwo = (List<string>) roleList[1];

            string[] roleOneArray = roleListOne.ToArray();
            string[] roleTwoArray = roleListTwo.ToArray();
            InitializeComponent();
            InitializeLists(roleOneArray, roleTwoArray);
        }

        private void InitializeLists(string[] roleOneArray, string[] roleTwoArray)
        {
            for (int index = 0; index < roleOneArray.Length; index++)
            {
                SuggestionListBox.Items.Add("The role " + roleOneArray[index] + " and " + roleTwoArray[index] + " have the exact same permissions. Consider merging the two roles.");
            }
        }
    }
}
