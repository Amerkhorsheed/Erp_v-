//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//// DTO/CustomerFeatureData.cs
//using Microsoft.ML.Data;

//namespace Erp_V1.ML.DTO
//{
//    // فئة لتمثيل بيانات الميزات المُدخلة للنموذج
//    public class CustomerFeatureData
//    {
//        [LoadColumn(0)]
//        public float CustomerId { get; set; }

//        [LoadColumn(1)]
//        public float Recency { get; set; } // عدد الأيام منذ آخر شراء

//        [LoadColumn(2)]
//        public float Frequency { get; set; } // إجمالي عدد المشتريات

//        [LoadColumn(3)]
//        public float MonetaryValue { get; set; } // إجمالي قيمة المشتريات

//        [LoadColumn(4)]
//        public float Tenure { get; set; } // مدة العلاقة بالأيام

//        [LoadColumn(5)]
//        [ColumnName("Label")] // يجب تسمية متغير الهدف بـ "Label"
//        public bool Churn { get; set; }
//    }

//    // فئة لتمثيل نتيجة التنبؤ
//    public class ChurnPrediction
//    {
//        [ColumnName("PredictedLabel")]
//        public bool PredictedChurn { get; set; }

//        public float Probability { get; set; }

//        public float Score { get; set; }
//    }
//}