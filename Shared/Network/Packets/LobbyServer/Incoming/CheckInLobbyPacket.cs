﻿namespace Shared.Network.LobbyServer
{
    public class CheckInLobbyPacket
    {
        /// <summary>
        ///     The used protocol version on client side
        /// </summary>
        public readonly uint ProtocolVersion;

        /// <summary>
        ///     The currently logged in user ticket in string format reported by client
        ///     DO NOT TRUST! RE-CHECK WITH SERVER VALUES
        /// </summary>
        public readonly string StringTicket;

        /// <summary>
        ///     The currently logged in username reported by client
        ///     DO NOT TRUST! RE-CHECK WITH SERVER VALUES
        /// </summary>
        public readonly uint Ticket;

        /// <summary>
        ///     The time
        /// </summary>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Time
        public readonly uint Time;

        /// <summary>
        ///     The currently logged in user ticket reported by client
        ///     DO NOT TRUST! RE-CHECK WITH SERVER VALUES
        /// </summary>
        public readonly string Username;

        public CheckInLobbyPacket(Packet packet)
        {
            ProtocolVersion = packet.Reader.ReadUInt32();
            Ticket = packet.Reader.ReadUInt32();
            Username = packet.Reader.ReadUnicodeStatic(0x28);
            Time = packet.Reader.ReadUInt32();
            StringTicket = packet.Reader.ReadAsciiStatic(0x40);

            // TODO: 50 bytes of data missing here.
        }
    }
}