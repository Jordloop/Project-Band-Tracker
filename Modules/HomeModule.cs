using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {

// root -> index.cshtml
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/band"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["all-band.cshtml", allBands];
      };

      Get["/band/add"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["add-band.cshtml", allVenues];
      };

      Post["/band/add"] = _ => {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        Venue selectedVenue = Venue.Find(Request.Form["venue-id"]);
        newBand.AddVenue(selectedVenue);
        return View["confirmed.cshtml"];
      };

      Get["/venue"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["all-venue.cshtml", allVenues];
      };

      Get["/venue/add"] = _ => {
        return View["add-venue.cshtml"];
      };

      Post["/venue/add"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["confirmed.cshtml"];
      };

    }
  }
}
