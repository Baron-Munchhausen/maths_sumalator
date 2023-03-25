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
using Microsoft.EntityFrameworkCore;

namespace maths_app
{
	/// <summary>
	/// Interaction logic for AdminWindow.xaml
	/// </summary>
	public partial class AdminWindow : Window
	{
		ApplicationContext db = new ApplicationContext();

		public AdminWindow()
		{
			InitializeComponent();

			Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			// гарантируем, что база данных создана
			db.Database.EnsureCreated();
			// загружаем данные из БД
			db.Users.Load();
			// и устанавливаем данные в качестве контекста
			DataContext = db.Users.Local.ToObservableCollection();
		}

        // добавление
        //private void Add_Click(object sender, RoutedEventArgs e)
        //{
        //    UserWindow UserWindow = new UserWindow(new User());
        //    if (UserWindow.ShowDialog() == true)
        //    {
        //        User User = UserWindow.User;
        //        db.Users.Add(User);
        //        db.SaveChanges();
        //    }
        //}
        // редактирование
        //private void Edit_Click(object sender, RoutedEventArgs e)
        //{
        //    // получаем выделенный объект
        //    User? user = usersList.SelectedItem as User;
        //    // если ни одного объекта не выделено, выходим
        //    if (user is null) return;

        //    UserWindow UserWindow = new UserWindow(new User
        //    {
        //        Id = user.Id,
        //        Age = user.Age,
        //        Name = user.Name
        //    });

        //    if (UserWindow.ShowDialog() == true)
        //    {
        //        // получаем измененный объект
        //        user = db.Users.Find(UserWindow.User.Id);
        //        if (user != null)
        //        {
        //            user.Age = UserWindow.User.Age;
        //            user.Name = UserWindow.User.Name;
        //            db.SaveChanges();
        //            usersList.Items.Refresh();
        //        }
        //    }
        //}
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            User? user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
