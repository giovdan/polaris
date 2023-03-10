namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;


    public class ExportFileManagement
    {
        //private readonly string _fileExtension;
        //private readonly string _path;
        //private readonly ImportExportFilter _filter;

        //public ExportFileManagement(ImportExportFilter filter,string extension)
        //{
        //    _path = filter.Path;
        //    _fileExtension = extension;
        //    _filter = filter;
        //}

        //public void SaveObjectToFile(string identifiers, List<ExportImportList> objectsToExport)
        //{            
        //    // Ogni oggetto su un specifico file
        //    // Recupero il path dove salvare i files
        
        //    // Il nome del file viene dato dagli identificatori dell'oggetto
        //    var fileName =identifiers;

        //    Regex illegalInFileName = new Regex(string.Format("[{0}]"
        //                                        , Regex.Escape(new string(Path.GetInvalidFileNameChars())))
        //                                        , RegexOptions.Compiled);
        //    // Rimuovo i caratteri non permessi  
        //    fileName = illegalInFileName.Replace(fileName, ".");

        //    var file = Path.Combine(_path,$"{fileName}.{_fileExtension}");

        //    // Se la cartella non esiste la creo
        //    if (!Directory.Exists(_path))
        //        Directory.CreateDirectory(_path);

        //    var objectToCript = JsonConvert.SerializeObject(objectsToExport
        //                                                        , Newtonsoft.Json.Formatting.Indented
        //                                                        , new DictionaryToPropertiesJsonConverter());
        //    // Creeo file di testo 
        //    var exportFile = File.CreateText(file);
        //    // Recupero chiave corrispondente al contenuto criptato e la metto come prima riga del file "Polaris"
        //    exportFile.WriteLine(EncryptionManagement.GetValidationKey(objectToCript));
        //    // Aggiungo il contenuto da esportare 
        //    exportFile.Write(objectToCript);
        //    exportFile.Close();
        //}

        //public string GetFullFileName(string objectIdentifiers)
        //{
        //    //if (Enum.TryParse<ImportExportObjectEnum>(_filter.ObjectType, out var importTypeEnum))
        //    //{
        //    Regex illegalInFileName = new Regex(string.Format("[{0}]"
        //                                        , Regex.Escape(new string(Path.GetInvalidFileNameChars())))
        //                                        , RegexOptions.Compiled);
                    
        //    // Il nome del file viene dato dagli identificatori dell'oggetto
        //    var fileName = objectIdentifiers;
        //    // Rimuovo i caratteri non permessi  
        //    fileName = illegalInFileName.Replace(fileName, ".");
        //    return (Path.Combine(_path, $"{fileName}.{_fileExtension}"));    
        //    //}
        //    //else
        //    //{ return string.Empty; }
        //}

        //public void CompleteExport()
        //{
        //    //if (!_multipleFiles) // Un unico file con tutti gli oggetti da esportare 
        //    //{
        //    //    File.WriteAllText(_fileName, JsonConvert.SerializeObject(_masters
        //    //                                                            , Newtonsoft.Json.Formatting.Indented
        //    //                                                            , new DictionaryToPropertiesJsonConverter()));
        //    //}
        //}
    }
}
