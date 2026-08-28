namespace entity_library;

public class Team
{
    private long id = 0;
    private string name = "";
    private string category = "";
    private List<Player> players = new();

    public long Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Category
    {
        get { return category; }
        set { category = value; }
    }

    public List<Player> Players
    {
        get { return players; }
        set { players = value; }
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }
}
