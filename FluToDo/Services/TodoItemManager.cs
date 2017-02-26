using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluToDo.Models;

namespace FluToDo.Services
{
	public class TodoItemManager
	{
		ITodoService todoService;

		public TodoItemManager(ITodoService service)
		{
			todoService = service;
		}

		public Task<ObservableCollection<TodoItem>> GetItemsAsync()
		{
			return todoService.RefreshDataAsync();
		}

		public Task<TodoItem> SaveItemAsync(TodoItem item, bool isNewItem = false)
		{
			return todoService.SaveTodoItemAsync(item, isNewItem);
		}

		public Task DeleteItemAsync(TodoItem item)
		{
			return todoService.DeleteTodoItemAsync(item);
		}
	}
}

