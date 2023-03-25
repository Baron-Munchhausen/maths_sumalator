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


namespace maths_app
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		ApplicationContext db = new ApplicationContext();
		int current_user_id;

		public MainWindow()
		{
			InitializeComponent();
			grid_auth.Visibility = Visibility.Visible;
		}

		private void button_admin_click(object sender, RoutedEventArgs e)
		{
			AdminWindow admin_window = new AdminWindow()
			{
				WindowStartupLocation = this.WindowStartupLocation,
				Left = this.Left,
				Top = this.Top
			};
			admin_window.Show();
			//Hide();
		}

		private void button_grid_auth_reg_click(object sender, RoutedEventArgs e)
		{
			grid_auth.Visibility = Visibility.Hidden;
			grid_reg.Visibility = Visibility.Visible;
		}

		private void button_grid_reg_auth_click(object sender, RoutedEventArgs e)
		{
			grid_auth.Visibility = Visibility.Visible;
			grid_reg.Visibility = Visibility.Hidden;
		}

		private void button_reg_click(object sender, RoutedEventArgs e)
		{
			string login = textbox_grid_reg_login.Text.Trim();
			string pass = textbox_grid_reg_pass.Password.Trim();
			string pass_2 = textbox_grid_reg_pass_2.Password.Trim();
			bool error = false;

			if (login.Length < 5)
			{
				textbox_grid_reg_login.ToolTip = "Логин меньше пяти символов!";
				textbox_grid_reg_login.Background = Brushes.LightPink;
				error = true;
			}
			else
			{
				textbox_grid_reg_login.ToolTip = "";
				textbox_grid_reg_login.Background = Brushes.Transparent;
			}

			if (pass.Length < 5)
			{
				textbox_grid_reg_pass.ToolTip = "Пароль меньше пяти символов!";
				textbox_grid_reg_pass.Background = Brushes.LightPink;
				error = true;
			}
			else
			{
				textbox_grid_reg_pass.ToolTip = "";
				textbox_grid_reg_pass.Background = Brushes.Transparent;
			}

			if (pass != pass_2)
			{
				textbox_grid_reg_pass_2.ToolTip = "Пароли не совпадают!";
				textbox_grid_reg_pass_2.Background = Brushes.LightPink;
				error = true;
			}
			else
			{
				textbox_grid_reg_pass_2.ToolTip = "";
				textbox_grid_reg_pass_2.Background = Brushes.Transparent;
			}

			if (error == false)
			{
				User user = new User(login, pass);

				db.Users.Add(user);
				db.SaveChanges();

				grid_auth.Visibility = Visibility.Visible;
				grid_reg.Visibility = Visibility.Hidden;
			}
		}

		private void button_grid_auth_enter(object sender, RoutedEventArgs e)
		{
			string login = textbox_grid_auth_login.Text.Trim();
			string pass = textbox_grid_auth_pass.Password.Trim();

			User auth_user = null;
			using (ApplicationContext db = new ApplicationContext())
			{
				auth_user = db.Users.Where(u => u.Login == login && u.Password == pass).FirstOrDefault();
			}

			if (auth_user != null)
			{
				tree_content.Visibility = Visibility.Visible;
				button_admin.Visibility = Visibility.Visible;
				grid_auth.Visibility = Visibility.Hidden;
				grid_reg.Visibility = Visibility.Hidden;
				current_user_id = auth_user.id;

				List<User_answer> _uas = null;
				using (ApplicationContext db = new ApplicationContext())
				{
					_uas = db.User_answers.Where(u => u.User_id == current_user_id).ToList();
					foreach(User_answer _ua in _uas)
					{
						object _obj = FindName(_ua.Task_id);
						((TextBox)_obj).Text = _ua.Answer;
						if (_ua.Is_right == 0)
						{
							((TextBox)_obj).Background = Brushes.LightPink;
						}
						else
						{
							((TextBox)_obj).Background = Brushes.LightGreen;
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Ошибка!");
			}
		}

		private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
		{
			TreeViewItem tree_view_item = (TreeViewItem)sender;

			if (tree_view_item.IsSelected)
			{
				// Обходим все объекты FlowDocumentScrollViewer и выключаем видимость
				foreach (UIElement _control in Tree.Children)
				{
					if (_control is FlowDocumentScrollViewer)
					{
						((FlowDocumentScrollViewer)_control).Visibility = Visibility.Hidden;
					}
				}

				// Находим и включаем видимость нужного документа с учебными материалами
				object obj_doc = FindName(tree_view_item.Name.ToString().Replace("item", "doc"));
				if (obj_doc != null)
				{
					((FlowDocumentScrollViewer)obj_doc).Visibility = Visibility.Visible;
				}

				// Находим и включаем видимость нужного документа с тестами
				object obj_test = FindName(tree_view_item.Name.ToString().Replace("item", "doc") + "_test");
				if (obj_test != null)
				{
					((FlowDocumentScrollViewer)obj_test).Visibility = Visibility.Visible;
				}
			}
		}

		private void answer_click(object sender, RoutedEventArgs e)
		{
			string current_task_id = ((Button)sender).Name.ToString().Replace("_answer", "");

			// Удаляем стаырй ответ
			User_answer old_answer = db.User_answers.Where(u => u.User_id == current_user_id && u.Task_id == current_task_id).FirstOrDefault();
			if (old_answer != null)
			{
				db.User_answers.Remove(old_answer);
				db.SaveChanges();
			}

			string new_answer = ((TextBox)FindName(current_task_id)).Text;

			int is_right = 0;
			string right_answer = string.Empty;

			switch (current_task_id)
			{
				case "task_1_1_1":
					right_answer = "5";
					break;

				case "task_1_1_2":
					right_answer = "5";
					break;
			}

			if (new_answer == right_answer)
			{
				is_right = 1;
				((TextBox)FindName(current_task_id)).Background = Brushes.LightGreen;
			}
			else
			{
				((TextBox)FindName(current_task_id)).Background = Brushes.LightPink;
			}

			// Сохраним новый ответ
			User_answer _ua = new User_answer(current_user_id, current_task_id, new_answer, is_right);
			db.User_answers.Add(_ua);
			db.SaveChanges();
		}
	}
}
