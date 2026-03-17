using AUCWO.Data;
using ConnectMES;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AUCWO
{
    public partial class Main : Form
    {
        private readonly Timer _timer;
        private loading_wait loading;
        public Main()
        {
            InitializeComponent();
            _timer = new Timer
            {
                Interval = 10_000 // 10,000 ms = 10S
            };
            _timer.Tick += Timer_Tick;


            ExcelPackage.License.SetNonCommercialPersonal("Your Name");
            loading = new loading_wait();
            loading.Dock = DockStyle.None;
            loading.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            loading.Location = new Point(0, 0);
            loading.Size = this.ClientSize;
            this.Controls.Add(loading);
            this.Controls.SetChildIndex(loading, 0);
            this.Resize += (s, e) =>
            {
                if (loading != null && !loading.IsDisposed)
                    loading.Size = this.ClientSize;
            };
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            RunUpdateOnce();
        }

        private async void RunUpdateOnce()
        {
            ////Kiểm tra trạng thái nếu kiểm tra lỗi kết nối thì thoát
            //    SAPstt.Text = WorkingThread.GetTimeUpdate("SAP");
            //    QADstt.Text = WorkingThread.GetTimeUpdate("QAD");



            SAPstt.Text = "Đang cập nhật...";
            QADstt.Text = "Đang cập nhật...";

            // await trên Task<string> là hợp lệ
            var sap = await WorkingThread.GetTimeUpdate("SAP");
            var qad = await WorkingThread.GetTimeUpdate("QAD");

            SAPstt.Text = sap;
            QADstt.Text = qad;


        }



        private void Btn_Upload_Click(object sender, EventArgs e)
        {
                LinkTbx.Text = WorkingThread.GetFile(); 
        }

        //private void DisPlayData()
        //{
        //    // 2) Bind lên DataGridView
        //    var bindingList = new BindingList<ItemRow>(AppData.Instance.items);
        //    var source = new BindingSource(bindingList, null);
        //    Viewdata.DataSource = source;

        //}

        private void DisPlayData()
        {
            // 1. Tắt tự động tạo cột để kiểm soát hoàn toàn
            Viewdata.AutoGenerateColumns = false;

            // 2. Xóa cột cũ nếu có (an toàn khi refresh dữ liệu nhiều lần)
            Viewdata.Columns.Clear();

            // 3. Cấu hình chung cho header (để hỗ trợ multiline và wrap text)
            Viewdata.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;     // Cho phép wrap text trong header
            Viewdata.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa header
            Viewdata.EnableHeadersVisualStyles = false;  // Tắt theme mặc định để tùy chỉnh dễ hơn
            Viewdata.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204); // Xanh dương (nếu bạn vẫn muốn)
            Viewdata.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Viewdata.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            Viewdata.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            Viewdata.ColumnHeadersHeight = 60;  // Tăng chiều cao header để chữ đa dòng hiển thị đầy đủ

            // 4. Tạo và map các cột
            var columns = new[]
            {
    new { DataPropertyName = "ItemSAP",       HeaderText = "Material Number for Order\nCAUFVD-MATNR",        Width = 140},
    new { DataPropertyName = "PlanSAP",       HeaderText = "Plant\nCAUFVD-WERKS",                             Width = 120 },
    new { DataPropertyName = "OderType",      HeaderText = "Order Type for Production Orders\nAUFPAR-PP_AUFART", Width = 130 },
    new { DataPropertyName = "StartDate",     HeaderText = "Basic start date\nCAUFVD-GSTRP",                  Width = 120 },
    new { DataPropertyName = "EndDate",       HeaderText = "Basic finish date\nCAUFVD-GLTRP",                 Width = 120 },
    new { DataPropertyName = "OrderQty",      HeaderText = "Total Order Quantity\nCAUFVD-GAMNG",              Width = 110 },
    new { DataPropertyName = "Unit",          HeaderText = "Common unit of measure\nCAUFVD-GMEIN",            Width = 90  },
    new { DataPropertyName = "Location",      HeaderText = "Storage location\nAFPOD-LGORT",                   Width = 110 },
    new { DataPropertyName = "LotSP",         HeaderText = "Batch Number\nAFPOD-CHARG",                       Width = 120 },
    new { DataPropertyName = "ProductionVer", HeaderText = "Production Version\nRC62F-PROD_VERS",             Width = 130 },
    new { DataPropertyName = "Status",        HeaderText = "Trạng thái",                                     Width = 100 }
};

            foreach (var colInfo in columns)
            {
                var column = new DataGridViewTextBoxColumn
                {
                    Name = "col" + colInfo.DataPropertyName,
                    HeaderText = colInfo.HeaderText,
                    DataPropertyName = colInfo.DataPropertyName,
                    Width = colInfo.Width,
                    ReadOnly = true,

                    // Căn giữa ngang & dọc cho dữ liệu trong ô
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        WrapMode = DataGridViewTriState.False  // Không wrap nội dung ô (trừ khi cần)
                    }
                };

                Viewdata.Columns.Add(column);
            }

            // 5. Tăng chiều cao dòng để nhìn thoáng hơn (tùy chọn)
            Viewdata.RowTemplate.Height = 28;

            // 6. Bind dữ liệu (phần này bạn đã có)
            var bindingList = new BindingList<ItemRow>(AppData.Instance.items);
            var source = new BindingSource(bindingList, null);
            Viewdata.DataSource = source;


            // === THÊM PHẦN MÀU HEADER XANH ===
            Viewdata.EnableHeadersVisualStyles = false;
            Viewdata.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204); // Xanh dương
            Viewdata.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Viewdata.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 6, FontStyle.Bold);
            Viewdata.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            Viewdata.ColumnHeadersHeight = 80; // Tăng chiều cao header cho đẹp (tùy chọn)
        }


        private void Viewdata_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra nếu dòng hợp lệ và có cột Status
            if (e.RowIndex >= 0 && Viewdata.Columns["colStatus"] != null)
            {
                // Lấy giá trị Status từ dòng hiện tại
                var statusCell = Viewdata.Rows[e.RowIndex].Cells["colStatus"];
                if (statusCell.Value != null && statusCell.Value.ToString().Trim().Equals("NG", StringComparison.OrdinalIgnoreCase))
                {
                    // Tô nền đỏ cho toàn bộ dòng
                    Viewdata.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;  // Hoặc Color.Red nếu muốn đỏ đậm
                    Viewdata.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.DarkRed;  // Màu khi chọn dòng (tùy chọn)
                }
                else if (statusCell.Value != null && statusCell.Value.ToString().Trim().Equals("Mã sản phẩm không đúng", StringComparison.OrdinalIgnoreCase))
                {
                    // Tô nền đỏ cho toàn bộ dòng
                    Viewdata.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;  // Hoặc Color.Red nếu muốn đỏ đậm
                    Viewdata.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.DarkRed;  // Màu khi chọn dòng (tùy chọn)
                }
                else
                {
                    // Reset về màu mặc định nếu không phải NG (ví dụ trắng hoặc mặc định)
                    Viewdata.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    Viewdata.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                }
            }
        }





        private void UpdateSummaryFromList()
        {
            var items = AppData.Instance.items;

            int total = items.Count;
            int totalNG = items.Count(x => string.Equals(x.Status, "NG", StringComparison.OrdinalIgnoreCase));
            int totalOK = total - totalNG;


            LblOK.Text = $"{totalOK}/{total}";
            LblNG.Text = $"{totalNG}/{total}";
        }


        private void Check_WO(object sender, EventArgs e)
        {
            try
            {
                var LinkFile = LinkTbx.Text;
                var Line = Prod_LineCBB.Text;
                Console.WriteLine(Line);
                if (LinkFile == "")
                {
                    MessageBox.Show("Chọn File để thực hiện");
                    return;
                }

                if (Line == "")
                {
                    MessageBox.Show("Chọn Line để thực hiện");
                    return;
                }
                //lặp từng dòng trong file excel, lấy dữ liệu và kiểm tra với database
                loading.ShowLoading();
                Task.Run(() =>
                {

                    //xử lý
                    WorkingThread.CheckItem(Line, LinkFile);

                    this.Invoke(new Action(() =>
                    {

                        loading.HideLoading();
                        UpdateSummaryFromList();
                        DisPlayData();
                        //MessageBox.Show("Hoàn thành!");
                        //Update_data();
                    }));
                });
            }
            catch (Exception ex) {
                loading.HideLoading();
                MessageBox.Show("Hệ thống đang lỗi vui lòng thử lại sau !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void Btn_Export(object sender, EventArgs e)
        {

            if (Viewdata.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveDialog.Title = "Lưu file Excel";
                    saveDialog.FileName = "CheckWO_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Tạo file Excel mới
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(saveDialog.FileName)))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Dữ liệu sản xuất");

                            // === 1. Xuất header ===
                            for (int col = 1; col <= Viewdata.Columns.Count; col++)
                            {
                                string headerText = Viewdata.Columns[col - 1].HeaderText;
                                worksheet.Cells[1, col].Value = headerText;
                                worksheet.Cells[1, col].Style.Font.Bold = true;
                                worksheet.Cells[1, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, col].Style.Fill.BackgroundColor.SetColor(Color.LightBlue); // Màu xanh nhạt cho header
                                worksheet.Cells[1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[1, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            }

                            // === 2. Xuất dữ liệu + tô màu + căn giữa ===
                            for (int row = 0; row < Viewdata.Rows.Count; row++)
                            {
                                bool isNG = false;
                                bool isNG1 = false;


                                // Kiểm tra Status ở cột "colStatus" (giả sử là cột cuối cùng hoặc bạn biết index)
                                // Nếu không chắc index, dùng tên cột để tìm
                                int statusColumnIndex = -1;
                                for (int col = 0; col < Viewdata.Columns.Count; col++)
                                {
                                    if (Viewdata.Columns[col].Name == "colStatus")
                                    {
                                        statusColumnIndex = col;
                                        break;
                                    }
                                }

                                if (statusColumnIndex >= 0)
                                {
                                    var statusValue = Viewdata.Rows[row].Cells[statusColumnIndex].Value?.ToString()?.Trim();
                                    if (statusValue?.Equals("NG", StringComparison.OrdinalIgnoreCase) == true)
                                    {
                                        isNG = true;
                                    }
                                    //var statusValue = Viewdata.Rows[row].Cells[statusColumnIndex].Value?.ToString()?.Trim();
                                    if (statusValue?.Equals("Mã sản phẩm không đúng", StringComparison.OrdinalIgnoreCase) == true)
                                    {
                                        isNG1 = true;
                                    }
                                }

                                for (int col = 0; col < Viewdata.Columns.Count; col++)
                                {
                                    var cellValue = Viewdata.Rows[row].Cells[col].Value;
                                    worksheet.Cells[row + 2, col + 1].Value = cellValue;

                                    // Căn giữa ngang và dọc cho mọi ô dữ liệu
                                    worksheet.Cells[row + 2, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet.Cells[row + 2, col + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                }

                                // Nếu là dòng NG → tô đỏ toàn bộ dòng (nền đỏ nhạt, chữ đen cho dễ đọc)
                                if (isNG)
                                {
                                    using (var range = worksheet.Cells[row + 2, 1, row + 2, Viewdata.Columns.Count])
                                    {
                                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 182, 193)); // LightCoral / đỏ nhạt
                                                                                                                  // Nếu muốn chữ trắng: range.Style.Font.Color.SetColor(Color.White);
                                    }
                                }
                                if (isNG1)
                                {
                                    using (var range = worksheet.Cells[row + 2, 1, row + 2, Viewdata.Columns.Count])
                                    {
                                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 102)); // LightCoral / đỏ nhạt
                                                                                                                  // Nếu muốn chữ trắng: range.Style.Font.Color.SetColor(Color.White);
                                    }
                                }
                            }



                            // === 3. Tự động điều chỉnh độ rộng cột ===
                            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                            // === 4. Lưu file ===
                            package.Save();

                            MessageBox.Show("Xuất file Excel thành công!\nFile đã lưu tại: " + saveDialog.FileName,
                                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void Prod_LineCBB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Gắn event (chạy 1 lần, ví dụ trong Form_Load)
            Viewdata.CellFormatting += Viewdata_CellFormatting;
            // Chạy ngay lần đầu (nếu muốn), sau đó mỗi phút
            RunUpdateOnce();
            _timer.Start();

        }

        private void LoadStatus(object sender, EventArgs e)
        {
            Console.WriteLine(WorkingThread.GetTimeUpdate("MES"));
            
        }
    }
}
