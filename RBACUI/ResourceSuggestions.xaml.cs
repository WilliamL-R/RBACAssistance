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
using RBACAssistance.Core;
using RBACAssistance.Core.Objects;

namespace RBACUI
{
    /// <summary>
    /// Interaction logic for ResourceSuggestions.xaml
    /// </summary>
    public partial class ResourceSuggestions : Window
    {
        List<string> resourceList;
        public ResourceSuggestions(List<string> resourceList)
        {
            this.resourceList = resourceList;
            InitializeComponent();
            InitializeLists();
        }

        private void InitializeLists()
        {
            if (resourceList.Count == 0)
            {
                SuggestionListBox.Items.Add("No suggestions available at this time.");
            }
            else
            {
                foreach (string item in resourceList)
                {
                    SuggestionListBox.Items.Add("The resource " + item + " has a lot of roles accessing the resource. Consider reducing the roles accessing it.");
                }
            }
        }
    }
}
