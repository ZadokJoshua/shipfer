using Azure.AI.Language.Conversations;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipfer.Models;
using Azure.Core;
using Newtonsoft.Json;

namespace Shipfer.Services;

public class LanguageUnderstandingService
{
    private readonly string _projectName = "shipfer-clu";
    private readonly string _deploymentName = "shipfer-clu";

    private readonly ConversationAnalysisClient _conversationAnalysisClient;
    public LanguageUnderstandingService()
    {
        Uri endpoint = new(AppConfig.LANGUAGE_PREDICTION_URL);
        AzureKeyCredential credential = new(AppConfig.LANGUAGE_API_KEY);

        _conversationAnalysisClient = new ConversationAnalysisClient(endpoint, credential);
    }

    public LanguagePredictionResponse GetIntentsAndEntities(string userInput)
    {
        var data = new
        {
            analysisInput = new
            {
                conversationItem = new
                {
                    text = userInput,
                    id = "1",
                    participantId = "1",
                }
            },
            parameters = new
            {
                projectName = _projectName,
                deploymentName = _deploymentName,
                StringIndexType = "Utf16CodeUnit",
            },
            kind = "Conversation"
        };

        Response response = _conversationAnalysisClient.AnalyzeConversation(RequestContent.Create(data));

        string content = response.Content.ToString();
        var prediction = JsonConvert.DeserializeObject<LanguagePredictionResponse>(content);

        return prediction;
    }
}
