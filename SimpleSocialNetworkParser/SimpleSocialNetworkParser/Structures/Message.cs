using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocialNetworkParser.Structures
{
  public class Message
  {
    public struct MessageStructure
    {
      public long Id { get; set; }
      public DateTime? Date { get; set; }
      public long OwnerId { get; set; }
      public long AutorId { get; set; }
      public string Text { get; set; }
      public string Type { get; set; }
    }

    public struct ActionResult
    { 
      public List<Structures.Message.MessageStructure> MessageStructure { get; set; }
    }
  }
}
