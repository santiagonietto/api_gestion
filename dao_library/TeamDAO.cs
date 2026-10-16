namespace dao_library;
using dao_library.entity_framework;
using entity_library;
using Microsoft.EntityFrameworkCore;

public class TeamDAO
{
    private readonly AppDbContext _db;

    public TeamDAO(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Team> CreateTeamAsync(Team team)
    {
        await AttachPlayersAsync(team);
        _db.Teams.Add(team);
        await _db.SaveChangesAsync();
        return team;
    }

    public Task<Team?> ReadTeamByIdAsync(long id)
    {
        return _db.Teams
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> UpdateTeamAsync(Team team)
    {
        Team? existing = await _db.Teams
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.Id == team.Id);
        if (existing == null)
        {
            return false;
        }

        existing.Name = team.Name;
        existing.Category = team.Category;
        await SyncPlayersAsync(existing, team.Players.Select(p => p.Id).ToList());
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTeamByIdAsync(long id)
    {
        Team? existing = await _db.Teams.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        _db.Teams.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Team>> GetAllTeamsAsync()
    {
        return _db.Teams.Include(t => t.Players).AsNoTracking().ToListAsync();
    }

    private async Task AttachPlayersAsync(Team team)
    {
        if (team.Players.Count == 0) return;
        var ids = team.Players.Select(p => p.Id).Where(id => id != 0).ToList();
        if (ids.Count == 0) return;
        var attached = await _db.Players.Where(p => ids.Contains(p.Id)).ToListAsync();
        team.Players = attached.Concat(team.Players.Where(p => p.Id == 0)).ToList();
    }

    private async Task SyncPlayersAsync(Team existing, List<long> playerIds)
    {
        var players = await _db.Players.Where(p => playerIds.Contains(p.Id)).ToListAsync();
        existing.Players.Clear();
        foreach (var player in players)
        {
            existing.Players.Add(player);
        }
    }
}
