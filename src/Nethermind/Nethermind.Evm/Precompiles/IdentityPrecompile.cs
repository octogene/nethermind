// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

using System;
using Nethermind.Core;
using Nethermind.Core.Specs;

namespace Nethermind.Evm.Precompiles
{
    public class IdentityPrecompile : IPrecompile<IdentityPrecompile>
    {
        public static readonly IdentityPrecompile Instance = new();

        private IdentityPrecompile()
        {
        }

        public static Address Address { get; } = Address.FromNumber(4);

        public long BaseGasCost(IReleaseSpec releaseSpec) => 15L;

        public long DataGasCost(ReadOnlyMemory<byte> inputData, IReleaseSpec releaseSpec) => 3L * EvmPooledMemory.Div32Ceiling((ulong)inputData.Length);

        public (byte[], bool) Run(ReadOnlyMemory<byte> inputData, IReleaseSpec releaseSpec)
        {
            return (inputData.ToArray(), true);
        }
    }
}
