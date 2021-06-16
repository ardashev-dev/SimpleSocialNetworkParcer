using System;
using System.Collections.Generic;
using System.Text;
using VkNet.Model;

namespace SimpleSocialNetworkParser.Parsers
{
  class VkParser
  {
    private VkNet.VkApi api;
    public VkParser(string token)
    {
      api = new VkNet.VkApi();
      api.Authorize(new ApiAuthParams
      {
        AccessToken = token
      });
    }


    public List<Structures.Message.MessageStructure> GetMessagesByHashtag(string hashTag, DateTime startTime, DateTime endTime)
    {
      var messages = new List<Structures.Message.MessageStructure>();
      var parameters = new VkNet.Model.RequestParams.NewsFeedSearchParams();
      parameters.Query = hashTag;
      parameters.StartTime = startTime;
      parameters.EndTime = endTime;
      var response = api.NewsFeed.Search(parameters);
      
      foreach (var item in response.Items)
      {
        messages.Add(new Structures.Message.MessageStructure()
        {
          Id = item.Id,
          Date = item.Date,
          OwnerId = item.OwnerId,
          AutorId = item.FromId,
          Text = item.Text,
          Type = item.PostType.ToString()
        });
      }

      return messages;
    }
  }
}
