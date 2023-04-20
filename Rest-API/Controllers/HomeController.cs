using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace Rest_API.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {



        //Gets all homes and adds them to a list of home objects

        public List<Home> Get() {

            DBConnect objDB = new DBConnect();
            DataSet ds = objDB.GetDataSet("SELECT * FROM TP_Homes");
            List<Home> homes = new List<Home>();
            Home home;

            foreach (DataRow record in ds.Tables[0].Rows) {
                home = new Home();
                home.HomeID = int.Parse(record["HomeID"].ToString());
                //Add more code similar to above for grabbing home
                //from a database and saving as home object
            }
            return homes;
        }
    }
}
