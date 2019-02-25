using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderUtility.FileDialog
{
    public class FileDialogConfirmation : Confirmation
    {
        #region Properties

        /// <summary>
        /// Gets or sets the filter string that determines what types of files are displayed
        /// from either the <see cref="Microsoft.Win32.OpenFileDialog"/> or <see cref="Microsoft.Win32.SaveFileDialog"/>.
        /// </summary>
        /// <value>
        /// A <see cref="System.String"/> that contains the filter. The default is <see cref="System.String.Empty"/>,
        /// which means that no filter is applied and all file types are displayed.
        /// </value>
        /// <exception cref="System.ArgumentException">
        /// The filter string is invalid.
        /// </exception>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets a string containing the full path of the file selected in a file dialog.
        /// </summary>
        /// <value>
        /// A <see cref="System.String"/> that is the full path of the file selected in the file dialog.
        /// The default is <see cref="System.String.Empty"/>.
        /// </value>
        public string FileName { get; set; }


        /// <summary>
        /// Gets a string that only contains the file name for the selected file.
        /// </summary>
        /// <value>
        /// A <see cref="System.String"/> that only contains the file name for the selected file. 
        /// The default is <see cref="System.String.Empty"/>, which is also the value when either no file is selected
        /// or a directory is selected.
        /// </value>
        public string SafeFileName { get; set; }


        /// <summary>
        /// Gets or sets a value that specifies whether warnings are displayed if the user
        /// types invalid paths and file names.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if warnings are displayed; otherwise, <see langword="false"/>. 
        /// The default is <see langword="true"/>.
        /// </value>
        public bool CheckPathExists { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a file dialog displays a warning if the
        /// user specifies a file name that does not exist.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if warnings are displayed; otherwise, <see langword="false"/>. 
        /// The default in this base class is <see langword="false"/>.
        /// </value>
        public bool CheckFileExists { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a file dialog automatically adds an extension
        /// to a file name if the user omits an extension.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if extensions are added; otherwise, <see langword="false"/>. 
        /// The default is <see langword="true"/>.
        /// </value>
        public bool AddExtension { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="FileDialogConfirmation" />
        /// </summary>
        public FileDialogConfirmation()
        {
            SafeFileName = string.Empty;
            CheckPathExists = true;
            CheckFileExists = false;
            AddExtension = true;
        }
        #endregion
    }

}