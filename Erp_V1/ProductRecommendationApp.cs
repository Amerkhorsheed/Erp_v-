using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class ProductRecommendationApp : Form
    {
        private ProductRecommendationModel model;

        public ProductRecommendationApp()
        {
            InitializeComponent();
            model = new ProductRecommendationModel();
        }

        private void buttonRecommend_Click(object sender, EventArgs e)
        {
            uint userId = uint.Parse(textBoxUserId.Text);
            uint productId = uint.Parse(textBoxProductId.Text);
            float score = model.Predict(userId, productId);
            labelScore.Text = $"Recommendation Score: {score}";
        }
    }
}
