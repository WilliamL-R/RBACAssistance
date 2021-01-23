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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RBACAssistance.Core;
using RBACAssistance.Core.Objects;

namespace RBACUI
{

    public partial class MainWindow : Window
    {
        RoleList roleList = new RoleList();
        ResourceList resourceList = new ResourceList();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRole(object sender, RoutedEventArgs e)
        {
            Role newRole = new Role(roleNameBox.Text);
            roleListView.Items.Add(newRole.GetRoleName());
            roleList.AddRoleToList(newRole);
            roleNameBox.Clear();
            CheckListCount();
        }

        private void AddResource(object sender, RoutedEventArgs e)
        {
            Resource newResource = new Resource(resourceNameBox.Text);
            resourceListView.Items.Add(newResource.GetResourceName());
            resourceList.AddResourceToList(newResource);
            resourceNameBox.Clear();
            CheckListCount();
        }

        private void CheckListCount()
        {
            if (roleList.GetCount() >= 1 && resourceList.GetCount() >= 1)
            {
                RoleAccessButton.IsEnabled = true;
            }
        }

        private void OpenRoleAccess(object sender, RoutedEventArgs e)
        {
            RoleAccessWindow popup = new RoleAccessWindow(roleList, resourceList);
            popup.ShowDialog();
        }
    }
}
