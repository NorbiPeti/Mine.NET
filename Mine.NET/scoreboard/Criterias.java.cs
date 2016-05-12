namespace Mine.NET.scoreboard;

/**
 * Criteria names which trigger an objective to be modified by actions in-game
 */
public class Criterias {
    public static readonly String HEALTH;
    public static readonly String PLAYER_KILLS;
    public static readonly String TOTAL_KILLS;
    public static readonly String DEATHS;

    static {
        HEALTH="health";
        PLAYER_KILLS="playerKillCount";
        TOTAL_KILLS="totalKillCount";
        DEATHS="deathCount";
    }

    private Criterias() {}
}
