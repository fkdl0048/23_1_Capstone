public class Player
{
    private int _id;
    private string _name;

    public int Id { get => _id; }
    public string Name { get => _name; }
    
    public Player(int id, string name)
    {
        _id = id;
        _name = name;
    }
}
