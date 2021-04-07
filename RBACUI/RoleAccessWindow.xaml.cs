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
using RBACAssistance.Core.XML;
using RBACAssistance.Core.RoleSuggestion;
using RBACAssistance.Core.Graph;
using Microsoft.VisualStudio.GraphModel;

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

            if (item == null)
            {
                MessageBox.Show("Resource needs to be selected");
                return;
            }

            if (selectedRole.CheckRoleAccess(item))
            {
                MessageBoxResult dialogResult = MessageBox.Show("This resource is considered sensitive and this role is not considered senior. Do you wish to proceed to allow access?", "Resourece Conflict", MessageBoxButton.YesNo);
                if(dialogResult == MessageBoxResult.No)
                {
                    return;
                }
            }
            selectedRole.AddResourceAccess(item);
            resourceAccessListView.Items.Clear();

            foreach (Resource res in selectedRole.GetResourceAccess())
            {
                resourceAccessListView.Items.Add(res.GetResourceName());
            }
        }

        private void GetAsXMLDoc(object sender, RoutedEventArgs e)
        {
            XMLWriter writer = new XMLWriter();
            writer.WriteXML(roleList, resourceList);        
        }

        private void roleListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try {
                Role item = roleList.ElementAt(roleListView.SelectedIndex);
                if (item == selectedRole)
                {
                    return;
                }
                List<Resource> itemResources = item.GetResourceAccess();

                resourceAccessListView.Items.Clear();

                foreach (Resource resource in itemResources)
                {
                    resourceAccessListView.Items.Add(resource.GetResourceName());
                }

                selectedRole = item;
                RoleSelectedTextBlock.Inlines.Clear();
                RoleSelectedTextBlock.Inlines.Add("Role Selected : " + selectedRole.GetRoleName());
                AddAccessButton.IsEnabled = true;
            }
            catch (ArgumentOutOfRangeException){
                MessageBox.Show("Role needs to be selected");
            }
        }

        private void DrawGraph(object sender, RoutedEventArgs e)
        {
            //DrawFromXML draw = new DrawFromXML();
            //draw.Serialize("PathHere");
            XMLWriter writer = new XMLWriter();
            writer.WriteDGML(roleList, resourceList);
        }

        private void CheckRoles(object sender, RoutedEventArgs e)
        {
            RoleReader roleReader = new RoleReader();
            int roleSimilarities = roleReader.RoleSuggest(roleList);
            RolesToBeJoinedLabel.Inlines.Clear();
            RolesToBeJoinedLabel.Inlines.Add("Roles that could be joined : " + roleSimilarities);
        }
    }
}