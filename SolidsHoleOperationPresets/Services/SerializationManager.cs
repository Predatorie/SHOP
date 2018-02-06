// <copyright file="SerializationManager.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

using SolidsHoleOperationPresets.Localization;

namespace SolidsHoleOperationPresets.Services
{
    using Mastercam;
    using Models;

    public partial class SerializationManager
    {
        private readonly ISerializationService serialization;

        private readonly IFileBrowserService browser;

        public SerializationManager(
            ISerializationService serializationService,
            IFileBrowserService fileBrowserService)
        {
            this.serialization = serializationService;
            this.browser = fileBrowserService;
        }
    }

    public partial class SerializationManager : ISerializationManager
    {
        public Result<Categories> Open()
        {
            var result = this.browser.Open();
            if (result.IsFailure)
            {
                return Result.Fail<Categories>(result.Error);
            }

            var data = this.serialization.DeSerialize(result.Value);
            return data.IsFailure ?
                Result.Fail<Categories>(data.Error) :
                this.serialization.DeSerialize(result.Value);
        }

        public Result<string> SaveAs(Categories categories)
        {
            var result = this.browser.SaveAs();
            if (!result.IsSuccess)
            {
                return Result.Fail<string>(result.Error);
            }

            var success = this.serialization.Serialize(categories, result.Value);
            return success.IsFailure ?
                Result.Fail<string>(success.Error) :
                Result.Ok(result.Value);
        }

        public Result<bool> Save(Categories categories, string file)
        {
            var result = this.serialization.Serialize(categories, file);
            return result.IsFailure ?
                Result.Fail<bool>(result.Error) :
                Result.Ok(result.Value);
        }
    }
}