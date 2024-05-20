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
    /// Логика взаимодействия для NewPage.xaml
    /// </summary>
    public partial class NewPage : Page
    {
        private int _RoleId;
        public int UserId { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        public NewPage(int RoleId, string Lastname, string Firstname, int UserId)
        {
            InitializeComponent();
            _RoleId = RoleId;
            this.UserId = UserId;
            this.Lastname = Lastname;
            this.Firstname = Firstname;
            UpdateButtonVisibility();
            DataContext = this;
        }

        private void UpdateButtonVisibility()
        {
            if (_RoleId == 1) // Предполагаем, что роль 1 - это админ
            {
                Reg.Visibility = Visibility.Collapsed;
                addButton.Visibility = Visibility.Visible;
                delButton.Visibility = Visibility.Visible;
            } else if (_RoleId == 2)
            {
                Reg.Visibility = Visibility.Collapsed;
                addButton.Visibility = Visibility.Collapsed;
                delButton.Visibility = Visibility.Collapsed;
            } else if (_RoleId == 3)
            {
                Reg.Visibility = Visibility.Collapsed;
                addButton.Visibility = Visibility.Collapsed;
                delButton.Visibility = Visibility.Collapsed;
            } else
            {
                Exit.Visibility = Visibility.Collapsed;
                Reg.Visibility = Visibility.Visible;
                addButton.Visibility = Visibility.Collapsed;
                delButton.Visibility = Visibility.Collapsed;
            }
        }

        //Взаимодействие с кнопкой которая хранится в DataGridTemplateColumn
        private void EditButton_Loaded(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;

            // Измените видимость кнопки "edit" в зависимости от роли пользователя
            if (_RoleId == 1)
            {
                editButton.Visibility = Visibility.Visible;
            } else if(_RoleId == 2)
            {
                editButton.Visibility = Visibility.Visible;
            } else 
            {
                editButton.Visibility = Visibility.Collapsed;
            } 
        }
        //Взаимодействие с кнопкой которая хранится в DataGridTemplateColumn

       

        public void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DG_Goods.ItemsSource = Entities.GetContext().Goods.ToList();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdit((sender as Button).DataContext as Goods));
           
        }
       

        //private void Buscket_Click(object sender, RoutedEventArgs e)
//        {


          //  Manager.MainFrame.Navigate(new Order());
       // }



        public void Del_Click(object sender, RoutedEventArgs e)
        {
            var del = DG_Goods.SelectedItems.Cast<Goods>().ToList();

            Entities.GetContext().Goods.RemoveRange(del);
            Entities.GetContext().SaveChanges();
            MessageBox.Show("Deleted");

            DG_Goods.ItemsSource = Entities.GetContext().Goods.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdit(null));
        }

        //private void Buscket_button(object sender, RoutedEventArgs e)
        //{

          //  Manager.MainFrame.Navigate(new Order());

        //}

        private void Exit_button(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Login());


        }

        private void Reg_button(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Reg());
        }

       
    }
}
