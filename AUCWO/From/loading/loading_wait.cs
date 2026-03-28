using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AUCWO
{
    public partial class loading_wait : UserControl
    {
        public loading_wait()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;   // luôn phủ toàn form
            this.Visible = false;
            this.BringToFront();

            // màu nền mờ mờ
            //this.BackColor = Color.FromArgb(100, Color.Gray);
        }

        public void ShowLoading()
        {
            //label1.Text = message;
            CenterControls();   // căn giữa lại gif + label
            this.Visible = true;
            this.BringToFront();
        }

        public void HideLoading()
        {
            this.Visible = false;
        }

        // Hàm tự căn giữa PictureBox + Label
        private void CenterControls()
        {
            if (pictureBox1 != null)
            {
                pictureBox1.Location = new Point(
                    (this.Width - pictureBox1.Width) / 2,
                    (this.Height - pictureBox1.Height) / 2 - 20
                );
            }


        }

        // Cập nhật khi form resize
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterControls();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
