using Shipfer.Models;

namespace Shipfer.Controls;

public class MessageDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate BotMessage { get; set; }
    public DataTemplate UserMessage { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return ((Message)item).IsBot ? BotMessage : UserMessage;
    }
}
