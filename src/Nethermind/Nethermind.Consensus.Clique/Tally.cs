// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

namespace Nethermind.Consensus.Clique
{
    public class Tally
    {
        public bool Authorize { get; }
        public int Votes { get; set; }

        public Tally(bool authorize)
        {
            Authorize = authorize;
        }
    }
}
