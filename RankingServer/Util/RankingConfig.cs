﻿using Shared.Util.Configuration;

namespace RankingServer.Util
{
    public class RankingConf : BaseConf
    {
        public RankingConf()
        {
            Ranking = new RankingConfFile();
        }

        /// <summary>
        ///     login.conf
        /// </summary>
        public RankingConfFile Ranking { get; protected set; }

        public override void Load()
        {
            LoadDefault();
            Ranking.Load();
        }
    }

    /// <summary>
    ///     Represents login.conf
    /// </summary>
    public class RankingConfFile : ConfFile
    {
        public int Port { get; protected set; }

        public void Load()
        {
            Require("system/conf/ranking.conf");

            Port = GetInt("port", 11078);
        }
    }
}