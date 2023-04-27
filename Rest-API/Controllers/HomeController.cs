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
            DBConnect homeDB = new DBConnect();
            DataSet homeDS = homeDB.GetDataSet("SELECT * FROM TP_Homes");
            Collection<Home> homes = new Collection<Home>();
            DBConnect roomDB = new DBConnect();
            for (int i = 0; i < homeDS.Tables[0].Rows.Count; i++)
            {
                Collection<Room> rooms = new Collection<Room>();
                DataSet roomDS = roomDB.GetDataSet("SELECT * FROM TP_HomeRooms WHERE HomeID = " + homeDB.GetField("HomeID", i));
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
                        (int)homeDB.GetField("RealtorID", i),
                        (decimal)homeDB.GetField("Price", i),
                        ((string)homeDB.GetField("Images", 0)).Split(","),
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

        //This takes in home objects as well as adds them
        [HttpPost()]
        [HttpPost("AddHome")]
        public bool AddHome([FromBody]Home home)
        {
            if (home != null)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_AddHome";

                objCommand.Parameters.AddWithValue("@Address", home.Address);
                objCommand.Parameters.AddWithValue("@RealtorID", home.RealtorID);
                objCommand.Parameters.AddWithValue("@Price", home.Price);
                objCommand.Parameters.AddWithValue("@Images", home.Images);
                objCommand.Parameters.AddWithValue("@Description", home.Description);
                objCommand.Parameters.AddWithValue("@DateListed", home.DateListed);
                objCommand.Parameters.AddWithValue("@Bedrooms", home.GetBedrooms());
                objCommand.Parameters.AddWithValue("@Bathrooms", home.GetBathrooms());
                objCommand.Parameters.AddWithValue("@Size", home.GetSquareFootage());
                objCommand.Parameters.AddWithValue("@HomeType", home.HomeType);
                objCommand.Parameters.AddWithValue("@Age", home.HomeAge);
                objCommand.Parameters.AddWithValue("@City", home.City);
                objCommand.Parameters.AddWithValue("@State", home.State);

                if (objDB.DoUpdateUsingCmdObj(objCommand) > 0)
                    return true;
                else
                    return false;
            }else
                return false;
        }

        //Deletes Home Records for specified ID
        [HttpDelete]
        [HttpDelete("DeleteHomeById/{id}")]
        public bool DeleteHomeById(int id)
        {
            if (id > 0)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_RemoveHome";

                objCommand.Parameters.AddWithValue("@HomeID", id);

                if (objDB.DoUpdateUsingCmdObj(objCommand) > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        [HttpPut()]
        [HttpPut("UpdateHome")]
        public bool UpdateHome([FromBody] Home home)
        {
            if (home != null)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TP_UpdateHome";

                objCommand.Parameters.AddWithValue("@HomeID", home.HomeID);
                objCommand.Parameters.AddWithValue("@Address", home.Address);
                objCommand.Parameters.AddWithValue("@RealtorID", home.RealtorID);
                objCommand.Parameters.AddWithValue("@Price", home.Price);
                objCommand.Parameters.AddWithValue("@Images", string.Join(",", home.Images));
                objCommand.Parameters.AddWithValue("@Description", home.Description);
                objCommand.Parameters.AddWithValue("@DateListed", home.DateListed);
                objCommand.Parameters.AddWithValue("@HomeType", home.HomeType);
                objCommand.Parameters.AddWithValue("@Age", home.HomeAge);
                objCommand.Parameters.AddWithValue("@City", home.City);
                objCommand.Parameters.AddWithValue("@State", home.State);

                if (objDB.DoUpdateUsingCmdObj(objCommand) > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        //Gets home by a given ID
        [HttpGet("GetHomeByID/{id}")]
        public Home GetHomeByName(string id)
        {
            DBConnect homeDB = new DBConnect();
            homeDB.GetDataSet("SELECT * FROM TP_Homes WHERE HomeID = " + id);
            DBConnect roomDB = new DBConnect();
            Collection<Room> rooms = new Collection<Room>();
            DataSet roomDS = roomDB.GetDataSet("SELECT * FROM TP_HomeRooms WHERE HomeID = " + id);

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

            return new Home(
                (int)homeDB.GetField("HomeID", 0),
                (string)homeDB.GetField("Address", 0),
                (int)homeDB.GetField("RealtorID", 0),
                (decimal)homeDB.GetField("Price", 0),
                ((string)homeDB.GetField("Images", 0)).Split(","),
                (string)homeDB.GetField("Description", 0),
                (DateTime)homeDB.GetField("DateListed", 0),
                rooms,
                (string)homeDB.GetField("HomeType", 0),
                (int)homeDB.GetField("Age", 0),
                (string)homeDB.GetField("City", 0),
                (string)homeDB.GetField("State", 0)
            );
        }
    }
}
