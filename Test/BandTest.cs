using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class BandTest
  // : IDisposable



  {
    public BandTest()
    { //  This tells the application where to find the test database.
      //  This overrides "DBConfiguration.ConnectionString" in Startup.cs.
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseIsEmpty_True()
    {
      //Arrange, act
      int atcual = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, atcual);
    }

  }
}
