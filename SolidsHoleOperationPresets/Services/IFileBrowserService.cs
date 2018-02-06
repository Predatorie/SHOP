// <copyright file="IFileBrowserService.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SolidsHoleOperationPresets.Services
{
    using Models;

    public interface IFileBrowserService
    {
        Result<string> Open();

        Result<string> SaveAs();
    }
}