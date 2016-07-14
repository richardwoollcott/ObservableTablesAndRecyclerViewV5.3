using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

using ObservableTables.ViewModel;

namespace ObservableTables.Droid
{
	public static class App
	{
		private static ViewModelLocator locator;

		public static ViewModelLocator Locator
		{
			get
			{
				if (locator == null)
				{
					// First time initialization

					locator = new ViewModelLocator();
				}

				return locator;
			}
		}
	}
}

