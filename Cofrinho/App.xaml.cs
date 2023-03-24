using Cofrinho.Views;

namespace Cofrinho;

public partial class App : Application
{
	public App(TransactionList listPage)
	{
		InitializeComponent();

		MainPage = new NavigationPage(listPage);
	}
}
