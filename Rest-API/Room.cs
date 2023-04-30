using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rest_API
{
    public class Room
    {
        private int roomId, homeId;
        private string type;
        private decimal length, width;

        public Room(int roomId, int homeId, string type, decimal length, decimal width) {
            this.homeId = homeId;
            this.roomId = roomId;
            this.type = type;
            this.length = length;
            this.width = width;
        }

        //Function that gets the square footage of the room
        public decimal GetSquareFootage()
        {
            return length*width;
        }

        //Properties
        public int RoomId { get { return roomId; } }
        public int HomeId { get { return homeId; } }
        public string Type { get { return type; } }
        public decimal Length { get { return length; } set { length = value;  } }
        public decimal Width { get { return width; } set { width = value; } }
    }
}