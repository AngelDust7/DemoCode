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

namespace Sport
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        private Users _users = new Users();

        private Users _Users = new Users();
        public Reg()
        {
            InitializeComponent();

            _Users = new Users();
            DataContext = _Users;
            CMRole.ItemsSource = Entities.GetContext().Roles.ToList();
        }
       

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            if(_Users.UserId == 0)
            {
                Entities.GetContext().Users.Add(_Users);
            }
            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Info saved");
                Manager.MainFrame.Navigate(new Login());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Bback_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new NewPage(_users.RoleId, _users.Lastname, _users.Firstname, _users.UserId));
        }

        private void Llog_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Login());
        }
    }
}
