using AUCWO.Data;
using AUCWO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUCWO
{
    public class AppData
    {
        private static AppData _instance;
        public static AppData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AppData();
                return _instance;
            }
        }
        // prod
        public string DB = "Server=10.239.1.54;Database=ACWO;User Id=sa;Password=123456;";

        public List<ItemRow> items = new List<ItemRow>();

        public List<Location> Loc = new List<Location>();

        public string StatusExpQAD { get; set; }
        public string StatusExpSAP { get; set; }


    }
}
