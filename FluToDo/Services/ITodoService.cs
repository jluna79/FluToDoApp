using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluToDo.Models;

namespace FluToDo.Services
{
	public interface ITodoService
	{
		Task<ObservableCollection<TodoItem>> RefreshDataAsync();

		Task<TodoItem> SaveTodoItemAsync(TodoItem item, bool isNewItem);

		Task<TodoItem> DeleteTodoItemAsync(TodoItem item);
	}
}