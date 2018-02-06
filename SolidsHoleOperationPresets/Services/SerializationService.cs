// <copyright file="SerializationService.cs" company="Mick George @Osoy">
// Copyright (c) Mick George @Osoy. All rights reserved.
// </copyright>

namespace SolidsHoleOperationPresets.Services
{
    using System;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Mastercam;
    using Models;

    public class SerializationService : ISerializationService
    {
        public Result<bool> Serialize(Categories categories, string path)
        { 
            return Result.Ok(default(bool));
        }

        public Result<Categories> DeSerialize(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Categories));
                var config = new XmlReaderSettings
                {
                    ValidationType = ValidationType.Schema
                };

                config.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                config.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;

                var assembly = Assembly.GetExecutingAssembly();
                using (var schemaStream = assembly.GetManifestResourceStream("SolidsHoleOperationPresets.HolePresets.xsd"))
                {
                    if (schemaStream != null)
                    {
                        var schema = XmlSchema.Read(schemaStream, null);
                        config.Schemas.Add(schema);

                        using (var xmlReader = XmlReader.Create(path, config))
                        {
                            var categories = (Categories)serializer.Deserialize(xmlReader);
                            return categories == null ?
                                Result.Fail<Categories>($"Deserialization failed {path}") :
                                Result.Ok(categories);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Result.Fail<Categories>(e.Message);
            }

            return Result.Fail<Categories>($"Deserialization failed {path}");
        }
    }
}