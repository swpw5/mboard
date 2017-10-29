using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Neo4j.Driver.V1;
using System.Collections.Generic;
using System.Diagnostics;
using Neo4jClient;
using Newtonsoft.Json;
using mboard.mvc.Models;


namespace mboard.mvc.Tests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestMethod1()
    {
      var client = new GraphClient(new Uri("http://localhost:7474/db/data"), username: "neo4j", password: "alpha");
      client.Connect();

      bool isConnected = client.IsConnected;
      Debug.Write(isConnected);
      if (isConnected)
      {



        var query = client.Cypher
            .Match("(n:Person)")
            .Return(n => n.As<User>()).Results;


        foreach (var item in query)
        {
          Debug.WriteLine(item.ToString());
        }

      }




      //      MATCH(n: Person) -[r: ACTED_IN]->(m: Movie) where n.name = "Laurence Fishburne"
      //return n, r, m

    }



    [TestMethod]
    public void Test()
    {

      using (var driver = GraphDatabase.Driver("http://localhost:7474", AuthTokens.Basic("neo4j", "neo4j")))
      using (var session = driver.Session())
      {
        session.Run("CREATE (a:Person {name: {name}, title: {title}})",
                    new Dictionary<string, object> { { "name", "Arthur" }, { "title", "King" } });

        var result = session.Run("MATCH (a:Person) WHERE a.name = {name} " +
                                 "RETURN a.name AS name, a.title AS title",
                                 new Dictionary<string, object> { { "name", "Arthur" } });

        foreach (var record in result)
        {
          Debug.WriteLine($"{record["title"].As<string>()} {record["name"].As<string>()}");
        }

      }

    }
  }
}
