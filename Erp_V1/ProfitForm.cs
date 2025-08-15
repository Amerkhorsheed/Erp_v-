using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class ProfitForm : Form
    {
        public ProfitForm()
        {
            InitializeComponent();
        }

        private void ProfitForm_Load(object sender, EventArgs e)
        {
            CalculateAndDisplayProfit();
        }

        private async void CalculateAndDisplayProfit()
        {
            decimal profitValue = await Task.Run(() =>
            {
                // Replace this with your actual ProfitBLL and GetFinalProfit() call
                // Assuming ProfitBLL and its dependencies are accessible in this project.
                Erp_V1.BLL.ProfitBLL profitBLL = new Erp_V1.BLL.ProfitBLL();
                return profitBLL.GetFinalProfit();
            });

            // Format profit for display (e.g., with currency symbol and 2 decimal places)
            ProfitValueLabel.Text = profitValue.ToString("C2");
        }
    }
}