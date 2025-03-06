// SPDX-FileCopyrightText: 2025 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nethermind.Evm.Tracing.GethStyle;

[JsonConverter(typeof(GethLikeTxTraceCollectionConverter))]
public record GethLikeTxTraceCollection(IReadOnlyCollection<GethLikeTxTrace> Traces) : IReadOnlyCollection<GethLikeTxTrace>
{
    public IEnumerator<GethLikeTxTrace> GetEnumerator() => Traces.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => Traces.Count;
}
