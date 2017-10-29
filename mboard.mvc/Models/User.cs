using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;
using Newtonsoft.Json;

namespace mboard.mvc.Models
{
  public class User
  {
    public string Name { get; set; }
    public int Id { get; set; }


    public void CreateUser()
    {
      var client = NeoDbContext.GClient;
      client.Connect();

      var newUser = new User { Id = this.Id, Name = this.Name };
      client.Cypher
        .Create("(user:User {newUser})")
        .WithParam("newUser", newUser)
        .ExecuteWithoutResults();



    }

  }
}