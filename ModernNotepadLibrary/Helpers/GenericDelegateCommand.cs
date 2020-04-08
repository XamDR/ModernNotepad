using System;
using System.Reflection;

namespace ModernNotepadLibrary.Helpers
{
    /// <summary>
    /// A generic version of the <see cref="DelegateCommand"/> class.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    public class DelegateCommand<T> : DelegateCommand
    {
        public DelegateCommand(Action<T> action)
            : base(o =>
            {
                if (IsValidParamater(o))
                {
                    action((T)o);
                }
            })
        { }

        public DelegateCommand(Action<T> action, Func<T, bool> canExecuteAction)
            : base(o =>
            {
                if (IsValidParamater(o))
                {
                    action((T)o);
                }
            }, o => IsValidParamater(o) && canExecuteAction((T)o))
        { }

        private static bool IsValidParamater(object parameter)
        {
            if (parameter != null)
            {
                return parameter is T;
            }
            var type = typeof(T);

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true;
            }
            return !type.GetTypeInfo().IsValueType;
        }
    }
}
