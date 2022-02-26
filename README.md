# ReportSharp.TelegramReporter
report crashes and requests from telegram bot
This Package Reports you Exeptions,Requests,Data from telegram

## Dependencies:
ReportSharp 1.0.5 or later

Dotnet Core 3.1 or later

## Usage : 

### First Step : 

Create Bot from BotFather from telegram and get your bot token

### Second Step : 

You need to install and configure [ReportSharp](https://www.nuget.org/packages/ReportSharp/) 1.0.5 or later to use this package.

You need to install and configure [ReportSharp.Telegram](https://www.nuget.org/packages/ReportSharp.TelegramReporter/)


### Third Step :

Add following Codes to `ConfigureServices` method in `Startup` class:
```cs

services.AddReportSharp(options => {
    options.ConfigReportSharp(configBuilder =>
        configBuilder.SetWatchdogPrefix("/")
    );
    
    
    //for report Request
     options.AddRequestReporter(() => new TelegramReportOptionsBuilder()
                    .SetToken("Your Telegram BotToken")
                    .AddChatIds(new List<int>()
                    {
                        Your Chat Ids
                    }));
                    
      //for report Exception
     options.AddExceptionReporter(() => new TelegramReportOptionsBuilder()
                    .SetToken("Your Telegram BotToken")
                    .AddChatIds(new List<int>()
                    {
                        Your Chat Ids
                    }));
      
        //for report Data
     options.AddDataReporter(() => new TelegramReportOptionsBuilder()
                    .SetToken("Your Telegram BotToken")
                    .AddChatIds(new List<int>()
                    {
                        Your Chat Ids
                    }));
                    
       options.AddReporter<TelegramReporter.TelegramReporter,TelegramReportOptionsBuilder>(
                    () => new TelegramReportOptionsBuilder()
                    .SetToken("Your Telegram BotToken")
                    .AddChatIds(new List<int>()
                    {
                        Your Chat Ids
                    }));
});                    

```


if you want to it for all reporters, you can use only `AddReporter` method

### Fourth Step :

You need to add following lines to `Configure` method in `Startup` class:

```cs
app.UseReportSharp(configure => {
    configure.UseReportSharpMiddleware<ReportSharpMiddleware>();
});

```

## Note :
For find your `chatId` you just need to get : 

` https://api.telegram.org/bot<YourBOTToken>/getUpdates`
