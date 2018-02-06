// <copyright file="ISerializationService.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SolidsHoleOperationPresets.Services
{
    using Mastercam;
    using Models;

    public interface ISerializationService
    {
        Result<bool> Serialize(Categories categories, string path);

        Result<Categories> DeSerialize(string path);
    }
}