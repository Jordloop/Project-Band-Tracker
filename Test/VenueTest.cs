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

    [Fact]
    public void Find_FindsVenueInDatabase_True()
    {
      //Arrange
      Venue testVenue = new Venue("Al's Den" );
      testVenue.Save();
      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId() );

      //Assert
      Assert.Equal(testVenue, foundVenue );
    }

    [Fact]
     public void AddBand_AddBandToOneVenue_True()
     {
       //Arrange
       Venue testVenue = new Venue("Al's Den" );
       testVenue.Save();

       Band firstBand = new Band("Radiohead" );
       firstBand.Save();
       Band secondBand = new Band("Pink Floyd" );
       secondBand.Save();
       //Act
       testVenue.AddBand(firstBand );
       testVenue.AddBand(secondBand );

       List<Band> result = testVenue.GetBands();
       List<Band> testList = new List<Band>{firstBand, secondBand };
       //Assert
       Assert.Equal(testList, result );
     }

     [Fact]
     public void GetBands_ReturnsAllBandsFromOneVenue_True()
     {
       //Arrange
       Venue testVenue = new Venue("Al's Den" );
       testVenue.Save();

       Band firstBand = new Band("Radiohead" );
       firstBand.Save();
       Band secondBand = new Band("Pink Floyd" );
       secondBand.Save();
       //Act
       testVenue.AddBand(firstBand );
       testVenue.AddBand(secondBand );
       List<Band> testBands = testVenue.GetBands();
       List<Band> contolBands = new List<Band>{firstBand, secondBand };
       //Assert
       Assert.Equal(contolBands, testBands );
     }

    [Fact]
    public void Update_UpdatesVenueInDatabase_True()
    {
      //Arrange
      Venue testVenue = new Venue("Allen's Den" );
      testVenue.Save();
      string newName = "Al's Den";
      //Act
      testVenue.Update(newName );
      string result = testVenue.GetName();
      //Assert
      Assert.Equal(newName, result );
    }

    [Fact]
    public void Delete_DeletesVenueFromDatabase_True()
    {
      //Arrange
      Venue testVenue1 = new Venue("Al's Den" );
      testVenue1.Save();

      Venue testVenue2 = new Venue("Sal's Hen" );
      testVenue2.Save();

      //Act
      testVenue1.Delete();

      List<Venue> resultVenueList = Venue.GetAll();
      List<Venue> testVenueList = new List<Venue>{testVenue2};

      //Assert
      Assert.Equal(testVenueList, resultVenueList );
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }


  }
}
