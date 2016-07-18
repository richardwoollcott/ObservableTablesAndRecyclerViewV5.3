using Android.App;
using Android.Widget;
using Android.OS;

using ObservableTables.ViewModel;

using GalaSoft.MvvmLight.Helpers;
using Android.Support.V7.Widget;

namespace ObservableTables.Droid
{
	[Activity (Label = "Tasks", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
        private RecyclerView taskRecyclerView;
        private ObservableRecyclerAdapter<TaskModel, CachingViewHolder> adapter;

		private Button addTaskButton;

		public RecyclerView TaskRecyclerView
        {
            get
            {
                return taskRecyclerView ??
                  (taskRecyclerView = FindViewById<RecyclerView>(
                        Resource.Id.tasksRecyclerView));
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

            TaskRecyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));

            // Create the adapter using the default CachingViewHolder
            adapter = Vm.TodoTasks.GetRecyclerAdapter(
                BindViewHolder,
                Resource.Layout.TaskTemplate);

            TaskRecyclerView.SetAdapter(adapter);

			//ensure that the Event will be present
			AddTaskButton.Click += (sender, e) => {};

			// Actuate the AddTaskCommand on the VM.
			AddTaskButton.SetCommand(
				"Click",
				Vm.AddTaskCommand);
		}

        private void BindViewHolder(CachingViewHolder holder, TaskModel taskModel, int position)
        {
            // if the data source doesn't change use the simpler form below
            //var name = holder.FindCachedViewById<TextView>(Resource.Id.NameTextView);
            //name.Text = taskModel.Name;

            //var desc = holder.FindCachedViewById<TextView>(Resource.Id.NotesTextView);
            //desc.Text = taskModel.Notes;

            var name = holder.FindCachedViewById<TextView>(Resource.Id.NameTextView);
            holder.DeleteBinding(name);

            var nameBinding = new Binding<string, string>(taskModel,
                                                          () => taskModel.Name,
                                                          name,
                                                          () => name.Text,
                                                          BindingMode.OneWay);
            
            holder.SaveBinding(name, nameBinding);

            var desc = holder.FindCachedViewById<TextView>(Resource.Id.NotesTextView);
            holder.DeleteBinding(desc);

            var descBinding = new Binding<string, string>(taskModel,
                                                          () => taskModel.Notes,
                                                          desc,
                                                          () => desc.Text,
                                                          BindingMode.OneWay);

            holder.SaveBinding(desc, descBinding);
        }
	}
}


