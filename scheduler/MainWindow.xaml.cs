using System;
using System.Collections.Generic;
using System.Data;
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
using scheduler.database;
using scheduler.structs;

namespace scheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CustomersDataGrid.DataContext = DatabaseManager.Instance.GetDataTable("SELECT * FROM customer");
            //AppointmnetsDataGrid.DataContext = DatabaseManager.Instance.GetDataTable("SELECT * FROM appointment");
        }

        private void RefreshAppointments()
        {
            throw new NotImplementedException();
        }

        private void CustomersDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedObject = (DataRowView)CustomersDataGrid.SelectedItem;
            State.Instance.CurrentCustomer = new Customer((int)selectedObject.Row.ItemArray[0]);
            try
            {
                RefreshCustomers();
            }
            catch (Exception ex)
            {
            }
        }

        private void RefreshCustomers()
        {
            State.Instance.CurrentCustomer.Load();
            custIDLabel.Content = State.Instance.CurrentCustomer.Id;
            custNameTextBox.Text = State.Instance.CurrentCustomer.Name;
            custActiveCheck.IsChecked = State.Instance.CurrentCustomer.IsActive;
            custAddressLabel.Content = State.Instance.CurrentCustomer.Address.ToString();
            custCreatedLabel.Content = "Created by " + State.Instance.CurrentCustomer.CreatedBy + " on " +
                                       State.Instance.CurrentCustomer.CreatedAt;
            custUpdatedLabel.Content = "Last updated by " + State.Instance.CurrentCustomer.UpdatedBy + " on " +
                                       State.Instance.CurrentCustomer.UpdatedAt;
        }

        private void AddressClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AppointmentsDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedObject = (DataRowView)AppointmentsDataGrid.SelectedItem;
            State.Instance.CurrentAppointment = new Appointment((int)selectedObject.Row.ItemArray[0]);
            try
            {
                RefreshCustomers();
            }
            catch (Exception ex)
            {
            }
        }
    }
}