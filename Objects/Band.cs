using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Band
  {
    private string _name;
    private int _id;

    public Band(string Name, int Id=0)
    {
      _name = Name;
      _id =Id;

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
    public void SetName(string Name)
    {
      _name = Name;
    }
    public void SetId(int Id)
    {
      _id = Id;
    }

//Class Methods-----


  public static List<Band> GetAll()
  {
    List<Band> allBands = new List<Band>{};

    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int bandId = rdr.GetInt32(0);
      string bandName = rdr.GetString(1);

      Band newBand = new Band(bandName, bandId);
      allBands.Add(newBand);
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

  }
}
