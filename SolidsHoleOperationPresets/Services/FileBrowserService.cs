// <copyright file="FileBrowserService.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SHOP.Services
{
    using System.Windows.Forms;

    using Mastercam.IO;

    using SHOP.Localization;
    using SHOP.Models;

    public class FileBrowserService : IFileBrowserService
    {
        public Result<string> Open()
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.InitialDirectory = SettingsManager.SharedDirectory;
                dlg.Filter = @"Operation Presets (*.xml)|*.xml";
                dlg.CheckFileExists = true;
                dlg.Multiselect = false;
                dlg.Title = AppStrings.OpenFile;

                switch (dlg.ShowDialog())
                {
                    case DialogResult.OK:
                        return Result.Ok(dlg.FileName);
                    default:
                        return Result.Fail<string>(AppStrings.UserCancelled);
                }
            }
        }

        public Result<string> SaveAs()
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.CreatePrompt = true;
                dlg.OverwritePrompt = true;
                dlg.InitialDirectory = SettingsManager.SharedDirectory;
                dlg.Filter = @"Operation Presets (*.xml)|*.xml";
                dlg.Title = AppStrings.SaveFile;

                switch (dlg.ShowDialog())
                {
                    case DialogResult.OK:
                        return Result.Ok(dlg.FileName);
                    default:
                        return Result.Fail<string>(AppStrings.UserCancelled);
                }
            }
        }
    }
}