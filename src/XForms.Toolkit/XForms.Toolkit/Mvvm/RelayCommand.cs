﻿using System;
using System.Windows.Input;

namespace XForms.Toolkit
{
	public class RelayCommand : ICommand
	{
		private readonly Action _execute;

		private readonly Func<bool> _canExecute;

		public RelayCommand(Action execute, Func<bool> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}

			_execute = new Action(execute);

			if (canExecute != null)
			{
				_canExecute = new Func<bool>(canExecute);
			}
		}

		/// <summary>
		/// Initializes a new instance of the RelayCommand class that 
		/// can always execute.
		/// </summary>
		/// <param name="execute">The execution logic.</param>
		/// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
		public RelayCommand(Action execute)
			: this(execute, null)
		{
		}

		/// <summary>
		/// Occurs when changes occur that affect whether the command should execute.
		/// </summary>
		public event EventHandler CanExecuteChanged;


		public void RaiseCanExecuteChanged()
		{
		
			var handler = CanExecuteChanged;
			if (handler != null)
			{
			handler(this, EventArgs.Empty);
			}
		}

		public bool CanExecute(object parameter)
		{
			if (_canExecute == null)
				return true;

			return _canExecute.Invoke();
		}

		public virtual void Execute(object parameter)
		{
			if (CanExecute(parameter)
				&& _execute != null)
			{
				_execute.Invoke();
			}
		}
	}
}

