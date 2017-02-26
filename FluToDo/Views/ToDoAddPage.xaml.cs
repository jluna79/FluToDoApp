using System;
using FluToDo.Models;
using Xamarin.Forms;

namespace FluToDo
{
	public partial class ToDoAddPage : ContentPage
	{
		public ToDoAddPage()
		{
			InitializeComponent();
		}

		public async void OnCreateItem(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			await App.TodoManager.SaveItemAsync(todoItem, true);
			await Navigation.PopAsync();
		}

	}
}
