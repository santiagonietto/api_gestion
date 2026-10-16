namespace dao_library;
using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

public class PlayerDAO
{
    private readonly AppDbContext _db;

    public PlayerDAO(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Player> CreatePlayerAsync(Player player)
    {
        await AttachTeamsAsync(player);
        _db.Players.Add(player);
        await _db.SaveChangesAsync();
        return player;
    }

    public Task<Player?> ReadPlayerByIdAsync(long id)
    {
        return _db.Players
            .Include(p => p.Teams)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Player>> GetPlayersByTeamAsync(long teamId)
    {
        return await _db.Players
            .Where(p => p.Teams.Any(t => t.Id == teamId))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> UpdatePlayerAsync(Player player)
    {
        Player? existing = await _db.Players
            .Include(p => p.Teams)
            .FirstOrDefaultAsync(p => p.Id == player.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = player.Name;
        existing.Age = player.Age;
        existing.Dni = player.Dni;
        existing.Numero = player.Numero;
        await SyncTeamsAsync(existing, player.Teams.Select(t => t.Id).ToList());
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePlayerByIdAsync(long id)
    {
        Player? existing = await _db.Players.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _db.Players.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Player>> GetAllPlayersAsync()
    {
        return _db.Players.Include(p => p.Teams).AsNoTracking().ToListAsync();
    }

    private async Task AttachTeamsAsync(Player player)
    {
        if (player.Teams.Count == 0) return;
        var ids = player.Teams.Select(t => t.Id).Where(id => id != 0).ToList();
        if (ids.Count == 0) return;
        var attached = await _db.Teams.Where(t => ids.Contains(t.Id)).ToListAsync();
        player.Teams = attached.Concat(player.Teams.Where(t => t.Id == 0)).ToList();
    }

    private async Task SyncTeamsAsync(Player existing, List<long> teamIds)
    {
        var teams = await _db.Teams.Where(t => teamIds.Contains(t.Id)).ToListAsync();
        existing.Teams.Clear();
        foreach (var team in teams)
        {
            existing.Teams.Add(team);
        }
    }
}
