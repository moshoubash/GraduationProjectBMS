using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

public class MessageData
{
    [LoadColumn(0)]
    public string Text { get; set; }

    [LoadColumn(1)]
    public bool Label { get; set; }
}

public class MessagePrediction : MessageData
{
    [ColumnName("PredictedLabel")]
    public bool Prediction { get; set; }
    public float Probability { get; set; }
    public float Score { get; set; }
}

public class ModelBuilder
{
    private static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "data", "messages.csv");
    private static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "data", "model.zip");

    public static void Train()
    {
        var context = new MLContext();

        var data = context.Data.LoadFromTextFile<MessageData>(_dataPath, hasHeader: true, separatorChar: ',');

        var pipeline = context.Transforms.Text.FeaturizeText("Features", nameof(MessageData.Text))
            .Append(context.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: nameof(MessageData.Label), featureColumnName: "Features"));

        var model = pipeline.Fit(data);

        context.Model.Save(model, data.Schema, _modelPath);
    }
}
