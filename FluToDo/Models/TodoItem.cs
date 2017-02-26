using System;
using System.Xml.Serialization;

namespace FluToDo.Models
{
	public class TodoItem
	{
		[XmlElement("Key")]
		public string Key { get; set; }

		[XmlElement("Name")]
		public string Name { get; set; }

		[XmlElement("IsComplete")]
		public bool IsComplete { get; set; }

		public TodoItem()
		{
			this.IsComplete = false;
		}

		public TodoItem(String Key, String Name, bool IsComplete = false) 
		{
			this.Key = Key;
			this.Name = Name;
			this.IsComplete = IsComplete;
		}
	}
}
