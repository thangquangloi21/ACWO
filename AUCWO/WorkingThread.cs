using AUCWO.Data;
using ConnectMES;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;




namespace AUCWO
{

    internal class WorkingThread
    {

    
    
    public static string GetFile()
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn file";
            ofd.Filter = "Excel Files|*.xlsx;*.xls";
            //ofd.Filter = "Tất cả file (*.*)|*.*";   // Bạn có thể giới hạn loại file
            ofd.Multiselect = false;               // Chỉ chọn 1 file

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;    // Đường dẫn đầy đủ
                return filePath;         // Gán vào textbox hoặc xử lý tùy bạn
            }
            return "";
        }

    public static string Maping(string SAPCode)
        {
            var sql = "SELECT MANUFACTURER_PART_NUMBER FROM DB_SAP_DWH.dbo.V_MATERIAL_MASTER_DATA where MATERIAL_CODE = @SAPCode";
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@SAPCode", SAPCode)
            };

             var data = DB.ExecuteQuery(sql, parameters);
            Console.WriteLine(data.Rows.Count);
            if (data.Rows.Count == 0) { 
            //MessageBox.Show($"Không tìm thấy mapping cho Item {SAPCode} trong database MES", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null; 
            }

            return data.Rows[0]["MANUFACTURER_PART_NUMBER"].ToString(); 
        }
    



    public static void GetMaping(string FileName)
        {
            try
            {
                AppData.Instance.items.Clear();
                ExcelPackage.License.SetNonCommercialPersonal("Your Name");
                using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(FileName)))
                {
                    // Lấy worksheet đầu tiên và lấy cột đầu tiên 
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    string ITEMCODE = worksheet.Cells[1, 1].Value?.ToString().Trim();
                    string LOT = worksheet.Cells[1, 9].Value?.ToString().Trim();
                    string STATUS = worksheet.Cells[1, 11].Value?.ToString().Trim();
                    //int colCount = worksheet.Dimension.Columns;
                    // kiểm tra xem đúng định dạng file chưa
                    //MessageBox.Show($"{ITEMCODE}");
                    Console.WriteLine($"Item = {ITEMCODE} , LOT = {LOT} , STATUS = {STATUS}");
                    if (ITEMCODE != "Material Number for Order CAUFVD-MATNR" || LOT != "If Column and Batch Number AFPOD-CHARG" || STATUS != "STATUS")
                    {
                        MessageBox.Show("File chưa đúng định dạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    // Duyệt qua từng hàng và cột để lấy dữ liệu
                    //lấy thời gian ngày tháng hiện tại
                    DateTime time = DateTime.Now;
                    for (int row = 2; row <= rowCount; row++) // Bỏ qua hàng tiêu đề
                    {
                        string ItemSAP = worksheet.Cells[row, 1].Text.Trim(); // Cột A
                                                                              // *** KIỂM TRA HÀNG CÓ DỮ LIỆU ***
                        if (string.IsNullOrEmpty(ItemSAP))
                        {
                            // Bỏ qua hàng nếu cột MeTT rỗng
                            continue;
                        }
                        string Plan = worksheet.Cells[row, 2].Text.Trim(); // Cột B
                        string OderType = worksheet.Cells[row, 3].Text.Trim(); // Cột C
                        string StartDate = worksheet.Cells[row, 4].Text.Trim(); // Cột D
                        string EndDate = worksheet.Cells[row, 5].Text.Trim(); // Cột E
                        string OrderQty = worksheet.Cells[row, 6].Text.Trim(); // Cột F
                        string Unit = worksheet.Cells[row, 7].Text.Trim(); // Cột G
                        string Location = worksheet.Cells[row, 8].Text.Trim(); // Cột H
                        string LotSP = worksheet.Cells[row, 9].Text.Trim();
                        string ProductionVer = worksheet.Cells[row, 10].Text.Trim(); // Cột J
                        string Status = worksheet.Cells[row, 11].Text.Trim(); // Cột K


                        // Tiền xử lý đầu vào
                        var itemSapNorm = string.IsNullOrWhiteSpace(ItemSAP) ? null : ItemSAP.Trim();

                        // Gọi Maping một lần
                        var mapped = Maping(itemSapNorm);

                        // Xác định status: nếu không map được => NG, ngược lại => null (OK ngầm định)
                        var status = mapped == null ? "NG" : null;

                        var Map = mapped == null ? itemSapNorm : Maping(itemSapNorm);



                        // Tạo ItemRow (sửa "OderType" -> "OrderType" nếu class có property này)
                        AppData.Instance.items.Add(new ItemRow
                        {
                            ItemSAP = itemSapNorm,
                            PlanSAP = Plan,
                            OderType = OderType,        // hoặc OrderType (đúng property)
                            StartDate = StartDate,
                            EndDate = EndDate,
                            OrderQty = OrderQty,
                            Unit = Unit,
                            Location = Location,
                            LotSP = LotSP,
                            ProductionVer = ProductionVer,
                            Status = status,
                            Mapping = Map
                        });



                        //MessageBox.Show($"Item = {ItemSAP}, Maping = {Maping(ItemSAP)} , LOT = {LotSP}  ");

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"File Không hợp lệ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void GetLocation(string Line)
        {

            // Reset trước khi load
            AppData.Instance.Loc.Clear();

            var sql = "SELECT [ProdLine], [Location] FROM [ACWO].[dbo].[MapingProdLine] Where ProdLine = @ProdLine";

            SqlParameter[] para = new SqlParameter[]
            {
             new SqlParameter("@ProdLine", Line)
            };
            var data = DB.ExecuteQuery(sql, para);


            foreach (System.Data.DataRow row in data.Rows)
            {
                AppData.Instance.Loc.Add(new Model.Location
                {
                    ProdLine = row["ProdLine"].ToString(),
                    LocationNumber = row["Location"].ToString()
                });
            }
        }

        public static void CheckDBMES()
        {
            // Giả sử class của bạn là:
            // public class ItemDto { public string Mapping; public string LotSP; public string Status; }

            var mesDB = new Classa();

            // 1) Lọc item đủ dữ liệu, và không phải NG sẵn
            var pairs = AppData.Instance.items
                .Where(it => it.Status != "NG") // không kiểm lại NG
                .Where(it => !string.IsNullOrWhiteSpace(it.Mapping) && !string.IsNullOrWhiteSpace(it.LotSP)) // đủ dữ liệu key
                .Select(it => (Item: it.Mapping.Trim(), Lot: it.LotSP.Trim()))
                .Distinct() // tránh duplicate (tránh kiểm trùng lặp)
                .ToList();

            // (Tuỳ chọn) In debug
            foreach (var pair in pairs)
            {
                Console.WriteLine($"Checking MES for Item: {pair.Item} , Lot: {pair.Lot}");
            }

            // 2) Gọi MES kiểm tra theo cặp (mapping, lot)
            // Giả định trả về: Dictionary<(string Item, string Lot), bool>
            //   - true  => tồn tại trên MES (coi là NG)
            //   - false => không tồn tại (coi là OK)
            var existence = mesDB.CheckItemsExistBulk(pairs);

            // 3) Gắn kết quả về lại từng item
            foreach (var it in AppData.Instance.items)
            {
                // Nếu đã NG từ trước thì không ghi đè
                if (string.Equals(it.Status, "NG", StringComparison.OrdinalIgnoreCase))
                {
                    it.Status = "Mã sản phẩm không đúng";
                    //MessageBox.Show($"Có lỗi sảy ra với mã {it.ItemSAP} Vui Lòng Kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    continue;
                }
                   

                // Nếu thiếu dữ liệu key thì không thể kiểm, giữ nguyên (hoặc định nghĩa lại là "UNKNOWN")
                if (string.IsNullOrWhiteSpace(it.Mapping) || string.IsNullOrWhiteSpace(it.LotSP))
                {
                    // it.Status ??= null; // giữ nguyên
                    // Hoặc: it.Status = "UNKNOWN";
                    continue;
                }

                var key = (it.Mapping.Trim(), it.LotSP.Trim());

                // Nếu key không có trong dictionary, coi như không tồn tại (OK) hoặc giữ nguyên — tuỳ nghiệp vụ
                if (!existence.TryGetValue(key, out bool exists))
                {
                    // Log cảnh báo để truy vết
                    //Console.WriteLine($"[WARN] Không tìm thấy kết quả cho key: {key.Item}|{key.Lot}");
                    // Quy ước: default là OK
                    exists = false;
                }

                // Quy ước: exists = true → NG; exists = false → OK
                it.Status = exists ? "NG" : "OK";

                // (Tuỳ chọn) Log kết quả
                Console.WriteLine($"Item: {it.Mapping} , Lot: {it.LotSP} , Status: {it.Status}");
            }



            //// Giả sử class của bạn có thuộc tính:
            //// public class ItemDto { public string Mapping; public string LotSP; public string Status; }

            //var mesDB = new Classa();

            //var pairs = AppData.Instance.items
            //    .Where(it => it.Status != "NG")
            //    .Select(it => (Item: it.Mapping, Lot: it.LotSP))
            //    .ToList();

            //foreach (var pair in pairs) { 
            //Console.WriteLine($"Checking MES for Item: {pair.Item} , Lot: {pair.Lot}");
            //}

            //var existence = mesDB.CheckItemsExistBulk(pairs);

            //// Gắn kết quả về lại từng item
            //foreach (var it in AppData.Instance.items)
            //{
            //    bool exists;
            //    if (!existence.TryGetValue((it.Mapping, it.LotSP), out exists))
            //    {
            //        // Nếu vì lý do nào đó cặp (item, lot) không có trong dictionary (ví dụ dữ liệu rỗng),
            //        // bạn có thể default là không tồn tại (OK) hoặc log cảnh báo.
            //        exists = false;
            //        // Console.WriteLine($"[WARN] Không tìm thấy key cho {it.Mapping}|{it.LotSP}");
            //    }

            //    // Quy ước: exists = true → NG; exists = false → OK
            //    it.Status = exists ? "NG" : "OK";

            //    // In ra kết quả (tùy bạn)
            //    Console.WriteLine($"Item: {it.Mapping} , Lot: {it.LotSP} , Status: {it.Status}");


            //}






            //var MesDB = new Classa();

            //foreach (var it in AppData.Instance.items)
            //{
            //    bool exists = MesDB.CheckItemExists(it.Mapping, it.LotSP);
            //    if (!exists)
            //        Console.WriteLine($"Item: {it.Mapping} , Lot: {it.LotSP} , Status: OK");
            //    else
            //        Console.WriteLine($"Item: {it.Mapping} , Lot: {it.LotSP} , Status: NG");
            //}
        }




        public static Boolean CheckQAD_SAP(string loc, string item, string lot)
        {

            //var sql = "SELECT wo_part FROM [Data_qad].[dbo].[wo_mstr] where wo__chr01 = @loc and wo_part = @item and wo_lot_next = @lot";

            var sql = "SELECT wo_part AS Part, wo_lot_next AS Lot " +
                "FROM [Data_qad].[dbo].[wo_mstr] " +
                "WHERE wo__chr01 = @loc AND wo_part = @item AND wo_lot_next = @lot " +
                "UNION ALL " +
                "SELECT MES_PART AS Part, LOT_SERIAL AS Lot " +
                "FROM DB_SAP_DWH.dbo.WORK_ORDER_ALLOCATE " +
                "WHERE LOCATION_ID = @loc AND MES_PART = @item AND LOT_SERIAL = @lot ";

            SqlParameter[] para = new SqlParameter[]
            {
             new SqlParameter("@loc", loc),
             new SqlParameter("@item", item),
             new SqlParameter("@lot", lot)
            };
            var data = DB.ExecuteQuery(sql, para);
            if (data.Rows.Count > 0) {
            //Console.WriteLine($"Item: {item}, Lot: {lot}, Status: NG");
                return true;
            }
            else
            {
                //Console.WriteLine($"Item: {item}, Lot: {lot}, Status: OK");
                return false;
            } 

            //return false;
        }

        public static Boolean CheckSAP(string item, string lot)
        {

            //var sql = "SELECT wo_part FROM [Data_qad].[dbo].[wo_mstr] where wo__chr01 = @loc and wo_part = @item and wo_lot_next = @lot";

            var sql = " SELECT  LOT_SERIAL  ,MES_PART FROM DB_SAP_DWH.dbo.WORK_ORDER_ALLOCATE " +
                "WHERE MES_PART = @item AND LOT_SERIAL = @lot ";

            SqlParameter[] para = new SqlParameter[]
            {
             new SqlParameter("@item", item),
             new SqlParameter("@lot", lot)
            };
            var data = DB.ExecuteQuery(sql, para);
            if (data.Rows.Count > 0)
            {
                //Console.WriteLine($"Item: {item}, Lot: {lot}, Status: NG");
                return true;
            }
            else
            {
                //Console.WriteLine($"Item: {item}, Lot: {lot}, Status: OK");
                return false;
            }

            //return false;
        }




        public static void CheckEvs()
        {
            foreach (var it in AppData.Instance.items)
            {
                var status = CheckSAP(it.Mapping, it.LotSP);

                //it.Status = status ? "NG" : "OK";
                // Nếu đã NG từ trước thì không ghi đè
                if (string.Equals(it.Status, "NG", StringComparison.OrdinalIgnoreCase))
                {
                    //MessageBox.Show($"Có lỗi sảy ra với mã {it.ItemSAP} Vui Lòng Kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    it.Status = "Mã sản phẩm không đúng";
                    continue;
                }

                // Quy ước: exists = true → NG; exists = false → OK
                it.Status = status ? "NG" : "OK";

                // (Tuỳ chọn) Log kết quả
                Console.WriteLine($"Item: {it.Mapping} , Lot: {it.LotSP} , Status: {it.Status}");

            }


        }


        public static async Task<string> GetTimeUpdate(string system)
        {
            var sql = "SELECT TOP (1) [ID] ,[SYSTEM] ,[TIME] FROM [ACWO].[dbo].[DataStatus]  where system = @system order by id desc";

            SqlParameter[] para = new SqlParameter[]
            {
             new SqlParameter("@system", system),
     
            };
            var data = DB.ExecuteQuery(sql, para);
            return data.Rows[0]["TIME"].ToString();

        }


      



        public static  void CheckMD()
        {
            foreach (var it in AppData.Instance.items)
            {
                var status = CheckQAD_SAP("03010", it.Mapping, it.LotSP);

                //it.Status = status ? "NG" : "OK";
                // Nếu đã NG từ trước thì không ghi đè
                if (string.Equals(it.Status, "NG", StringComparison.OrdinalIgnoreCase))
                {
                    //MessageBox.Show($"Có lỗi sảy ra với mã {it.ItemSAP} Vui Lòng Kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    it.Status = "Mã sản phẩm không đúng";
                    continue;

                }




                // Quy ước: exists = true → NG; exists = false → OK
                it.Status = status ? "NG" : "OK";

                // (Tuỳ chọn) Log kết quả
                Console.WriteLine($"Item: {it.Mapping} , Lot: {it.LotSP} , Status: {it.Status}");

            }
        }

        public static void CheckItem(string ProdLine, string linkfile)
        {

            //maping tên cột trong file excel với tên cột trong database để lấy dữ liệu check
            GetMaping(linkfile);
            //foreach (var it in AppData.Instance.items)
            //{ 
            //Console.WriteLine($"Item: {it.ItemSAP} , Maping: {it.Mapping} , Lot: {it.LotSP} , Status: {it.Status}");
            //}

            //Check theo bộ phận
            if (ProdLine == "IK,GW,RFC,RFS")
            {
                CheckDBMES();
            }


            if (ProdLine == "MD")
            {
                CheckMD();
                //MessageBox.Show("Chưa Phát triển");
            }

            if (ProdLine == "EVS")
            {
                //AppData.Instance.items.Clear();
                CheckEvs();
            }
        }
    }



}
