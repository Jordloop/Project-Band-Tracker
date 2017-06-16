using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class BandTest : IDisposable



  {
    public BandTest()
    { //  This tells the application where to find the test database.
      //  This overrides "DBConfiguration.ConnectionString" in Startup.cs.
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_DatabaseIsEmpty_True()
    {
      //Arrange, act
      int atcual = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, atcual);
    }

    [Fact]
    public void Equals_NamesAreTheSame_True()
    {
      //Arrange, Act
      Band firstBand = new Band("Radiohead" );
      Band secondBand = new Band("Radiohead" );

      //Assert
      Assert.Equal(firstBand, secondBand );
    }

    [Fact]
    public void Save_SavesBandToDatabase_True()
    {
      //Arrange
      Band testBand = new Band("Radiohead" );
      //Act
      testBand.Save();
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand };
      //Assert
      Assert.Equal(testList, result );
    }

    [Fact]
    public void Find_FindsBandInDatabase_True()
    {
      //Arrange
      Band testBand = new Band("Radiohead" );
      testBand.Save();
      //Act
      Band foundBand = Band.Find(testBand.GetId() );
      //Assert
      Assert.Equal(testBand, foundBand );
    }

    [Fact]
     public void AddVenue_AddVenuesToOneBand_True()
     {
       //Arrange
       Band testBand = new Band("Radiohead" );
       testBand.Save();

       Venue firstVenue = new Venue("Al's Den" );
       firstVenue.Save();
       Venue secondVenue = new Venue("Porland Zoo" );
       secondVenue.Save();
       //Act
       testBand.AddVenue(firstVenue );
       testBand.AddVenue(secondVenue );

       List<Venue> result = testBand.GetVenues();
       List<Venue> testList = new List<Venue>{firstVenue, secondVenue };
       //Assert
       Assert.Equal(testList, result );
     }

     [Fact]
     public void GetVenues_ReturnsAllVenuesFromOneBand_True()
     {
       //Arrange
       Band testBand = new Band("Radiohead" );
       testBand.Save();

       Venue firstVenue = new Venue("Al's Den" );
       firstVenue.Save();
       Venue secondVenue = new Venue("Portland Zoo" );
       secondVenue.Save();
       //Act
       testBand.AddVenue(firstVenue );
       testBand.AddVenue(secondVenue );
       List<Venue> testVenues = testBand.GetVenues();
       List<Venue> contolVenues = new List<Venue>{firstVenue, secondVenue };
       //Assert
       Assert.Equal(contolVenues, testVenues );
     }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
