using FluToDo.Services;
using Xamarin.Forms;

namespace FluToDo
{
	public partial class App : Application
	{
		public static TodoItemManager TodoManager { get; private set; }

		public App()
		{
			InitializeComponent();

			TodoManager = new TodoItemManager(new TodoService());
			MainPage = new NavigationPage(new FluToDoPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
