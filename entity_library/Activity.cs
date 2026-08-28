namespace entity_library;

public class Activity
{
    private long id = 0;
    private string title = "";
    private string? description;
    private DateTime date = DateTime.MinValue;
    private TypeActivity typeActivity = TypeActivity.Homework;

    public long Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string? Description
    {
        get { return description; }
        set { description = value; }
    }

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

    public TypeActivity TypeActivity
    {
        get { return typeActivity; }
        set { typeActivity = value; }
    }
}
