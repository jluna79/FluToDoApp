using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluToDo.Models;
using Newtonsoft.Json;

namespace FluToDo.Services
{
	public class TodoService : ITodoService
	{
		HttpClient client;
		public ObservableCollection<TodoItem> Items { get; private set; }
		private const String Url = "http://192.168.2.100:8080/api/todo{0}";

		public TodoService()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
		}

		public async Task<ObservableCollection<TodoItem>> RefreshDataAsync()
		{

			var uri = new Uri(string.Format(Url, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					Items = JsonConvert.DeserializeObject<ObservableCollection<TodoItem>>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"[TodoService] ERROR {0}", ex.Message);
			}

			return Items;
		}

		public async Task<TodoItem> SaveTodoItemAsync(TodoItem item, bool isNewItem)
		{
			var uri = new Uri(string.Format(Url, isNewItem? string.Empty : "/"+item.Key));

			try
			{
				var json = JsonConvert.SerializeObject(item);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem)
				{
					response = await client.PostAsync(uri, content);
				}
				else {
					response = await client.PutAsync(uri, content);
				}

				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine(@"[TodoService] Item {0} saved", item.Key);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"[TodoService] SAVE ERROR {0}", ex.Message);
			}

			return item;
		}

		public async Task<TodoItem> DeleteTodoItemAsync(TodoItem item)
		{
			var uri = new Uri(string.Format(Url, "/" + item.Key));

			try
			{
				var response = await client.DeleteAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine(@"[TodoService] Item {0} deleted", item.Key);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"[TodoService] DELETE ERROR {0}", ex.Message);
			}

			return item;
		}

	}
}
