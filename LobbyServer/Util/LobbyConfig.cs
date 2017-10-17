﻿using Shared.Util.Configuration;

namespace LobbyServer.Util
{
    public class LobbyConf : BaseConf
    {
        public LobbyConf()
        {
            Lobby = new LobbyConfFile();
        }

        /// <summary>
        ///     login.conf
        /// </summary>
        public LobbyConfFile Lobby { get; protected set; }

        public override void Load()
        {
            LoadDefault();
            Lobby.Load();
        }
    }

    /// <summary>
    ///     Represents login.conf
    /// </summary>
    public class LobbyConfFile : ConfFile
    {
        public int Port { get; protected set; }
        public int NewCharacterMito { get; protected set; }
        public int NewCharacterHancoin { get; protected set; }

        public void Load()
        {
            Require("system/conf/lobby.conf");

            Port = GetInt("port", 11011);
            NewCharacterMito = GetInt("newCharacterMito", 10000);
            NewCharacterHancoin = GetInt("newCharacterHancoin", 10000);
        }
    }
}