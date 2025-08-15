using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Erp_V1.DAL.DTO;
using Erp_V1.ML.DTO; // Required for CustomerPredictionResult

namespace Erp_V1.BLL
{
    /// <summary>
    /// Professional service contract for training and using a customer churn model.
    /// </summary>
    public interface IChurnPredictionService
    {
        /// <summary>
        /// Asynchronously trains a new churn prediction model using the latest data,
        /// evaluates its performance, explains its decisions, and persists it to disk.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the training operation.</param>
        /// <returns>A task representing the asynchronous training operation.</returns>
        Task TrainAndSaveModelAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously predicts customers at risk of churn using the persisted model.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the prediction operation.</param>
        /// <returns>
        /// A task that results in a read-only list of CustomerPredictionResult objects,
        /// each containing the customer and their probability of churning.
        /// </returns>
        Task<IReadOnlyList<CustomerPredictionResult>> GetChurningCustomersAsync(CancellationToken cancellationToken = default);
    }
}