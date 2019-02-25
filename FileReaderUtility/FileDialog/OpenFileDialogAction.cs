using Microsoft.Win32;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace FileReaderUtility.FileDialog
{
    /// <summary>
    /// This class represents the action of showing an Open File dialog
    /// </summary>
    /// <seealso cref="TriggerAction{FrameworkElement}" />
    public class OpenFileDialogAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// Invokes the action with the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void Invoke(object parameter)
        {
            InteractionRequestedEventArgs args = (InteractionRequestedEventArgs)parameter;
            FileDialogConfirmation ctx = (FileDialogConfirmation)args.Context;
            OpenFileDialog ofd = new OpenFileDialog
            {
                AddExtension = ctx.AddExtension,
                FileName = ctx.FileName,
                Filter = ctx.Filter,
            };

            if (ofd.ShowDialog() == true)
            {
                ctx.Confirmed = true;
                ctx.FileName = ofd.FileName;
                ctx.SafeFileName = ofd.SafeFileName;
            }
            else
            {
                ctx.Confirmed = false;
            }

            args.Callback?.Invoke();
        }
    }
}
