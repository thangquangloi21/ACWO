using System;
using System.Drawing;
using System.Windows.Forms;

namespace TWSL
{
    public partial class LoadingControl : UserControl
    {
        private Panel panelCenter;
        private Label label1;
        private ProgressBar progressBar1;

        public LoadingControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;                          // luôn phủ form cha
            this.Visible = false;                                // mặc định ẩn
            this.BackColor = Color.FromArgb(120, Color.Black);   // nền mờ
            this.BringToFront();

            // Panel trung tâm
            panelCenter = new Panel
            {
                BackColor = Color.White,
                Size = new Size(250, 100),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Label
            label1 = new Label
            {
                Dock = DockStyle.Top,
                Height = 40,
                Text = "Đang xử lý, vui lòng chờ...",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ProgressBar
            progressBar1 = new ProgressBar
            {
                Dock = DockStyle.Bottom,
                Height = 20,
                Style = ProgressBarStyle.Marquee
            };

            panelCenter.Controls.Add(label1);
            panelCenter.Controls.Add(progressBar1);
            this.Controls.Add(panelCenter);

            CenterPanel(); // căn giữa ngay lần đầu
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterPanel(); // luôn căn giữa khi resize form
        }

        private void CenterPanel()
        {
            if (panelCenter != null)
            {
                panelCenter.Left = (this.Width - panelCenter.Width) / 2;
                panelCenter.Top = (this.Height - panelCenter.Height) / 2;
            }
        }

        public void ShowLoading(string message = "Đang xử lý, vui lòng chờ...")
        {
            label1.Text = message;
            progressBar1.Style = ProgressBarStyle.Marquee;
            this.Visible = true;
            this.BringToFront();
        }

        public void HideLoading()
        {
            this.Visible = false;
        }

        private void LoadingControl_Load(object sender, EventArgs e)
        {

        }
    }
}
