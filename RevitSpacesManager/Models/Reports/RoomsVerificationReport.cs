﻿using System.Collections.Generic;

namespace RevitSpacesManager.Models
{
    internal class RoomsVerificationReport
    {
        internal List<RoomElement> VerifiedRooms { get; } = new List<RoomElement>();
        internal List<RoomElement> IncorrectyPlacedRooms { get; } = new List<RoomElement>();
        internal List<RoomElement> IncorrectLevelRooms { get; } = new List<RoomElement>();


        internal RoomsVerificationReport(RevitDocument revitDocument, List<RoomElement> roomElements)
        {
            foreach (RoomElement roomElement in roomElements)
            {
                if (IsRoomNotPlacedCorrectly(roomElement))
                {
                    IncorrectyPlacedRooms.Add(roomElement);
                }
                else if (IsRoomLevelNotAvailableInRevitDocument(revitDocument, roomElement))
                {
                    IncorrectLevelRooms.Add(roomElement);
                }
                else
                {
                    VerifiedRooms.Add(roomElement);
                }
            }
        }


        private bool IsRoomNotPlacedCorrectly(RoomElement roomElement)
        {
            double area = roomElement.Area;
            if (area > 0)
                return false;
            return true;
        }

        private bool IsRoomLevelNotAvailableInRevitDocument(RevitDocument revitDocument, RoomElement roomElement)
        {
            // TODO - level available 
            // TODO - level upper limit available
            // TODO - level same elevation
            // TODO - level upper limit same elevation
            return true;
        }
    }
}