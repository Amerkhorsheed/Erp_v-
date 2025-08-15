using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Erp_V1;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Erp_V1.BLL.Tests; // Contains FakeProductPredictorService

namespace Erp_V1.Tests
{
    [TestClass]
    public class FrmProductPredictorModernTests
    {
        /// <summary>
        /// Helper method to find a control of type T anywhere in the control hierarchy.
        /// </summary>
        private T FindControl<T>(Control parent) where T : Control
        {
            if (parent is T found)
                return found;
            foreach (Control child in parent.Controls)
            {
                found = FindControl<T>(child);
                if (found != null)
                    return found;
            }
            return null;
        }

        [TestMethod]
        public void GeneratePredictions_WithValidData_ShouldPopulateUI()
        {
            // Arrange: Instantiate the form with the fake predictor.
            var fakePredictor = new FakeProductPredictorService();
            using (var form = new FrmProductPredictorModern(fakePredictor))
            {
                // Force the form to create and layout its controls.
                form.Show();
                Application.DoEvents();

                // Retrieve the parameters panel from the dynamic UI.
                var tableLayout = form.Controls.OfType<TableLayoutPanel>().FirstOrDefault();
                Assert.IsNotNull(tableLayout, "TableLayoutPanel was not found on the form.");

                var parametersPanel = tableLayout.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                Assert.IsNotNull(parametersPanel, "Parameters FlowLayoutPanel was not found.");

                // Retrieve all numeric controls from the parameters panel.
                var numericControls = parametersPanel.Controls.OfType<NumericUpDown>().ToList();
                Assert.IsTrue(numericControls.Count >= 4, "Not enough NumericUpDown controls found in parameters panel.");

                // Set valid values on each control (assumed order: horizon, confidence, seasonality, trendWindow)
                numericControls[0].Value = 30; // numHorizon
                numericControls[1].Value = 90; // numConfidence
                numericControls[2].Value = 7;  // numSeasonality
                numericControls[3].Value = 14; // numTrendWindow

                // Act: Find and click the Predict button.
                var btnPredict = parametersPanel.Controls.OfType<Button>().FirstOrDefault();
                Assert.IsNotNull(btnPredict, "Predict button was not found on the parameters panel.");
                btnPredict.PerformClick();

                // Process pending UI events.
                Application.DoEvents();

                // Assert: Locate the SplitContainer that hosts the DataGridView.
                var splitContainer = tableLayout.Controls.OfType<SplitContainer>().FirstOrDefault();
                Assert.IsNotNull(splitContainer, "SplitContainer was not found on the form.");

                var dgv = splitContainer.Panel1.Controls.OfType<DataGridView>().FirstOrDefault();
                Assert.IsNotNull(dgv, "DataGridView was not found on the form.");

                // Check that the DataSource is set.
                Assert.IsNotNull(dgv.DataSource, "DataGridView DataSource is null.");
                var predictions = dgv.DataSource as List<ProductPredictionDTO>;
                Assert.IsNotNull(predictions, "DataGridView DataSource is not of type List<ProductPredictionDTO>.");
                Assert.IsTrue(predictions.Any(), "No predictions were generated, but dummy data was provided.");

                form.Close();
            }
        }
    }
}
