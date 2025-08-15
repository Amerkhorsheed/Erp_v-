using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

public class ProductRating
{
    [LoadColumn(0), KeyType(count: 1000)]
    public uint userId;

    [LoadColumn(1), KeyType(count : 1000)]
    public uint productId;

    [LoadColumn(2)]
    public float Label;
}

public class ProductRecommendation
{
    public float Score { get; set; }
}

public class ProductRecommendationModel
{
    private readonly MLContext mlContext;
    private readonly ITransformer model;

    public ProductRecommendationModel()
    {
        mlContext = new MLContext();
        var data = mlContext.Data.LoadFromTextFile<ProductRating>("product_ratings.csv", separatorChar: ',', hasHeader: true);
        var dataSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);

        var options = new MatrixFactorizationTrainer.Options
        {
            MatrixColumnIndexColumnName = nameof(ProductRating.userId),
            MatrixRowIndexColumnName = nameof(ProductRating.productId),
            LabelColumnName = nameof(ProductRating.Label),
            NumberOfIterations = 20,
            ApproximationRank = 100
        };

        var estimator = mlContext.Recommendation().Trainers.MatrixFactorization(options);
        model = estimator.Fit(dataSplit.TrainSet);
    }

    public float Predict(uint userId, uint productId)
    {
        var predictionEngine = mlContext.Model.CreatePredictionEngine<ProductRating, ProductRecommendation>(model);
        var prediction = predictionEngine.Predict(new ProductRating { userId = userId, productId = productId });
        return prediction.Score;
    }
}
