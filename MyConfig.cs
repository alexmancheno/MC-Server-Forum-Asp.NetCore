using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_Forum 
{
    public class MyConfig
    {
        public string ConnectionString { get; set; }
        public MyConfig()
        {
            // Set default value.
            ConnectionString = "not a valid connection string!";
        }
    }
}