// <copyright file="SerializationManager.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SHOP.Services
{
    using Mastercam;

    using SHOP.Models;

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
            var data = this.serialization.DeSerialize();
            return data.IsFailure ?
                Result.Fail<Categories>(data.Error) :
                Result.Ok(data.Value);
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