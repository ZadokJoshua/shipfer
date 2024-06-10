using Newtonsoft.Json;

namespace Shipfer.Models;

public class Entity
{
    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("offset")]
    public int Offset { get; set; }

    [JsonProperty("length")]
    public int Length { get; set; }

    [JsonProperty("confidenceScore")]
    public double ConfidenceScore { get; set; }

    [JsonProperty("extraInformation")]
    public List<ExtraInformation> ExtraInformation { get; set; }
}

public class ExtraInformation
{
    [JsonProperty("extraInformationKind")]
    public string ExtraInformationKind { get; set; }

    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("regexPattern")]
    public string RegexPattern { get; set; }
}

public class Intent
{
    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("confidenceScore")]
    public double ConfidenceScore { get; set; }
}

public class Prediction
{
    [JsonProperty("topIntent")]
    public string TopIntent { get; set; }

    [JsonProperty("projectKind")]
    public string ProjectKind { get; set; }

    [JsonProperty("intents")]
    public List<Intent> Intents { get; set; }

    [JsonProperty("entities")]
    public List<Entity> Entities { get; set; }
}

public class Result
{
    [JsonProperty("query")]
    public string Query { get; set; }

    [JsonProperty("prediction")]
    public Prediction Prediction { get; set; }
}

public class LanguagePredictionResponse
{
    [JsonProperty("kind")]
    public string Kind { get; set; }

    [JsonProperty("result")]
    public Result Result { get; set; }
}