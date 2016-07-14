using Foundation;
using System;
using UIKit;
using GalaSoft.MvvmLight.Helpers;
using ObservableTables.ViewModel;

namespace ObservableTables.iOS
{
    partial class TaskListViewController : UITableViewController
	{
		private ObservableTableViewSource<TaskModel> source;

		private TaskListViewModel Vm => Application.Locator.TaskList;

		public TaskListViewController (IntPtr handle) : base (handle)
		{
		}

		public UIBarButtonItem AddTaskButton
		{
			get;
			private set;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Vm.Initialize ();

			AddTaskButton = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			this.NavigationItem.SetLeftBarButtonItem (AddTaskButton, false);

			//note this was needed when deploying to a real iPhone but worked
			//on the simulator without it. The Argument Exception Event not found clicked was thrown
			AddTaskButton.Clicked += (sender, e) => { };

			AddTaskButton.SetCommand ("Clicked", Vm.AddTaskCommand);

			source = Vm.TodoTasks.GetTableViewSource (
				CreateTaskCell,
				BindTaskCell,
				factory: () => new TaskListObservableTableSource ());

			TasksTableView.Source = source;
		}

		private void BindTaskCell (UITableViewCell cell, TaskModel taskModel, NSIndexPath path)
		{
			cell.TextLabel.Text = taskModel.Name;
			cell.DetailTextLabel.Text = taskModel.Notes;
		}

		private UITableViewCell CreateTaskCell (NSString cellIdentifier)
		{
			var cell = new UITableViewCell (UITableViewCellStyle.Subtitle, null);
			cell.TextLabel.TextColor = UIColor.FromRGB (55, 63, 255);
			cell.DetailTextLabel.LineBreakMode = UILineBreakMode.TailTruncation;

			return cell;
		}
	}

}