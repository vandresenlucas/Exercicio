using Newtonsoft.Json;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {

        using (HttpClient client = new HttpClient())
        {
            int totalGoals = 0;
            int currentPage = 1;
            int totalPages = 1;

            while (currentPage <= totalPages)
            {
                var apiUrl = $"https://jsonmock.hackerrank.com/api/football_matches?team1={team}&year={year}&page={currentPage}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(jsonResponse);

                    if (currentPage == 1) 
                        totalPages = result.total_pages;

                    foreach (var match in result.data)
                    {
                        totalGoals += int.Parse(match.team1goals.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Erro ao acessar API: " + response.StatusCode);
                    return 0;
                }

                apiUrl = $"https://jsonmock.hackerrank.com/api/football_matches?team2={team}&year={year}&page={currentPage}";

                response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(jsonResponse);

                    foreach (var match in result.data)
                    {
                        totalGoals += int.Parse(match.team2goals.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Erro ao acessar API: " + response.StatusCode);
                    return 0;
                }

                currentPage++;
            }

            return totalGoals;
        }
    }
}