using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmScientificCalculator : XtraForm
    {
        private TextBox txtDisplay;
        
        public FrmScientificCalculator()
        {
            InitializeComponent();
            InitializeCalculatorUI();
        }

        private void InitializeCalculatorUI()
        {
            this.Size = new Size(350, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Add calculator UI implementation here
            // (Create number buttons, operators, and display logic)
        }
    }
} 