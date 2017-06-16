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

      Get["/venue"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["all-venue.cshtml", allVenues];
      };
    }
  }
}
