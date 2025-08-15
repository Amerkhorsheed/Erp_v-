using System;
using System.IO;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Professional, bindable configuration for the churn prediction model.
    /// These properties can be populated from appsettings.json or other config sources.
    /// </summary>
    public class ChurnPredictionOptions
    {
        public const string SectionName = "ChurnPrediction";

        /// <summary>
        /// Days of inactivity to consider a customer churned.
        /// </summary>
        public int ChurnThresholdInDays { get; set; } = 90;

        /// <summary>
        /// Path to store the trained model, relative to the application's base directory.
        /// </summary>
        public string ModelFileName { get; set; } = "customer_churn_model.zip";

        /// <summary>
        /// Number of folds for cross-validation. 5 is a standard, robust choice.
        /// </summary>
        public int CrossValidationFolds { get; set; } = 5;
    }
}