using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace GraduationProjectBMS.Repositories.MLModel
{
    public class MLModel : IMLModel
    {
        private readonly ITransformer _model;
        private readonly MLContext _mlContext;

        public MLModel()
        {
            _mlContext = new MLContext();
            _model = _mlContext.Model.Load("data/model.zip", out var modelInputSchema);
        }

        public bool IsMessageAllowed(string message)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<MessageData, MessagePrediction>(_model);
            var input = new MessageData { Text = message };
            var prediction = predictionEngine.Predict(input);
            return !prediction.Prediction;  // Assuming Label 1 is bad and 0 is good
        }
    }
}
