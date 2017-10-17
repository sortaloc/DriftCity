﻿using System;
using System.IO;
using Shared.Util;

namespace Shared.Network.LobbyServer
{
    /// <summary>
    /// sub_53D320
    /// </summary>
    public class LobbyTimeAnswerPacket : OutPacket
    {
        public int LocalTime;
        public int TimeT;

        public LobbyTimeAnswerPacket()
        {
            LocalTime = Environment.TickCount;
            TimeT = Environment.TickCount;
        }
        
        public override Packet CreatePacket()
        {
            return base.CreatePacket(Packets.LobbyTimeAck);
        }

        public override int ExpectedSize() => 10;

        public override byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bs = new BinaryWriterExt(ms))
                {
                    bs.Write(LocalTime);
                    bs.Write(TimeT);
                }
                return ms.ToArray();
            }
        }
    }
}