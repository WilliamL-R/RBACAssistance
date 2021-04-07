using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input; 
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using RBACAssistance.Core;
using RBACAssistance.Core.Objects;
using RBACAssistance.Core.DataImport;

namespace RBACUI
{

    public partial class MainWindow : Window
    {
        RoleList roleList = new RoleList();
        ResourceList resourceList = new ResourceList();
        UserList userList = new UserList();

        Resource selectedResource;

        public MainWindow()
        {
            InitializeComponent();
            foreach(ResourceSensitivity resvals in Enum.GetValues(typeof(ResourceSensitivity)))
            {
                resourceComboBox.Items.Add(resvals);
            }
            seniorComboBox.Items.Add(true);
            seniorComboBox.Items.Add(false);
            resourceListView.MouseDoubleClick += new MouseButtonEventHandler(resourceListView_MouseDoubleClick);
        }

        private void AddRole(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(roleNameBox.Text))
            {
                MessageBox.Show("Role name can not be empty.", "Error");
                return;
            }
            Role newRole = new Role(roleNameBox.Text, (bool) seniorComboBox.SelectedItem);
            roleListView.Items.Add(newRole.GetRoleName());
            roleList.AddRoleToList(newRole);
            roleNameBox.Clear();
            CheckListCount();
        }

        private void AddResource(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(resourceNameBox.Text))
            {
                MessageBox.Show("Resource name can not be empty.", "Error");
                return;
            }

            ResourceSensitivity resourceSensitivity = (ResourceSensitivity)resourceComboBox.SelectedItem;
            Resource newResource = new Resource(resourceNameBox.Text, resourceSensitivity);
            resourceListView.Items.Add(newResource.GetResourceName());
            resourceList.AddResourceToList(newResource);
            resourceNameBox.Clear();
            CheckListCount();
        }

        private void RemoveResource(object sender, RoutedEventArgs e)
        {
            if(selectedResource == null)
            {
                return;
            }
            resourceListView.Items.Remove(selectedResource);
            resourceList.RemoveResource(selectedResource);

            for (int roleIndex = 0; roleIndex < roleList.GetCount(); roleIndex++)
            {
                Role role = roleList.ElementAt(roleIndex);
                List<Resource> resourceAccess = role.GetResourceAccess();
                foreach (Resource res in resourceAccess.ToList())
                {
                    if (res.GetResourceName() == selectedResource.GetResourceName())
                    {
                        resourceAccess.Remove(res);
                    }
                }
            }
            resourceListView.Items.Clear();
            foreach (Resource resource in resourceList)
            {
                resourceListView.Items.Add(resource.GetResourceName());
            }
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

        private void AddFilePath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Comma-separated values (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (System.IO.Path.GetExtension(openFileDialog.FileName) != ".csv")
            {
                MessageBox.Show("Your selection was not a CSV file, please select a CSV file.");
                return;
            }

            if (result == true) 
            {
                fileNameBox.Text = openFileDialog.FileName.ToString();
            }
        }

        private void ImportDataset(object sender, RoutedEventArgs e)
        {
            CSVReader csvReader = new CSVReader();
            object[] listArray = csvReader.BindDataCSV(fileNameBox.Text, roleList, resourceList, userList);
            if(listArray == null)
            {
                MessageBox.Show("Your CSV file was unable to be accessed. Please make sure your chosen file is not being used and exists.");
                return;
            }
            roleListView.Items.Clear();
            resourceListView.Items.Clear();
            userListView.Items.Clear();
            roleList = (RoleList) listArray[0];
            resourceList = (ResourceList)listArray[1];
            userList = (UserList)listArray[2];
            foreach (Role role in roleList)
            {
                roleListView.Items.Add(role.GetRoleName());
            }
            foreach (Resource resource in resourceList)
            {
                resourceListView.Items.Add(resource.GetResourceName());
            }
            foreach (User user in userList)
            {
                userListView.Items.Add(user.GetUserName());
            }
            CheckListCount();
        }

        private void roleListView_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void resourceListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Resource item = resourceList.ElementAt(resourceListView.SelectedIndex);
                if (item == selectedResource)
                {
                    return;
                }
                selectedResource = item;
                Console.WriteLine(selectedResource.GetResourceName());
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Resource needs to be selected.");
            }
        }
    }
}