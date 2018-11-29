// <copyright file="SerializationService.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SHOP.Services
{
    using Mastercam;
    using SHOP.Models;
    using System;
    using System.Diagnostics;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public class SerializationService : ISerializationService
    {
        public Result<bool> Serialize(Categories categories, string path)
        {
            return Result.Ok(default(bool));
        }

        public Result<Categories> DeSerialize()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Categories));
                var config = new XmlReaderSettings
                {
                    ValidationType = ValidationType.Schema
                };

                //config.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                //config.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;

                using (var reader = new XmlTextReader(MastercamPaths.SolidHolesPresetSchema))
                {
                    var schema = XmlSchema.Read(reader, this.ValidationCallback);

                    if (schema == null)
                    {
                        return Result.Fail<Categories>("Failed to read schema");
                    }

                    config.Schemas.Add(schema);

                    // C:\Users\Public\Documents\Shared Mastercam 2020\common\FBM\SolidsHoleOperationPresets.xml
                    using (var xmlReader = XmlReader.Create(MastercamPaths.SolidHolesPresetFile, config))
                    {
                        var categories = (Categories)serializer.Deserialize(xmlReader);
                        return categories == null ?
                                   Result.Fail<Categories>($"Deserialization failed {MastercamPaths.SolidHolesPresetFile}") :
                                   Result.Ok(categories);
                    }
                }
            }
            catch (Exception e)
            {
                return Result.Fail<Categories>(e.Message);
            }
        }


        private void ValidationCallback(object sender, ValidationEventArgs args)
        {
            Debug.WriteLine(args.Message);
        }
    }
}