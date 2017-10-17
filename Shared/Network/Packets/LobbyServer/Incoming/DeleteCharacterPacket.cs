﻿namespace Shared.Network.LobbyServer
{
    public class DeleteCharacterPacket
    {
        /*
        000000: 41 00 64 00 6D 00 69 00 6E 00 00 00 00 01 00 00  A · d · m · i · n · · · · · · ·
        000016: 00 40 00 00 00 00 00 00 01 00 00 00 00 00 00 00  · @ · · · · · · · · · · · · · ·
        000032: 00 00 00 00 00 00 00 00 00 00 02 00 00 00 00 00  · · · · · · · · · · · · · · · ·
        000048: 00 00 52 00 00 00 03 00 00 00  · · R · · · · · · ·

        000000: 41 00 64 00 6D 00 69 00 6E 00 69 00 73 00 74 00  A · d · m · i · n · i · s · t ·
        000016: 72 00 61 00 74 00 6F 00 72 00 00 00 00 00 00 00  r · a · t · o · r · · · · · · ·
        000032: 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00  · · · · · · · · · · · · · · · ·
        000048: 00 00 52 00 00 00 03 00 00 00  · · R · · · · · · ·
        */

        public readonly string CharacterName;

        public DeleteCharacterPacket(Packet packet)
        {
            CharacterName = packet.Reader.ReadUnicodeStatic(21);
        }
    }
}