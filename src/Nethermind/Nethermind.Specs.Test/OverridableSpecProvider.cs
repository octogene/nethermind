// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

using System;
using Nethermind.Core.Specs;
using Nethermind.Int256;

namespace Nethermind.Specs.Test
{
    public class OverridableSpecProvider : ISpecProvider
    {
        private readonly ISpecProvider _specProvider;
        private readonly Func<IReleaseSpec, ForkActivation, IReleaseSpec> _overrideAction;

        public OverridableSpecProvider(ISpecProvider specProvider, Func<IReleaseSpec, IReleaseSpec> overrideAction)
            : this(specProvider, (spec, _) => overrideAction(spec))
        {
        }

        public OverridableSpecProvider(ISpecProvider specProvider, Func<IReleaseSpec, ForkActivation, IReleaseSpec> overrideAction)
        {
            _specProvider = specProvider;
            _overrideAction = overrideAction;
            TimestampFork = _specProvider.TimestampFork;
        }

        public void UpdateMergeTransitionInfo(long? blockNumber, UInt256? terminalTotalDifficulty = null)
        {
            _specProvider.UpdateMergeTransitionInfo(blockNumber, terminalTotalDifficulty);
        }

        public ForkActivation? MergeBlockNumber => _specProvider.MergeBlockNumber;

        public ulong TimestampFork { get; set; }

        public UInt256? TerminalTotalDifficulty => _specProvider.TerminalTotalDifficulty;

        public IReleaseSpec GenesisSpec => _overrideAction(_specProvider.GenesisSpec, new ForkActivation(0));

        public IReleaseSpec GetSpec(ForkActivation forkActivation) => _overrideAction(_specProvider.GetSpec(forkActivation), forkActivation);

        public long? DaoBlockNumber => _specProvider.DaoBlockNumber;
        public ulong? BeaconChainGenesisTimestamp => _specProvider.BeaconChainGenesisTimestamp;
        public ulong NetworkId => _specProvider.NetworkId;
        public ulong ChainId => _specProvider.ChainId;
        public string SealEngine => _specProvider.SealEngine;

        public ForkActivation[] TransitionActivations => _specProvider.TransitionActivations;
    }
}
