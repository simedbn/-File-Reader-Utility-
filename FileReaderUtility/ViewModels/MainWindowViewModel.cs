using FileReaderUtility.FileDialog;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.IO;

namespace FileReaderUtility.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        #region Fields
        private string _title = "File reader utility";
        private string _filePath;
        private string _fileResult;
        #endregion

        #region Properties
        /// <summary>
        /// Window title.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// File Path.
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }

        /// <summary>
        /// File Path.
        /// </summary>
        public string FileResult
        {
            get { return _fileResult; }
            set { SetProperty(ref _fileResult, value); }
        }

        /// <summary>
        /// Interaction request to open the file.
        /// </summary>
        public InteractionRequest<FileDialogConfirmation> OpenFileRequest { get; private set; }

        #endregion

        #region Commands
        /// <summary>
        /// Open File Dialog command.
        /// </summary>
        public DelegateCommand OpenFileDialogCommand { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// The command action.
        /// </summary>
        private void OpenFileDialogMethod()
        {
            // The open file dialog confirmation.
            var fileConfirmation = new FileDialogConfirmation();

            fileConfirmation.Title = "Choose the file to read";
            fileConfirmation.AddExtension = true;
            fileConfirmation.Filter = "Text files (*.txt)|*.txt|XML (*.*)|*.xml";

            // Show the dialog.
            OpenFileRequest.Raise(fileConfirmation, (of) =>
            {
                // Ge the result
                FilePath = of.FileName;
                if (FilePath != null)
                    FileResult = File.ReadAllText(of.FileName);
            });
        }
        #endregion


        public MainWindowViewModel()
        {
            OpenFileRequest = new InteractionRequest<FileDialogConfirmation>();
            OpenFileDialogCommand = new DelegateCommand(OpenFileDialogMethod);
        }
    }
}
