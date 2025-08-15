using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Erp_V1
{
    public class ALSModel
    {
        public Matrix<double> UserFactors { get; private set; }
        public Matrix<double> ProductFactors { get; private set; }

        public ALSModel(double[,] userItemMatrix, int numFeatures, double lambda, int numIterations)
        {
            // Convert the 2D array into MathNet.Numerics Matrix
            var matrix = DenseMatrix.OfArray(userItemMatrix);

            int numUsers = matrix.RowCount;
            int numProducts = matrix.ColumnCount;

            // Randomly initialize user and product factors
            UserFactors = DenseMatrix.Build.Random(numUsers, numFeatures);
            ProductFactors = DenseMatrix.Build.Random(numProducts, numFeatures);

            // ALS iterations
            for (int iter = 0; iter < numIterations; iter++)
            {
                // Update user factors
                for (int u = 0; u < numUsers; u++)
                {
                    var productRatings = matrix.Row(u).ToArray();
                    UpdateFactors(UserFactors, ProductFactors, u, productRatings, lambda);
                }

                // Update product factors
                for (int p = 0; p < numProducts; p++)
                {
                    var userRatings = matrix.Column(p).ToArray();
                    UpdateFactors(ProductFactors, UserFactors, p, userRatings, lambda);
                }
            }
        }

        private void UpdateFactors(Matrix<double> factors, Matrix<double> otherFactors, int index, double[] ratings, double lambda)
        {
            // ALS formula update for a factor vector
            var XTX = otherFactors.TransposeThisAndMultiply(otherFactors);
            var lambdaI = DenseMatrix.CreateIdentity(XTX.RowCount).Multiply(lambda);
            var XTy = otherFactors.TransposeThisAndMultiply(DenseVector.OfArray(ratings));

            // Solve for updated factors using (X^T * X + λI)^-1 * X^T * y
            var updatedFactors = (XTX + lambdaI).Inverse() * XTy;
            factors.SetRow(index, updatedFactors);
        }
    }
}
