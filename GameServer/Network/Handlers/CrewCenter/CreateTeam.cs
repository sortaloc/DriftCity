﻿using System;
using Shared.Models;
using Shared.Network;
using Shared.Objects;
using Shared.Util;

namespace GameServer.Network.Handlers
{
    public static class CreateTeam
    {
        [Packet(Packets.CmdCreateTeam)]
        public static void Handle(Packet packet)
        {
            if (packet.Sender.User == null)
            {
                packet.Sender.SendError("Session game information is abnormal. Please log-in again.");
                packet.Sender.KillConnection("User was not loaded.");
                return;
            }
            
            if (packet.Sender.User.ActiveCharacter == null)
            {
                packet.Sender.SendError("Session character information is abnormal. Please log-in again.");
                packet.Sender.KillConnection("Character was not loaded.");
                return;
            }
            
            var crew = new Crew();
            crew.Unserialize(packet.Reader);

            var cost = GameServer.Instance.Config.Game.CrewCreationCost;
            var levelReq = GameServer.Instance.Config.Game.CrewCreationMinLevel;
            var character = packet.Sender.User.ActiveCharacter;

            // Check if char already is in a crew
            if (character.Crew != null || character.CrewId != -1)
            {
                packet.Sender.SendError("You are already a member of a crew.");
                return;
            }
            
            // Check if the crew name really doesn't exist
            if (CrewModel.CheckNameExists(GameServer.Instance.Database.Connection, crew.Name))
            {
                packet.Sender.SendError("This crew name is already in use.");
                Log.Warning(
                    $"{packet.Sender.User.Username} suspected packet hacking. Tried to create a crew with a name that already is used!");
                return;
            }
            
            // Check if he has enough mito to "buy" the crew
            if (character.MitoMoney < cost && packet.Sender.User.Permission < UserPermission.Developer)
            {
                packet.Sender.SendError($"{cost} Mito is required to make a crew.");
                return;
            }

            // Check if he has the level required (Ignore if he's dev or higher.)
            if (character.Level < levelReq && packet.Sender.User.Permission < UserPermission.Developer)
            {
                packet.Sender.SendError($"Only users at Level {levelReq} or higher can make a crew.");
                return;
            }
            
            crew.LeaderName = packet.Sender.User.ActiveCharacter.Name;
            crew.LeaderId = (long) packet.Sender.User.ActiveCharacterId;
            crew.OwnerName = packet.Sender.User.ActiveCharacter.Name;
            crew.OwnerId = (long) packet.Sender.User.ActiveCharacterId;
            crew.CreateDate = (uint) DateTimeOffset.Now.ToUnixTimeMilliseconds();
            crew.MemberCnt = 1;
            crew.Point = 0;

            if (!CrewModel.Create(GameServer.Instance.Database.Connection, ref crew))
            {
                packet.Sender.SendError("Failed to create crew. Please try again later!");
                return;
            }
            
            packet.Sender.User.ActiveCharacter.CrewId = crew.Id;
            packet.Sender.User.ActiveCharacter.CrewRank = 1; // Crew Master
            packet.Sender.User.ActiveCharacter.Crew = crew;
            packet.Sender.User.ActiveCharacter.MitoMoney -= cost;
            if (!CharacterModel.Update(GameServer.Instance.Database.Connection, packet.Sender.User.ActiveCharacter))
            {
                packet.Sender.SendError("Failed to set your crew!");
                return;
            }

            var ack = new Packet(Packets.CreateTeamAck);
            ack.Writer.Write(1); // Result? 1=Correct, 2=Wrong? Does this even matter?
            ack.Writer.Write(crew);
            ack.Writer.Write((ushort) 0); // Session Age?
            ack.Writer.Write(packet.Sender.User.ActiveCharacter.MitoMoney); // Result money..
            packet.Sender.Send(ack);

            // sub_546B80 => 680 
            /*
            int m_Result;
            XiStrTeamInfo m_TeamInfo;
            unsigned __int16 m_Age;
            __int64 m_ResultMoney;
            */
        }
    }
}