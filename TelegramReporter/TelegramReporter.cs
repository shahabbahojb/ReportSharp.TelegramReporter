using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ReportSharp.Models;
using ReportSharp.Reporters;

namespace Presentation.TelegramReporter;

public class TelegramReporter:IExceptionReporter,IDataReporter,IRequestReporter
{

    private readonly List<int> _chatIds  = new List<int>();
    private string _token;
    
    public TelegramReporter(IOptions<TelegramConfig> options)
    {
        _token = options.Value.Token;
        _chatIds.AddRange(options.Value.ChatIds);
    }
    
    public async Task ReportException(HttpContext httpContext, ReportSharpRequest request, Exception exception)
    {
        for (int i = 0; i < _chatIds.Count; i++)
        {
            await SendAsync(_chatIds[i], request + "  exception : "+ exception.ToString());
        }
    }

    private async Task<bool> SendAsync(int resiverChatId,string message)
    {
        try
        {
            string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
            string apiToken = _token;
            string chatId = resiverChatId.ToString();
            string text = message;
            urlString = String.Format(urlString, apiToken, chatId, text);
            WebRequest myRequest = WebRequest.Create(urlString);
            Stream rs = myRequest.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(rs);
            string line = "";
            StringBuilder sb = new StringBuilder();
            while (line != null)
            {
                line = reader.ReadLine();
                if (line != null)
                    sb.Append(line);
            }
            string response = sb.ToString();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task ReportData(HttpContext httpContext, string tag, string data)
    {
        for (int i = 0; i < _chatIds.Count; i++)
        {
            await SendAsync(_chatIds[i], tag+" data : "+data);
        }
    }

    public async Task ReportRequest(HttpContext httpContext, ReportSharpRequest request)
    {
        for (int i = 0; i < _chatIds.Count; i++)
        {
            await SendAsync(_chatIds[i], request.ToString());
        }
    }
}