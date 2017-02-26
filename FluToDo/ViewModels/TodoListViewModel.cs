using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using FluToDo.Models;

namespace FluToDo.ViewModels
{
	// Probably better to use MVVMCross or MVVMLight
	public class TodoListViewModel : INotifyPropertyChanged
	{

		private ObservableCollection<TodoItem> _todoList = new ObservableCollection<TodoItem>();
		public ObservableCollection<TodoItem> TodoList
		{
			get
			{
				return _todoList;
			}
			set
			{
				_todoList = value;
				OnPropertyChanged("TodoList");
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}


		public async Task<ObservableCollection<TodoItem>> ToggleComplete(TodoItem item)
		{
			item.IsComplete = !item.IsComplete;
			await App.TodoManager.SaveItemAsync(item);
			var updatedList = await LoadList();

			return updatedList;
		}

		public async Task<ObservableCollection<TodoItem>> DeleteItem(TodoItem item)
		{
			await App.TodoManager.DeleteItemAsync(item);
			var updatedList = await LoadList();

			return updatedList;
		}

		public async Task<ObservableCollection<TodoItem>> LoadList()
		{
			TodoList = await App.TodoManager.GetItemsAsync();
			return TodoList;
		}

		public TodoListViewModel()
		{

		}

}
}
