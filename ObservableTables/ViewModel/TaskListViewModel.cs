using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;

namespace ObservableTables.ViewModel
{
	public class TaskListViewModel : ViewModelBase
	{
		public ObservableCollection<TaskModel> TodoTasks { get; private set; }

		public TaskListViewModel()
		{
			AddTaskCommand = new RelayCommand(AddTask);
		}

		private List<TaskModel> SeedData()
		{
			var tasks = new List<TaskModel>()
						{
							new TaskModel {
								Name = "Make Lunch",
								Notes = ""
							},
							new TaskModel {
								Name = "Pack Lunch",
								Notes = "In the bag, make sure we don't squash anything. Remember to pack the orange juice too."
							},
							new TaskModel {
								Name = "Goto Work",
								Notes = "Walk if it's sunny"
							},
							new TaskModel {
								Name = "Eat Lunch",
								Notes = ""
							}
						};	

			return tasks;
		}

		public void Initialize()
		{
			if (TodoTasks != null)
			{
				// Prevent memory leak in Android
				var tasksCopy = TodoTasks.ToList();
				TodoTasks = new ObservableCollection<TaskModel>(tasksCopy);
				return;
			}

			TodoTasks = new ObservableCollection<TaskModel>();

			var people = SeedData();
			TodoTasks.Clear();
			foreach (var person in people)
			{
				TodoTasks.Add(person);
			}
		}

		public RelayCommand AddTaskCommand { get; set; }

		private void AddTask()
		{
			TodoTasks.Add(new TaskModel
				{
					Name = "New Task",
					Notes = ""
				});
		}
	}
}