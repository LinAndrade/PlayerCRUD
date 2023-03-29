public static class PlayerRepository
{
    public static List<Player> Player = new List<Player>();

    public static void Add(Player player)
    {
        if (player == null)
            Player = new List<Player>();

        Player.Add(player);
    }

    public static Player Getby(string name)
    {
        return Player.FirstOrDefault(p => p.Name == name);
    }
    public static void Remove (Player player)
    {
        Player.Remove(player);
    }
}
