using System.Net.Http.Json;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);
        
        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }
    public static int getTotalScoredGoals(string team, int year)
    {
        return requestAPILoop("team1", team, year) 
             + requestAPILoop("team2", team, year);
    }
    public static int requestAPILoop(string filterTeam, string team, int year)
    {
        int totalGoals = 0;
        int page = 1;
        bool existNext;
        do
        {
            existNext = false;
            var response = requestAPI(filterTeam, team, year, page).GetAwaiter().GetResult();
            if (response is not null)
            {
                totalGoals += response.TotalGoals(filterTeam == "team1");
                existNext = response.Page < response.Total_Pages;
            }
            page++;
        } while (existNext);
        return totalGoals;
    }

    public static async Task<FootballMatchesPage> requestAPI(string filterTeam, string team, int year, int page)
    {
        var url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&{filterTeam}={team}&page={page}";
        using (HttpClient httpClient = new HttpClient())
        {
            return await httpClient.GetFromJsonAsync<FootballMatchesPage>(url);
        }
    }
    public class FootballMatchesPage
    {
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Total { get; set; }
        public int Total_Pages { get; set; }
        public FootballMatches[] Data { get; set; }

        public int TotalGoals(bool time1)
        {
            int totalGoals = 0;

            foreach (var game in Data)
            {
                if (time1 && int.TryParse(game.Team1Goals, out int goals))
                {
                    totalGoals += goals;
                }
                if (!time1 && int.TryParse(game.Team2Goals, out int goalsTeam2))
                {
                    totalGoals += goalsTeam2;
                }
            }

            return totalGoals;
        }
    }
    public class FootballMatches
    {
        public string Competition { get; set; }
        public int Year { get; set; }
        public string Round { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Team1Goals { get; set; }
        public string Team2Goals { get; set; }
    }
}