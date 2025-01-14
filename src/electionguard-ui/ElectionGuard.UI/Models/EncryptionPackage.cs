﻿namespace ElectionGuard.UI.Models;


/// <summary>
/// Representative of all data needed to export an encryption Package
/// </summary>
public record EncryptionPackage
{
    public string Context { get; init; }
    public string Constants { get; init; }
    public string Manifest { get; init; }

    private const string MANIFEST_FILE = "manifest.json";
    private const string CONSTANTS_FILE = "constants.json";
    private const string CONTEXT_FILE = "context.json";

    public EncryptionPackage(string context, string constants, string manifest)
    {
        Context = context;
        Constants = constants;
        Manifest = manifest;
    }

    public EncryptionPackage(ContextRecord contextRecord, ConstantsRecord constantsRecord, ManifestRecord manifestRecord)
    {
        Context = contextRecord.ToString();
        Constants = constantsRecord.ToString();
        Manifest = manifestRecord.ToString();
    }


    /// <summary>
    /// Make a list of files for export into <see cref="IStorageService"/>
    /// </summary>
    /// <param name="package">The election package</param>
    public static implicit operator List<FileContents>(EncryptionPackage package)
    {
        return new()
        {
            new(MANIFEST_FILE, package.Manifest),
            new(CONTEXT_FILE, package.Context),
            new(CONSTANTS_FILE, package.Constants),
        };
    }
}
