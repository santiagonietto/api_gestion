namespace dao_library;
using entity_library;

public class PlayerDAO
{
    public Player CreatePlayer(Player player)
    {
        player.Id = MockDatabase.Players.Count > 0
            ? MockDatabase.Players.Max(p => p.Id) + 1
            : 1;
        MockDatabase.Players.Add(player);
        return player;
    }

    public Player? ReadPlayerById(long id)
    {
        return MockDatabase.Players.FirstOrDefault(p => p.Id == id);
    }

    public List<Player> GetPlayersByTeam(long teamId)
    {
        Team? team = MockDatabase.Teams.FirstOrDefault(t => t.Id == teamId);
        return team != null ? team.Players : new List<Player>();
    }

    public bool UpdatePlayer(Player player)
    {
        Player? existing = ReadPlayerById(player.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = player.Name;
        existing.Age = player.Age;
        existing.Dni = player.Dni;
        existing.Numero = player.Numero;
        return true;
    }

    public bool DeletePlayerById(long id)
    {
        Player? existing = ReadPlayerById(id);
        if (existing == null)
        {
            return false;
        }

        MockDatabase.Players.Remove(existing);
        return true;
    }

    public List<Player> GetAllPlayers()
    {
        return MockDatabase.Players;
    }
}
