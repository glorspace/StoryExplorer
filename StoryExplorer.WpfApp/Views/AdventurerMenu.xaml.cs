using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StoryExplorer.DataModel;

namespace StoryExplorer.WpfApp
{
	/// <summary>
	/// Interaction logic for AdventurerMenu.xaml
	/// </summary>
	public partial class AdventurerMenu : Window
	{
		private Adventurer adventurer;
		private MainWindow mainWindow;

		public AdventurerMenu()
		{
			InitializeComponent();
		}

		public AdventurerMenu(MainWindow mainWindow, Adventurer adventurer) : this()
		{
			this.mainWindow = mainWindow;
			this.adventurer = adventurer;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			mainWindow.Close();
		}
	}
}
