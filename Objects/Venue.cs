using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
  public class Venue
  {
    private string _name;
    private int _id;

    public Venue(string Name, int Id=0)
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



  }
}
