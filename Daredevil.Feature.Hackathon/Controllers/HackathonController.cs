using System.Collections.Generic;
using System.Web.Mvc;
using Daredevil.Feature.Hackathon.Models;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Daredevil.Feature.Hackathon.Controllers
{
    public class HackathonController : Controller
    {
        public ActionResult TitleDescription()
        {
            var item = RenderingContext.Current.Rendering.Item;
            var viewmodel = new TitleDescriptionModel(){Title = item.Fields["Title"].Value, Description = item.Fields["Description"].Value };

            return View(viewmodel);
        }

        public ActionResult Teams()
        {
            var items = Sitecore.Context.Database.GetItem("/sitecore/content/Home/Site Content/Teams").Children; // RenderingContext.Current.Rendering.Item.GetChildren();

            var viewmodel = new TeamsModel.CountryTeams();

            List<TeamsModel.CountryTeam> countryteamslist = new List<TeamsModel.CountryTeam>();

            viewmodel.CountryTeamList = countryteamslist;

            foreach (Sitecore.Data.Items.Item child in items)
            {
                var countryteam = new TeamsModel.CountryTeam() {Country = child.Name};
                List<TeamsModel.TeamInfo> teaminfolist= new List<TeamsModel.TeamInfo>();

                foreach (Sitecore.Data.Items.Item team in child.Children)
                {
                    Sitecore.Data.Fields.ImageField imgField = team.Fields["Team Logo"];
                    string imageurl = "";
                    if (imgField?.MediaItem != null)
                    {
                        imageurl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(imgField.MediaItem);
                    }

                    TeamsModel.TeamInfo teaminfo = new TeamsModel.TeamInfo
                    {
                        Name = team.Fields["Name"].Value,
                        Imgurl = imageurl
                    };
                    //add members
                    //Read the Multifield List
                    Sitecore.Data.Fields.MultilistField multiselectField = team.Fields["Members"];
                    Sitecore.Data.Items.Item[] members = multiselectField.GetItems();

                    List<TeamsModel.Members> memberlist=new List<TeamsModel.Members>();
                    foreach (Item memb in members)
                    {
                        var member = new TeamsModel.Members();
                        member.Name = memb.Fields["Name"].Value;
                        member.Email = memb.Fields["Email"].Value;

                        Sitecore.Data.Fields.LinkField linkField = memb.Fields["LinkedIn Url"];
                        member.LinkedInUrl = linkField.GetFriendlyUrl();

                        member.TwitterHandle = memb.Fields["Twitter Handle"].Value;

                        Sitecore.Data.Fields.LinkField website = memb.Fields["website"];
                        member.Website = website.GetFriendlyUrl();

                        memberlist.Add(member);
                    }
                    teaminfo.MembersList= memberlist;
                    teaminfolist.Add(teaminfo);
                }

                countryteam.TeamsList = teaminfolist;
                viewmodel.CountryTeamList.Add(countryteam);
            }


            return View(viewmodel);
        }
    }
}