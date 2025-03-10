// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

using System.Collections.Generic;
using System.IO;
using Ethereum.Test.Base.Interfaces;
using Nethermind.Core;
using Nethermind.Core.Crypto;
using Nethermind.Core.Specs;
using Nethermind.Int256;

namespace Ethereum.Test.Base
{
    public class GeneralStateTest : EthereumTest
    {
        public IReleaseSpec? Fork { get; set; }
        public string? ForkName { get; set; }
        public Address? CurrentCoinbase { get; set; }
        public UInt256 CurrentDifficulty { get; set; }

        public UInt256? CurrentBaseFee { get; set; }
        public long CurrentGasLimit { get; set; }
        public long CurrentNumber { get; set; }
        public ulong CurrentTimestamp { get; set; }
        public Hash256? PreviousHash { get; set; }
        public Dictionary<Address, AccountState> Pre { get; set; }
        public Hash256? PostHash { get; set; }
        public Hash256? PostReceiptsRoot { get; set; }
        public Transaction? Transaction { get; set; }
        public Hash256? CurrentRandom { get; set; }
        public Hash256? CurrentBeaconRoot { get; set; }
        public Hash256? CurrentWithdrawalsRoot { get; set; }
        public ulong? CurrentExcessBlobGas { get; set; }
        public UInt256? ParentBlobGasUsed { get; set; }
        public UInt256? ParentExcessBlobGas { get; set; }

        public Hash256? RequestsHash { get; set; }

        public override string ToString()
        {
            return $"{Path.GetFileName(Category)}.{Name}_{ForkName}";
        }
    }
}
