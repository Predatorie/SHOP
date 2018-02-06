// <copyright file="ISerializationManager.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SolidsHoleOperationPresets.Services
{
    using Mastercam;
    using Models;

    public interface ISerializationManager
    {
        Result<Categories> Open();

        Result<string> SaveAs(Categories categories);

        Result<bool> Save(Categories categories, string file);
    }
}