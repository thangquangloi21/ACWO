using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUCWO.Data
{
        public class ItemRow
        {
            public string ItemSAP { get; set; }
            public string PlanSAP { get; set; }
            public string OderType { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string OrderQty { get; set; }
            public string Unit { get; set; }
            public string Location { get; set; }
            public string LotSP { get; set; }
            public string ProductionVer { get; set; }
            public string Status { get; set; }
            public string Mapping { get; set; }


        // Nếu cần thêm cột khác, bạn bổ sung ở đây
    }
}
