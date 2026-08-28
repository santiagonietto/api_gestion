namespace dao_library;
using entity_library;

public class TeamDAO
{
    public Team CreateTeam(Team team)
    {
        team.Id = MockDatabase.Teams.Count > 0
            ? MockDatabase.Teams.Max(t => t.Id) + 1
            : 1;
        MockDatabase.Teams.Add(team);
        return team;
    }

    public Team? ReadTeamById(long id)
    {
        return MockDatabase.Teams.FirstOrDefault(t => t.Id == id);
    }

    public bool UpdateTeam(Team team)
    {
        Team? existing = ReadTeamById(team.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = team.Name;
        existing.Category = team.Category;
        return true;
    }

    public bool DeleteTeamById(long id)
    {
        Team? existing = ReadTeamById(id);
        if (existing == null)
        {
            return false;
        }

        MockDatabase.Teams.Remove(existing);
        return true;
    }

    public List<Team> GetAllTeams()
    {
        return MockDatabase.Teams;
    }
}
