using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

using ObservableTables.ViewModel;

using GalaSoft.MvvmLight.Helpers;

namespace ObservableTables.Droid
{
	[Activity (Label = "Tasks", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		private ListView taskList;

		private Button addTaskButton;

		public ListView TaskList
		{
			get
			{
				return taskList
					?? (taskList = FindViewById<ListView>(Resource.Id.tasksListView));
			}
		}

		public Button AddTaskButton
		{
			get
			{
				return addTaskButton
					?? (addTaskButton = FindViewById<Button>(Resource.Id.addTaskButton));
			}
		}

		public TaskListViewModel Vm
		{
			get
			{
				return App.Locator.TaskList;
			}
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var toolbar = FindViewById<Toolbar> (Resource.Id.tasksToolbar);
			//Toolbar will now take on default Action Bar characteristics
			SetActionBar (toolbar);

			Vm.Initialize ();
			TaskList.Adapter = Vm.TodoTasks.GetAdapter(GetTaskAdapter);

			//ensure that the Event will be present
			AddTaskButton.Click += (sender, e) => {};

			// Actuate the AddTaskCommand on the VM.
			AddTaskButton.SetCommand(
				"Click",
				Vm.AddTaskCommand);
		}

		private View GetTaskAdapter(int position, TaskModel taskModel, View convertView)
		{
			// Not reusing views here
			convertView = LayoutInflater.Inflate(Resource.Layout.TaskTemplate, null);

			var title = convertView.FindViewById<TextView>(Resource.Id.NameTextView);
			title.Text = taskModel.Name;

			var desc = convertView.FindViewById<TextView>(Resource.Id.NotesTextView);
			desc.Text = taskModel.Notes;

			return convertView;
		}
	}
}


