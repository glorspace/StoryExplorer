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

namespace StoryExplorer.WpfApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void loadAdventurer_Click(object sender, RoutedEventArgs e)
		{
			//ViewModel currentViewModel = (ViewModel)DataContext;
			newAdventurer.IsEnabled = false;
			loadAdventurer.IsEnabled = false;
			selectAdventurer.Visibility = Visibility.Visible;
			adventurerProfile.Visibility = Visibility.Visible;
		}
	}
}
