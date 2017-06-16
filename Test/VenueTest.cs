using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class VenueTest : IDisposable



  {
    public VenueTest()
    { //  This tells the application where to find the test database.
      //  This overrides "DBConfiguration.ConnectionString" in Startup.cs.
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_DatabaseIsEmpty_True()
    {
      //Arrange, act
      int atcual = Venue.GetAll().Count;
      //Assert
      Assert.Equal(0, atcual);
    }

    [Fact]
    public void Test_NamesAreTheSame_True()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("Al's Den" );
      Venue secondVenue = new Venue("Al's Den" );

      //Assert
      Assert.Equal(firstVenue, secondVenue );
    }

    [Fact]
    public void Test_SavesVenueToDatabase_True()
    {
      //Arrange
      Venue testVenue = new Venue("Al's Den" );
      //Act
      testVenue.Save();
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue };
      //Assert
      Assert.Equal(testList, result );
    }

    public void Dispose()
    {
      Venue.DeleteAll();
    }


  }
}
