namespace entity_library;

public class Player : Person
{
    private int numero = 0;
    private List<Team> teams = new();

    public int Numero
    {
        get { return numero; }
        set { numero = value; }
    }

    public List<Team> Teams
    {
        get { return teams; }
        set { teams = value; }
    }

    public void AddTeam(Team team)
    {
        teams.Add(team);
    }
}
