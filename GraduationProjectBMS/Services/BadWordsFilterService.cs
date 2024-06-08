using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using System.Linq;

public class BadWordsFilterService
{
    private readonly MLContext _mlContext;
    private readonly ITransformer _model;
    private readonly PredictionEngine<MessageData, MessagePrediction> _predictionEngine;

    public BadWordsFilterService()
    {
        _mlContext = new MLContext();
        _model = TrainModel();
        _predictionEngine = _mlContext.Model.CreatePredictionEngine<MessageData, MessagePrediction>(_model);
    }

    public bool ContainsBadWords(string message)
    {
        var prediction = _predictionEngine.Predict(new MessageData { Message = message });
        return prediction.Prediction;
    }

    private ITransformer TrainModel()
    {
        var dataPath = Path.Combine(Environment.CurrentDirectory, "data.csv");
        IDataView dataView = _mlContext.Data.LoadFromTextFile<MessageData>(dataPath, hasHeader: true, separatorChar: ',');

        var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(MessageData.Message))
            .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(MessageData.Label), featureColumnName: "Features"));

        var model = pipeline.Fit(dataView);

        return model;
    }
}

public class MessageData
{
    [LoadColumn(0)]
    public string Message { get; set; }

    [LoadColumn(1)]
    public bool Label { get; set; }
}

public class MessagePrediction : MessageData
{
    [ColumnName("PredictedLabel")]
    public bool Prediction { get; set; }

    public float Score { get; set; }
}
