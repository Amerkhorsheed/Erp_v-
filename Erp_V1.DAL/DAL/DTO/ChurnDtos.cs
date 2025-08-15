using Erp_V1.DAL.DTO;
using Microsoft.ML.Data;

namespace Erp_V1.ML.DTO
{
    // No changes to CustomerFeatureData, it is still correct.
    public class CustomerFeatureData
    {
        [LoadColumn(0)] public float Recency { get; set; }
        [LoadColumn(1)] public float Frequency { get; set; }
        [LoadColumn(2)] public float MonetaryValue { get; set; }
        [LoadColumn(3)] public float Tenure { get; set; }
        [LoadColumn(4)] public float AvgTransactionValue { get; set; }
        [LoadColumn(5)] public float PurchaseFrequencyDays { get; set; }
        [LoadColumn(6)] public float RecencyScore { get; set; }
        [LoadColumn(7)] public float FrequencyScore { get; set; }
        [LoadColumn(8)] public float MonetaryScore { get; set; }
        [LoadColumn(9), ColumnName("Label")] public bool Churn { get; set; }
    }

    /// <summary>
    /// Represents the rich output of a churn prediction, including probability.
    /// </summary>
    public class CustomerPredictionResult
    {
        public CustomerDetailDTO Customer { get; set; }
        public bool PredictedChurn { get; set; }
        public float Probability { get; set; }
    }

    // Internal class used by the prediction engine.
    public class InternalChurnPrediction
    {
        [ColumnName("PredictedLabel")] public bool PredictedChurn { get; set; }
        public float Probability { get; set; }
        public float Score { get; set; }
    }
}