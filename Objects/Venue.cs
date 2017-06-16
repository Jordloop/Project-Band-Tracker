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
//Getters----
    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }
//Setters----
    public void SetName(string Name )
    {
      _name = Name;
    }
    public void SetId(int Id )
    {
      _id = Id;
    }

//Class Methods-----
//GetAll()
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
//----Save()
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name ) OUTPUT INSERTED.id VALUES (@VenueName );", conn );

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@VenueName";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {

        this._id = rdr.GetInt32(0 );
      }
      if(rdr != null )
      {
        rdr.Close();
      }
      if(conn != null )
      {
        conn.Close();
      }
    }
//----Find()
    public static Venue Find(int id )
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn );

      SqlParameter VenueIdParameter = new SqlParameter();
      VenueIdParameter.ParameterName = "@VenueId";
      VenueIdParameter.Value = id.ToString();
      cmd.Parameters.Add(VenueIdParameter );

      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0 );
        foundVenueName = rdr.GetString(1 );
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueId );

      if (rdr != null )
      {
        rdr.Close();
      }
      if (conn != null )
      {
        conn.Close();
      }
      return foundVenue;
    }
//----AddBand()
    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO band_venue_join(band_id, venue_id) VALUES (@BandId, @VenueId)", conn );

      SqlParameter BandIdParam = new SqlParameter();
      BandIdParam.ParameterName = "@BandId";
      BandIdParam.Value = newBand.GetId();
      cmd.Parameters.Add(BandIdParam );

      SqlParameter VenueIdParam = new SqlParameter();
      VenueIdParam.ParameterName = "@VenueId";
      VenueIdParam.Value = this.GetId();
      cmd.Parameters.Add(VenueIdParam );

      cmd.ExecuteNonQuery();
      if(conn != null )
      {
        conn.Close();
      }
    }
//----GetBands()
    public List<Band> GetBands()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN band_venue_join ON (venues.id = band_venue_join.venue_id) JOIN bands ON (bands.id = band_venue_join.band_id) WHERE venues.id = @VenueId", conn );

        SqlParameter VenueIdParam = new SqlParameter();
        VenueIdParam.ParameterName = "@VenueId";
        VenueIdParam.Value = this.GetId().ToString();

        cmd.Parameters.Add(VenueIdParam);

        SqlDataReader rdr = cmd.ExecuteReader();

        List<Band> bands = new List<Band>{};

        while(rdr.Read())
        {
          int bandId = rdr.GetInt32(0);
          string bandName = rdr.GetString(1);

          Band newBand = new Band(bandName, bandId);
          bands.Add(newBand);
        }

        if(rdr != null)
        {
          rdr.Close();
        }
        if(conn != null)
        {
          conn.Close();
        }
        return bands;
      }
//----Update()
    public void Update(string newName )
    {
      Console.WriteLine("Ding!");
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName OUTPUT INSERTED.name WHERE id = @VenueId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter );

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter );

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0 );
      }
      if(rdr != null )
      {
        rdr.Close();
      }
      if(conn != null )
      {
        conn.Close();
      }
    }
//----Delete()
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId;", conn );

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();

      cmd.Parameters.Add(venueIdParameter );
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }
//----DeleteAll()
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
