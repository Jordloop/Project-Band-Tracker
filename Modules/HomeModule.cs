using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
 
//root
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
//Band----
      Get["/band"] = _ => {
        List<Band> allBands = Band.GetAll();
        return View["all-band.cshtml", allBands];
      };
//----Add band
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
//----View band
      Get["/band/{id}"] = param => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Band selectedBand = Band.Find(param.id);
        List<Venue> BandVenues = selectedBand.GetVenues();

        model.Add("Band", selectedBand);
        model.Add("Venue", BandVenues);
        return View["this-band.cshtml", model];
      };
//Venue----
      Get["/venue"] = _ => {
        List<Venue> allVenues = Venue.GetAll();
        return View["all-venue.cshtml", allVenues];
      };
//----Add venue
      Get["/venue/add"] = _ => {
        return View["add-venue.cshtml"];
      };
      Post["/venue/add"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["confirmed.cshtml"];
      };
//----View venue
      Get["/venue/{id}"] = param => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Venue selectedVenue = Venue.Find(param.id);
        List<Band> VenueBands = selectedVenue.GetBands();

        model.Add("Venue", selectedVenue);
        model.Add("Band", VenueBands);
        return View["this-venue.cshtml", model];
      };
//----Update venue
      Get["/venue/update/{id}"] = param => {
        Venue selectedVenue = Venue.Find(param.id);
        return View["update-venue.cshtml", selectedVenue];
      };
      Patch["/venue/update/{id}"] = param => {
      Venue selectedVenue = Venue.Find(param.id);
      selectedVenue.Update(Request.Form["update-venue"]);
      return View["confirmed.cshtml"];
    };
//----Delete venue
      Delete["/venue/delete/{id}"] = param => {
      Venue selectedVenue = Venue.Find(param.id);
      selectedVenue.Delete();
      return View["confirmed.cshtml"];
      };
    }
  }
}
