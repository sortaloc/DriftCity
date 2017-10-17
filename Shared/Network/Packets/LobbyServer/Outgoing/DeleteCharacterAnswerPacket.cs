﻿using System.IO;
using Shared.Util;

namespace Shared.Network.LobbyServer
{
    /// <summary>
    /// sub_53D1A0
    /// </summary>
    public class DeleteCharacterAnswerPacket : OutPacket
    {
        public string CharacterName;

        public override Packet CreatePacket()
        {
            return base.CreatePacket(Packets.DeleteCharAck);
        }

        public override int ExpectedSize() => 44;

        public override byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bs = new BinaryWriterExt(ms))
                {
                    bs.WriteUnicodeStatic(CharacterName, 21);
                }
                return ms.ToArray();
            }
        }
    }
}