using System;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ImediateWriteJson.VM
{
    public class ViewModelBase : ObservableObject
    {

        protected bool BaseCanRunCommand() => true;


        protected void HandleException(Exception ex)
        {
            if (ex is OperationCanceledException)
            {
                return;
            }
#if DEBUG
            MessageBox.Show(ex.ToString());
#else
            MessageBox.Show(ex.Message);
#endif
        }
    }
}
