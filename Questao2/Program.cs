using Newtonsoft.Json;

public class Program
{
    private const string TEAM1 = "team1";
    private const string TEAM2 = "team2";

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
                var response = await GetInformations(TEAM1, team, year, currentPage);

                if (response.IsSuccessStatusCode)
                {
                    dynamic result = await GetObjectResult(response);

                    if (currentPage == 1)
                        totalPages = result.total_pages;

                    totalGoals += GetTotalGoals(TEAM1, result);
                }
                else
                {
                    Console.WriteLine("Erro ao acessar API: " + response.StatusCode);
                    return 0;
                }

                response = await GetInformations(TEAM2, team, year, currentPage);

                if (response.IsSuccessStatusCode)
                {
                    dynamic result = await GetObjectResult(response);

                    totalGoals += GetTotalGoals(TEAM2, result);
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

    private static int GetTotalGoals(string param, dynamic result)
    {
        var goals = 0;

        foreach (var match in result.data)
        {
            goals += param == TEAM1 
                ? int.Parse(match.team1goals.ToString())
                : int.Parse(match.team2goals.ToString());
        }

        return goals;
    }

    private static async Task<dynamic> GetObjectResult(HttpResponseMessage response)
    {
        string jsonResponse = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(jsonResponse);
        return result;
    }

    private static async Task<HttpResponseMessage> GetInformations(string paramName, string team, int year, int currentPage)
    {
        var apiUrl = $"https://jsonmock.hackerrank.com/api/football_matches?{paramName}={team}&year={year}&page={currentPage}";

        var client = new HttpClient();
        return await client.GetAsync(apiUrl);
    }

}