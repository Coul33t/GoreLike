namespace GoreLike.Interfaces {
    public interface IActor {
        string name { get; set; }
        int awareness { get; set; }

        int attack { get; set; }
        int defense { get; set; }
        int health { get; set; }
        int max_health { get; set; }
        int gold { get; set; }
        int speed { get; set; }
    }
}
