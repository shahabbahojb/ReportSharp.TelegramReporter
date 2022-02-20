using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Builder.ReporterOptionsBuilder;

namespace Presentation.TelegramReporter;

public class TelegramReportOptionsBuilder : IExceptionReporterOptionsBuilder<TelegramReporter>
    , IRequestReporterOptionsBuilder<TelegramReporter>,
    IDataReporterOptionsBuilder<TelegramReporter>
{
    private readonly List<int> _chatids = new List<int>();
    private string _token;

    public void Build(IServiceCollection serviceCollection)
    {
        Console.WriteLine("Telegram Bot is Here !");

        serviceCollection.Configure<TelegramConfig>(config =>
        {
            config.Token = _token;
            config.ChatIds = _chatids;
        });
    }


    public TelegramReportOptionsBuilder SetToken(string token)
    {
        _token = token;
        return this;
    }

    public TelegramReportOptionsBuilder AddChatIds(List<int> chatids)
    {
        _chatids.AddRange(chatids);
        return this;
    }
}