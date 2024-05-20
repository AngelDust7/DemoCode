using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Page
    {
        private Goods _currentgoods;

        public AddEdit(Goods selectedGoods)
        {
            _currentgoods = selectedGoods ?? new Goods(); // Если selectedGoods равен null, создаем новый Goods

            InitializeComponent();

            DataContext = _currentgoods;
            ComboEd.ItemsSource = Entities.GetContext().EdIzm.ToList();
            ComboNameOfCategoryOfGoods.ItemsSource = Entities.GetContext().CategoryOfGoods.ToList();
            ComboNameOfDeveloper.ItemsSource = Entities.GetContext().Developer.ToList();
            ComboNameOfGoods.ItemsSource = Entities.GetContext().NameOfGoods.ToList();
            ComboNameOfPostavshik.ItemsSource = Entities.GetContext().Postavshik.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            /*Здесь мы проверяем, есть ли у товара ID (GoodsId). Если GoodsId равен 0, это означает, 
             * что это новый товар, и мы добавляем его в контекст. В противном случае (GoodsId не равен 0), 
             * мы изменяем состояние существующего товара на "Modified", чтобы при сохранении изменения применились к базе данных.
             */

            if (_currentgoods.GoodsId == 0)
            {
                Entities.GetContext().Goods.Add(_currentgoods);
            }
            else
            {
                Entities.GetContext().Entry(_currentgoods).State = EntityState.Modified;
            }

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Info saved");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
