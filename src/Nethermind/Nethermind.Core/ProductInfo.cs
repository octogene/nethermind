// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Nethermind.Core;

public static class ProductInfo
{
    static ProductInfo()
    {
        var assembly = Assembly.GetEntryAssembly()!;
        var metadataAttrs = assembly.GetCustomAttributes<AssemblyMetadataAttribute>()!;
        var productAttr = assembly.GetCustomAttribute<AssemblyProductAttribute>()!;
        var versionAttr = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!;
        var timestamp = metadataAttrs?.FirstOrDefault(static a => a.Key.Equals("BuildTimestamp", StringComparison.Ordinal))?.Value;

        BuildTimestamp = long.TryParse(timestamp, out var t)
            ? DateTimeOffset.FromUnixTimeSeconds(t)
            : DateTimeOffset.MinValue;
        Name = productAttr?.Product ?? "Nethermind";
        OS = Platform.GetPlatformName();
        OSArchitecture = RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant();
        Runtime = RuntimeInformation.FrameworkDescription;
        Version = versionAttr.InformationalVersion;

        var index = Version.IndexOf('+', StringComparison.Ordinal);

        if (index != -1)
        {
            Commit = Version[(index + 1)..];
            Version = Version[..Math.Min(index + 9, Version.Length - 1)];
        }

        ClientIdParts = new Dictionary<string, string>
        {
            { "name", Name },
            { "version", $"v{Version}" },
            { "os", $"{OS.ToLowerInvariant()}-{OSArchitecture}" },
            { "runtime", $"dotnet{Runtime[5..]}" }
        };

        ClientId = FormatClientId("{name}/{version}/{os}/{runtime}");
        PublicClientId = ClientId;
    }

    public static DateTimeOffset BuildTimestamp { get; }

    private static string FormatClientId(string formatString)
    {
        return ClientIdParts.Aggregate(formatString, (current, placeholder) =>
            current.Replace($"{{{placeholder.Key}}}", placeholder.Value)
        );
    }

    public static string ClientId { get; }

    public static string ClientCode { get; } = "NM";

    public static string Commit { get; set; } = string.Empty;

    public static string Name { get; }

    public static string OS { get; }

    public static string OSArchitecture { get; }

    public static string Runtime { get; }

    public static string Version { get; }

    public static string Network { get; set; } = string.Empty;

    public static string Instance { get; set; } = string.Empty;

    public static string SyncType { get; set; } = string.Empty;

    public static string PruningMode { get; set; } = string.Empty;

    private static Dictionary<string, string> ClientIdParts { get; }

    public static string PublicClientId { get; private set; }

    public static void InitializePublicClientId(string formatString)
    {
        PublicClientId = FormatClientId(formatString);
    }
}
