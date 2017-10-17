﻿using System;
using Shared.Network;

namespace Shared.Util.Configuration.Files
{
    /// <summary>
    ///     Represents log.conf
    /// </summary>
    public class LogConfFile : ConfFile
    {
        public bool Archive { get; protected set; }
        public LogLevel Hide { get; protected set; }
        public bool DumpIncomingPackets { get; protected set; }
        public bool DumpOutgoingPackets { get; protected set; }

        public void Load()
        {
            Require("system/conf/log.conf");

            Archive = GetBool("archive", true);
            Hide = (LogLevel) GetInt("cmd_hide", (int) LogLevel.Debug);

            if (Archive)
                Log.Archive = "log/archive/";
            Log.LogFile = $"log/{AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "").Replace(".vshost", "")}.txt";
            Log.Hide |= Hide;
            
            DumpIncomingPackets = GetBool("dump_incoming", true);
            DumpOutgoingPackets = GetBool("dump_outgoing", true);
            DefaultServer.DumpIncoming = DumpIncomingPackets;
            DefaultServer.DumpOutgoing = DumpOutgoingPackets;
        }
    }
}