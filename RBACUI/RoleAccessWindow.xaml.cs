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

    public partial class RoleAccessWindow : Window
    {
        RoleList roleList ;
        ResourceList resourceList ;

        Role selectedRole;

        public RoleAccessWindow(RoleList roleList, ResourceList resourceList)
        {
            this.roleList = roleList;
            this.resourceList = resourceList;
            InitializeComponent();
            InitializeLists();
            roleListView.MouseDoubleClick += new MouseButtonEventHandler(roleListView_MouseDoubleClick);
        }

        private void InitializeLists()
        {
            foreach (Role item in roleList)
            {
                roleListView.Items.Add(item.GetRoleName());
            }
            foreach (Resource item in resourceList)
            {
                resourceAvailableListView.Items.Add(item.GetResourceName());
            }
        }

        private void AddAllowedAccess(object sender, RoutedEventArgs e)
        {
            Resource item = resourceList.ElementAt(resourceAvailableListView.SelectedIndex);

            if(selectedRole == null)
            {
                return;
            }
            selectedRole.AddResourceAccess(item);
            resourceAccessListView.Items.Clear();
            foreach (Resource res in selectedRole.GetResourceAccess())
            {
                resourceAccessListView.Items.Add(res.GetResourceName());
            }
        }

        private void roleListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;
            Role item = (Role)roleListView.ItemContainerGenerator.ItemFromContainer(dep);
            */
            Role item = roleList.ElementAt(roleListView.SelectedIndex);
            List<Resource> itemResources = item.GetResourceAccess();

            foreach (Resource resource in itemResources)
            {
                resourceAccessListView.Items.Add(resource.GetResourceName());
            }

            selectedRole = item;
            RoleSelectedTextBlock.Inlines.Clear();
            RoleSelectedTextBlock.Inlines.Add("Role Selected : " + selectedRole.GetRoleName());
        }

    }
}