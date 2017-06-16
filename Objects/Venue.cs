using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Venue
  {
    private string _name;
    private int _id;

    public Venue(string Name, int Id=0 )
    {
      _name = Name;
      _id =Id;

    }

    public override bool Equals(System.Object otherVenue )
    {
      if (!(otherVenue is Venue ))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());

        return (idEquality && nameEquality );
      }
    }
//Getters
    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }
//Setters
    public void SetName(string Name )
    {
      _name = Name;
    }
    public void SetId(int Id )
    {
      _id = Id;
    }

//Class Methods-----

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn );
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0 );
        string venueName = rdr.GetString(1 );

        Venue newVenue = new Venue(venueName, venueId );
        allVenues.Add(newVenue );
      }

      if (rdr != null )
      {
        rdr.Close();
      }
      if (conn != null )
      {
        conn.Close();
      }
      return allVenues;
    }


  }
}
