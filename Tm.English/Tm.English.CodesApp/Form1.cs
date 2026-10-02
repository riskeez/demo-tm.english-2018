using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using Tm.English.CodesApp.Reports;
using Tm.Mobile.Core.Security;

namespace Tm.English.CodesApp
{
    public partial class Form1 : Form
    {
        readonly ICipherService cipherService;

        public Form1(ICipherService cipherService)
        {
            InitializeComponent();

            this.cipherService = cipherService;

            tbPrivateKey.Text = GetNewKey();
        }

        private void btnRefreshPrivate_Click(object sender, EventArgs e)
        {
            tbPrivateKey.Text = GetNewKey();
        }

        private string GetNewKey()
        {
            return Guid.NewGuid().ToString("N");
        }

        private bool CheckSecretKey()
        {
            if (tbPrivateKey.Text.Length == 0)
            {
                MessageBox.Show("'Secret Key' is empty");
                return false;
            }

            return true;
        }

        private void ShowReport(XtraReport rep)
        {
            var tool = new ReportPrintTool(rep);
            tool.ShowPreview();
        }

        private void btnShowQRCode_Click(object sender, EventArgs e)
        {
            if (!CheckSecretKey())
                return;

            var rep = new QRCodeReport();
            rep.DataSource = new ReportQRCodes() { new ReportQRCode() { EncryptedData = tbPrivateKey.Text, CodeNo = "Private Key"  } };

            ShowReport(rep);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (!CheckSecretKey())
                return;

            var privateKey = tbPrivateKey.Text;

            var codes = new ReportQRCodes();
            for (int i = 1; i <= udQuantity.Value; i++)
            {
                var qrCode = new QRCode()
                {
                    Key = GetNewKey(),
                    No = DateTime.Now.ToString("yyMMddhhmmss") + i
                };

                var serialized = JsonConvert.SerializeObject(qrCode);

                var item = new ReportQRCode();
                item.EncryptedData = cipherService.Encrypt(serialized, privateKey);
                item.CodeNo = qrCode.No;

                codes.Add(item);
            }

            var rep = new QRCodeReport() { DataSource = codes };

            ShowReport(rep);
        }
    }
}
