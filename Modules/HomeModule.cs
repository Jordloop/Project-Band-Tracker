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

    }
  }
}
