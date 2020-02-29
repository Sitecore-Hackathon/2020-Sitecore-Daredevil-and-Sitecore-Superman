using System.Collections.Generic;

namespace Daredevil.Feature.Hackathon.Models
{
    public class TeamsModel
    {
        public class CountryTeams
        {
           public List<CountryTeam> CountryTeamList { get; set; }
        }
        public class CountryTeam
        {
            public string Country { get; set; }
            public List<TeamInfo> TeamsList { get; set; }
        }
        public class TeamInfo
        {
            public string Name { get; set; }
            public string Imgurl { get; set; }
            public List<Members> MembersList { get; set; }
        }
        public class Members
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string LinkedInUrl { get; set; }
            public string TwitterHandle { get; set; }
            public string Website { get; set; }
        }
    }
}