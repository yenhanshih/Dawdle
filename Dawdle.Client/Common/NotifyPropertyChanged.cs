using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dawdle.Client.Properties;

namespace Dawdle.Client.Common
{
    public class NotifyPropertyChanged
        : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}