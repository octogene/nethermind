// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

using System.Threading.Tasks;
using FluentAssertions;
using Nethermind.Consensus;
using Nethermind.Core;
using Nethermind.Core.Crypto;
using NUnit.Framework;
// ReSharper disable AssignNullToNotNullAttribute

namespace Nethermind.Blockchain.Test.Consensus
{
    [TestFixture]
    public class NullSignerTests
    {
        [Test, MaxTime(Timeout.MaxTestTime)]
        public void Test()
        {
            NullSigner signer = NullSigner.Instance;
            signer.Address.Should().Be(Address.Zero);
            signer.CanSign.Should().BeTrue();
        }

        [Test, MaxTime(Timeout.MaxTestTime)]
        public async Task Test_signing()
        {
            NullSigner signer = NullSigner.Instance;
            await signer.Sign((Transaction)null!);
            signer.Sign((Hash256)null!).Bytes.Length.Should().Be(64);
        }
    }
}
