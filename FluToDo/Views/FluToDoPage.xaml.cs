using FluToDo.ViewModels;
using Xamarin.Forms;
using System;
using FluToDo.Models;

namespace FluToDo
{
	public partial class FluToDoPage : ContentPage
	{
		public FluToDoPage()
		{
			InitializeComponent();
			ViewModel = new TodoListViewModel();
			BindingContext = ViewModel;
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await ViewModel.LoadList();
		}

		protected async void OnRefreshing(object sender, EventArgs e) 
		{
			await ViewModel.LoadList();
			todoListView.IsRefreshing= false;
		}

		public async void OnDelete(object sender, EventArgs e)
		{			
			var mi = ((MenuItem)sender);
			var item = mi.CommandParameter as TodoItem;

			await ViewModel.DeleteItem(item);

			await DisplayAlert("Deleted item", "ToDo Item " + item.Name + " has been deleted correctly", "OK");
		}

		protected async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			await ViewModel.ToggleComplete((TodoItem)e.SelectedItem);
			await ViewModel.LoadList();
		}

		public void OnAddItem(object sender, EventArgs e)
		{
			var todoItem = new TodoItem()
			{
				Key = Guid.NewGuid().ToString()
			};
			var todoPage = new ToDoAddPage();
			todoPage.BindingContext = todoItem;
			Navigation.PushAsync(todoPage);
		}

		public TodoListViewModel ViewModel { get; private set; }

	}
}
