using FileReaderUtility.FileDialog;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace FileReaderUtility.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        #region Fields
        private string _title = "File reader utility";
        private string _filePath;
        private string _fileResult;
        private ObservableCollection<string> _encryptionAlgorithms;
        private string _selectedEncryptionAlgorithm;
        private string _selectedFileType;
        private ObservableCollection<string> _fileTypeList;
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
        /// File Result.
        /// </summary>
        public string FileResult
        {
            get { return _fileResult; }
            set { SetProperty(ref _fileResult, value); }
        }

        /// <summary>
        /// Selected Encryption Algorithm
        /// </summary>
        public string SelectedEncryptionAlgorithm
        {
            get { return _selectedEncryptionAlgorithm; }

            set
            {
                SetProperty(ref _selectedEncryptionAlgorithm, value);
            }
        }

        /// <summary>
        /// Selected File Type
        /// </summary>
        public string SelectedFileType
        {
            get { return _selectedFileType; }

            set
            {
                SetProperty(ref _selectedFileType, value);
            }
        }

        /// <summary>
        /// Encryption Algorithm List
        /// </summary>
        public ObservableCollection<string> EncryptionAlgorithmList { get => _encryptionAlgorithms; set => SetProperty(ref _encryptionAlgorithms, value); }

        /// <summary>
        /// Encryption Algorithm List
        /// </summary>
        public ObservableCollection<string> FileTypeList { get => _fileTypeList; set => SetProperty(ref _fileTypeList, value); }

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
            if (string.IsNullOrEmpty(SelectedFileType))
            {
                MessageBox.Show("Please Select a file type.");
                return;
            }

            // The open file dialog confirmation.
            var fileConfirmation = new FileDialogConfirmation();

            fileConfirmation.Title = "Choose the file to read";
            fileConfirmation.AddExtension = true;
            fileConfirmation.Filter = $"{SelectedFileType} files (*.{SelectedFileType})|*.{SelectedFileType}";

            // Show the dialog.
            OpenFileRequest.Raise(fileConfirmation, (of) =>
            {
                // Ge the result
                FilePath = of.FileName;
                if (FilePath != null)
                {
                    string text = File.ReadAllText(of.FileName);
                    if (text?.Length > 0)
                    {
                        switch (SelectedEncryptionAlgorithm)
                        {
                            case "AES":
                                FileResult = EncryptTextByAes(text);
                                break;
                            case "RSA":
                                FileResult = EncryptTextByRsa(text);
                                break;
                            default:
                                FileResult = text;
                                break;
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Encrypt Text By AES Algorithm
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string EncryptTextByAes(string text)
        {
            SymmetricAlgorithm aesAlgorithm = new AesManaged();
            // Create an encryptor from the AES algorithm instance and pass the aes algorithm key and inialiaztion vector to generate a new random sequence each time for the same text  
            ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

            // Create a memory stream to save the encrypted data in it  
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(cs))
                    {
                        // Write the text in the stream writer   
                        writer.Write(text);
                    }
                }

                // Get the result as a byte array from the memory stream   
                byte[] encryptedDataBuffer = ms.ToArray();

                return Encoding.UTF8.GetString(encryptedDataBuffer);
            }
        }

        /// Encrypt Text By RSA Algorithm
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string EncryptTextByRsa(string text)
        {

            // Convert the text to an array of bytes   
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = byteConverter.GetBytes(text);

            // Create a byte array to store the encrypted data in it   
            byte[] encryptedData;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                string publicKey = rsa.ToXmlString(false); // false to get the public key   

                // Set the rsa pulic key   
                rsa.FromXmlString(publicKey);

                // Encrypt the data and store it in the encyptedData Array   
                encryptedData = rsa.Encrypt(dataToEncrypt, false);

                return Encoding.UTF8.GetString(encryptedData);
            }
        }
        #endregion


        public MainWindowViewModel()
        {
            OpenFileRequest = new InteractionRequest<FileDialogConfirmation>();
            OpenFileDialogCommand = new DelegateCommand(OpenFileDialogMethod);
            EncryptionAlgorithmList = new ObservableCollection<string>
            {
                 "RSA",
                 "AES"
            };
            FileTypeList = new ObservableCollection<string>
            {

                 "TXT",
                 "XML",
                 "JSON"
            };
        }
    }
}
