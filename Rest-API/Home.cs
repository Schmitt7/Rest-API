using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API
{
    public class Home
    {
        private int homeId, realtorId, homeAge;
        private string address, description, homeType, city, state;
        private string[] images;
        private DateTime dateListed;
        private Collection<Room> rooms;
        private decimal price;

        public Home(int homeId, string address, int realtorId, decimal price, string[] images, string description, DateTime dateListed, Collection<Room> rooms, string homeType, int homeAge, string city, string state) {
            this.homeId = homeId;
            this.address = address;
            this.realtorId = realtorId;
            this.price = price;
            this.images = images;
            this.description = description;
            this.dateListed = dateListed;
            this.rooms = rooms;
            this.homeType = homeType;
            this.homeAge = homeAge;
            this.city = city;
            this.state = state;
        }

        //Returns number of bathrooms
        public int GetBathrooms()
        {
            int count = 0;

            foreach(Room room in rooms)
                if (room.Type.ToLower().Equals("bathroom"))
                    count++;

            return count;
        }

        public int GetBedrooms()
        {
            int count = 0;

            foreach (Room room in rooms)
                if (room.Type.ToLower().Equals("bedroom"))
                    count++;

            return count;
        }

        //Calculates Time House has been on the market (Extra Credit)
        public TimeSpan ListingAge(DateTime ListedDate) {
            return DateTime.Now - ListedDate;
        }

        //Calculate Square Footage
        public decimal GetSquareFootage()
        {
            decimal squareFootage = 0;

            foreach (Room room in rooms)
                squareFootage += room.GetSquareFootage();

            return squareFootage;
        }

        //Properties
        public int HomeID { get { return homeId; } }
        public string Address { get { return address; } set { address = value; } }
        public int RealtorID { get { return realtorId; } }
        public decimal Price { get { return price; } set { price = value; } }
        public string[] Images { get { return images; } set { images = value; } }
        public string Description { get { return description; } set { description = value; } }
        public DateTime DateListed { get { return dateListed; } }
        public Collection<Room> Rooms { get { return rooms; } set { rooms = value; } }
        public string HomeType { get { return homeType; } set { homeType = value; } }
        public int HomeAge { get { return homeAge; } set { homeAge = value; } }
        public string City { get { return city; } set { city = value; } }
        public string State { get { return state; } set { state = value; } }
    }
}
