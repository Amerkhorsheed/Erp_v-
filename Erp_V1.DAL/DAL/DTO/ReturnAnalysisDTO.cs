using Microsoft.ML.Data;
using System;
using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    public class ReturnAnalysisDTO
    {
        public int ReturnID { get; set; }
        public string OriginalReason { get; set; }
        public string ProcessedReason { get; set; }
        public uint ClusterID { get; set; }
        public string ClusterDescription { get; set; }
        public float Confidence { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
    }

    public class ReturnAnalysisConfigDTO
    {
        public int ClusterCount { get; set; }
        public int MinWordLength { get; set; }
        public int MaxFeatures { get; set; }
        public int MaxIterations { get; set; }
        public double SilhouetteThreshold { get; set; }
        public HashSet<string> StopWords { get; set; }
    }

    public class ReturnAnalysisResultsDTO
    {
        public List<ReturnAnalysisDTO> Results { get; set; }
        public Dictionary<int, double> ClusterMetrics { get; set; }
        public Dictionary<uint, List<string>> ClusterKeywords { get; set; }
        public double QualityScore { get; set; }
    }

    public class ReturnReasonData
    {
        public int ReturnID { get; set; }
        public string Reason { get; set; }
        public string OriginalReason { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
    }

    public class ReturnClusterPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId { get; set; }

        [ColumnName("Score")]
        public float[] Score { get; set; }
    }

    public class ReturnAnalysisResult
    {
        public int ReturnID { get; set; }
        public string OriginalReason { get; set; }
        public string ProcessedReason { get; set; }
        public uint ClusterID { get; set; }
        public string ClusterDescription { get; set; }
        public float Confidence { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
    }
}
