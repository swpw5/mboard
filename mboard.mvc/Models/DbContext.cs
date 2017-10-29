using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4j.Driver.V1;
using Neo4jClient;
using Newtonsoft.Json;

namespace mboard.mvc.Models
{
  public static class NeoDbContext
  {
    public  static GraphClient GClient
    {
      get
      {
       return new GraphClient(new Uri("http://localhost:7474/db/data"), username: "neo4j", password: "alpha");
      
      }
    }
  }
}