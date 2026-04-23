public class NotificationService
{
    private readonly IMessageService _messageService;

    public NotificationService(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public string Notify(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Invalid message");

        return _messageService.SendMessage(text);
    }
}
