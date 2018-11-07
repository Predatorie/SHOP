// <copyright file="ShellViewModel.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SHOP.ViewModels
{
    using System.Windows.Input;

    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;

    using Mastercam;

    using Prism.Commands;
    using Prism.Mvvm;

    using SHOP.Localization;
    using SHOP.Services;

    public class ShellViewViewModel : BindableBase
    {
        #region Private Fields

        /// <summary>
        /// Backing field for the ISerializationManager property
        /// </summary>
        private readonly ISerializationManager manager;

        /// <summary>
        /// Backing field for the IDialogCoordinator property
        /// </summary>
        private readonly IDialogCoordinator dialogCoordinator;

        /// <summary>
        /// Backing field for the Title property
        /// </summary>
        private string title;

        /// <summary>
        /// Backing field for the XML property
        /// </summary>
        private string xml;

        /// <summary>
        /// Backing field for the File property
        /// </summary>
        private string file;

        /// <summary>
        /// Backing field for the Categories property
        /// </summary>
        private Categories categories;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewViewModel"/> class.
        /// </summary>
        /// <param name="serializationManager">The ISerializationManager singleton</param>
        /// <param name="dialogCoordinator">The IDialogCoordinator singleton</param>
        public ShellViewViewModel(
            ISerializationManager serializationManager,
            IDialogCoordinator dialogCoordinator)
        {
            this.Title = AppStrings.Title;

            this.manager = serializationManager;
            this.dialogCoordinator = dialogCoordinator;

            this.SaveCommand = new DelegateCommand(
                    this.OnSaveCommand,
                    () => this.Categories != null).ObservesProperty(() => this.Categories);

            this.SaveAsCommand = new DelegateCommand(
                    this.OnSaveAsCommand,
                    () => this.Categories != null).ObservesProperty(() => this.Categories);

            this.OpenCommand = new DelegateCommand(this.OnOpenCommand);

            this.CloseCommand = new DelegateCommand<MetroWindow>(v => v?.Close());

            this.Open();
        }

        #endregion

        #region Public Commands

        public ICommand SaveCommand { get; }

        public ICommand SaveAsCommand { get; }

        public ICommand OpenCommand { get; }

        public ICommand CloseCommand { get; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the Title property value
        /// </summary>
        public string Title
        {
            get => this.title;
            private set => this.SetProperty(ref this.title, value);
        }

        /// <summary>
        /// Gets or sets gets the XML property value
        /// </summary>
        public string XML
        {
            get => this.xml;
            set => this.SetProperty(ref this.xml, value);
        }

        /// <summary>
        /// Gets or sets gets the Categories property value
        /// </summary>
        public Categories Categories
        {
            get => this.categories;
            set => this.SetProperty(ref this.categories, value);
        }

        /// <summary>
        /// Gets the File property value
        /// </summary>
        public string File
        {
            get => this.file;
            private set => this.SetProperty(ref this.file, value);
        }

        #endregion

        #region Private Methods

        private void Open()
        {
            var result = this.manager.Open();
            if (result.IsFailure)
            {
                // Ignore user cancelling out of the dialog
                if (result.Error.Contains(AppStrings.UserCancelled))
                {
                    return;
                }

                // Valid error
                this.ShowErrorMessage(result.Error);
            }
            else
            {
                // Update the data source
                this.Categories = result.Value;
            }
        }

        private void OnOpenCommand()
        {
            var result = this.manager.Open();
            if (result.IsFailure)
            {
                // Ignore user cancelling out of the dialog
                if (result.Error.Contains(AppStrings.UserCancelled))
                {
                    return;
                }

                // Valid error
                this.ShowErrorMessage(result.Error);
            }
            else
            {
                // Update the data source
                this.Categories = result.Value;
            }
        }

        private void OnSaveAsCommand()
        {
            var result = this.manager.SaveAs(this.Categories);
            if (result.IsFailure)
            {
                // Ignore user cancelling out of the dialog
                if (result.Error.Contains(AppStrings.UserCancelled))
                {
                    return;
                }

                // Valid error
                this.ShowErrorMessage(result.Error);
            }
            else
            {
                // Update the active file
                this.File = result.Value;
            }
        }

        private async void OnSaveCommand()
        {
            var result = this.manager.Save(this.Categories, this.File);
            if (result.IsFailure)
            {
                // Valid error serializing to disk
                this.ShowErrorMessage(result.Error);
                return;
            }

            await this.dialogCoordinator.ShowMessageAsync(
             this,
             AppStrings.Title,
             AppStrings.FileSaved,
             MessageDialogStyle.Affirmative,
             this.CreateMetroDialogSettings());
        }

        private MetroDialogSettings CreateMetroDialogSettings() => new MetroDialogSettings
        {
            AffirmativeButtonText = AppStrings.OK,
            AnimateShow = true,
            AnimateHide = false,
            ColorScheme = MetroDialogColorScheme.Accented,
        };

        private async void ShowErrorMessage(string error) => await this.dialogCoordinator.ShowMessageAsync(
                this,
                AppStrings.Title,
                error,
                MessageDialogStyle.Affirmative,
                this.CreateMetroDialogSettings());

        #endregion
    }
}
