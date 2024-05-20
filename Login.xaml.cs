using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Sport
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {

       private Users _users = new Users();
        public Login()
        {
            InitializeComponent();
        }



        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            //Прописываем строку подключения
            SqlConnection sqlConnection = new SqlConnection(@"data source=DESKTOP-SFUJH1J;initial catalog=01_Ахматшин Кирилл 20 КИС-1;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }


        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text.Length > 0)
            {
                if (PsBox.Password.Length > 0)  // Добавляем условие, если RoleId равен 4, пароль не обязателен
                {
                    DataTable dt_user = Select("SELECT Users.UserId, Users.RoleId, Users.Lastname, Users.Firstname, Users.UserId FROM Users, Roles WHERE Roles.RoleId=Users.RoleId AND [Login] = '" + TbLogin.Text + "' AND [Password] = '" + PsBox.Password + "'");
                    if (dt_user.Rows.Count > 0)
                    {
                        _users.RoleId = Convert.ToInt32(dt_user.Rows[0]["RoleId"]);
                        _users.UserId = Convert.ToInt32(dt_user.Rows[0]["UserId"]);
                        _users.Lastname = dt_user.Rows[0]["Lastname"].ToString();
                        _users.Firstname = dt_user.Rows[0]["Firstname"].ToString();


                        if (_users.RoleId == 1 || _users.RoleId == 2 || _users.RoleId == 3)
                        {
                            Manager.MainFrame.Navigate(new NewPage(_users.RoleId, _users.Lastname, _users.Firstname, _users.UserId));
                           
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователя не найден");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }
        }

        private void Passed_Click(object sender, RoutedEventArgs e)
        {
            
        
               Manager.MainFrame.Navigate(new NewPage(_users.RoleId, _users.Lastname, _users.Firstname, _users.UserId));
   
        }

        private void Regis_button(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Reg());
        }
    }
}
