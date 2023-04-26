using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using System.Drawing;
using System.Collections.ObjectModel;

namespace Rest_API.Controllers
{
    [Produces("application/json")]
    [Route("api/HomeService")]
    public class HomeController : Controller
    {



        //Gets all homes and adds them to a list of home objects
        [HttpGet]
        [HttpGet("GetHomes")]
        public Collection<Home> Get() {
            DBConnect roomDB = new DBConnect();
            DataSet roomDS;
            Collection<Room> rooms = new Collection<Room>();

            DBConnect homeDB = new DBConnect();
            DataSet homeDS = homeDB.GetDataSet("SELECT * FROM TP_Homes");
            Collection<Home> homes = new Collection<Home>();
            for (int i = 0; i < homeDS.Tables[0].Rows.Count; i++)
            {
                roomDS = roomDB.GetDataSet("SELECT * FROM TP_HomeRooms WHERE HomeID = " + homeDB.GetField("HomeID", i));
                for (int j = 0; j < roomDS.Tables[0].Rows.Count; j++)
                {
                    rooms.Add(
                        new Room(
                            (int)roomDB.GetField("RoomID", j),
                            (int)roomDB.GetField("HomeID", j),
                            (string)roomDB.GetField("RoomType", j),
                            (decimal)roomDB.GetField("RoomLength", j),
                            (decimal)roomDB.GetField("RoomWidth", j)
                        )
                    );
                }
                homes.Add(
                    new Home(
                        (int)homeDB.GetField("HomeID", i),
                        (string)homeDB.GetField("Address", i),
                        (int)homeDB.GetField("SellerID", i),
                        (int)homeDB.GetField("RealtorID", i),
                        (decimal)homeDB.GetField("Price", i),
                        (string)homeDB.GetField("Images", i),
                        (string)homeDB.GetField("Description", i),
                        (DateTime)homeDB.GetField("DateListed", i),
                        rooms,
                        (string)homeDB.GetField("HomeType", i),
                        (int)homeDB.GetField("Age", i),
                        (string)homeDB.GetField("City", i),
                        (string)homeDB.GetField("State", i)
                    )
                );
            }

            return homes;
        }
    }
}
