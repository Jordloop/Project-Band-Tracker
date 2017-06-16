using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Band
  {
    private string _name;
    private int _id;

    public Band(string Name, int Id=0 )
    {
      _name = Name;
      _id =Id;

    }
    public override bool Equals(System.Object otherBand )
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());

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

//GetAll
  public static List<Band> GetAll()
  {
    List<Band> allBands = new List<Band>{};

    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn );
    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int bandId = rdr.GetInt32(0 );
      string bandName = rdr.GetString(1 );

      Band newBand = new Band(bandName, bandId );
      allBands.Add(newBand );
    }

    if (rdr != null )
    {
      rdr.Close();
    }
    if (conn != null )
    {
      conn.Close();
    }
    return allBands;
  }
//Save
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name ) OUTPUT INSERTED.id VALUES (@BandName );", conn );

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@BandName";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter );

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
//DELETE
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
