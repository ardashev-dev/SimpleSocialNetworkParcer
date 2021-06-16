using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NDesk.Options;

namespace SimpleSocialNetworkParser
{
  class Program
  {
    static void Main(string[] args)
    {
      #region Обработка входных параметров.
      bool isHelp = false;

      var network = string.Empty;
      var action = string.Empty;
      var query = string.Empty;
      DateTime startDate = DateTime.Now;
      DateTime endDate = DateTime.Now;
      var startDateString = string.Empty;
      var endDateString = string.Empty;
      var exportVariant = string.Empty;

      var p = new OptionSet() {
        { "n|network=",  "Тип социальной сети. vk", v => network = v },
        { "a|action=",  "Действие. Get или Send", v => action = v },
        { "q|query=",  "Запрос поиска. Если надо найти сообщения по хештегу, то указать хештеги со знаком #", v => query = v },
        { "s|startDate=",  "Дата начала поиска в формате yyyy-MM-dd.", v => startDateString = v },
        { "e|endDate=",  "Конечная дата поиска в формате yyyy-MM-dd.", v => endDateString = v },
        { "x|exportVariant=",  "Вариант экспорта данных. csv", v => exportVariant = v },
        { "h|help", "Показать справку.", v => isHelp = (v != null) },
      };

      

      try
      {
        p.Parse(args);
      }
      catch (OptionException e)
      {
        Console.WriteLine("Invalid arguments: " + e.Message);
        p.WriteOptionDescriptions(Console.Out);
        return;
      }

      if (isHelp || string.IsNullOrEmpty(network) || string.IsNullOrEmpty(action) || string.IsNullOrEmpty(query))
      {
        p.WriteOptionDescriptions(Console.Out);
        return;
      }
      #endregion

      if (!DateTime.TryParseExact(startDateString, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out startDate))
      startDate = DateTime.Now.Date;

      if (!DateTime.TryParseExact(endDateString, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out endDate))
        endDate = DateTime.Now.Date;

      var actionResult = MakeAction(network, action, query, startDate, endDate);
      if (actionResult.MessageStructure.Count > 0)
      {
        ExportResult(actionResult.MessageStructure, exportVariant);
      }

    }

    private static Structures.Message.ActionResult MakeAction(string network, string action, string query, DateTime startDate, DateTime endDate)
    {
      var result = new Structures.Message.ActionResult();
      var getResult = new List<Structures.Message.MessageStructure>();
      switch (network.ToLower())
      {
        case "vk":
          if (action.ToLower() == "get")
          {
            var token = Secret.Data.VkToken;
            var vkParser = new Parsers.VkParser(token);
            getResult.AddRange(vkParser.GetMessagesByHashtag(query, startDate, endDate));
          }
          break;
        default:
          break;
      }
      result.MessageStructure = getResult;
      return result;
    }

    private static void ExportResult(List<Structures.Message.MessageStructure> messageList, string variant)
    {
      switch (variant.ToLower())
      {
        case "csv":
          var content = new StringBuilder();
          content.AppendLine(string.Format("{0};{1};{2};{3};{4};{5};", "Id", "Date", "OwnerId", "AutorId", "Text", "Type"));

          foreach (var item in messageList)
            content.AppendLine(string.Format("{0};{1};{2};{3};{4};{5};",
              item.Id,
              item.Date.HasValue ? item.Date.Value.ToString("yyyy-MM-dd") : string.Empty,
              item.OwnerId,
              item.AutorId,
              item.Text.Replace("\r"," ").Replace("\n", ""),
              item.Type));

          var fileName = string.Format("{0}.csv", DateTime.Now.ToString("yyyy-MM-dd_HHmmss"));
          var path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), fileName);
          System.IO.File.WriteAllText(path, content.ToString());
          break;
        default:
          break;
      }
    }

  }
}
