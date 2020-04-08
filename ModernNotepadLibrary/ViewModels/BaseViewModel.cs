using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernNotepadLibrary.ViewModels
{
    /// <summary>
    /// Base class for ViewModels that implements the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Sets the new value of the backing field associate to a given property.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="oldValue">The original value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="propertyName">Returns true if the value changed, otherwise returns false.</param>
        /// <returns></returns>
        protected bool Set<T>(ref T oldValue, T newValue, [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                return false;
            }
            oldValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Called when a property in the ViewModel changes. Can be overriden in a derived class.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
