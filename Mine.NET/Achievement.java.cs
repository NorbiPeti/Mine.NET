/**
 * Represents an achievement, which may be given to players.
 */
public class Achievement {
    public static readonly Achievement OPEN_INVENTORY = new Achievement();

    public readonly Achievement MINE_WOOD = new Achievement(OPEN_INVENTORY);
    public readonly Achievement BUILD_WORKBENCH = new Achievement(MINE_WOOD);
    public readonly Achievement BUILD_PICKAXE = new Achievement(BUILD_WORKBENCH);
    public readonly Achievement BUILD_FURNACE = new Achievement(BUILD_PICKAXE);
    public readonly Achievement ACQUIRE_IRON = new Achievement(BUILD_FURNACE);
    public readonly Achievement BUILD_HOE = new Achievement(BUILD_WORKBENCH);
    public readonly Achievement MAKE_BREAD = new Achievement(BUILD_HOE);
    public readonly Achievement BAKE_CAKE = new Achievement(BUILD_HOE);
    public readonly Achievement BUILD_BETTER_PICKAXE = new Achievement(BUILD_PICKAXE);
    public readonly Achievement COOK_FISH = new Achievement(BUILD_FURNACE);
    public readonly Achievement ON_A_RAIL = new Achievement(ACQUIRE_IRON);
    public readonly Achievement BUILD_SWORD = new Achievement(BUILD_WORKBENCH);
    public readonly Achievement KILL_ENEMY = new Achievement(BUILD_SWORD);
    public readonly Achievement KILL_COW = new Achievement(BUILD_SWORD);
    public readonly Achievement FLY_PIG = new Achievement(KILL_COW);
    public readonly Achievement SNIPE_SKELETON = new Achievement(KILL_ENEMY);
    public readonly Achievement GET_DIAMONDS = new Achievement(ACQUIRE_IRON);
    public readonly Achievement NETHER_PORTAL = new Achievement(GET_DIAMONDS);
    public readonly Achievement GHAST_RETURN = new Achievement(NETHER_PORTAL);
    public readonly Achievement GET_BLAZE_ROD = new Achievement(NETHER_PORTAL);
    public readonly Achievement BREW_POTION = new Achievement(GET_BLAZE_ROD);
    public readonly Achievement END_PORTAL = new Achievement(GET_BLAZE_ROD);
    public readonly Achievement THE_END = new Achievement(END_PORTAL);
    public readonly Achievement ENCHANTMENTS = new Achievement(GET_DIAMONDS);
    public readonly Achievement OVERKILL = new Achievement(ENCHANTMENTS);
    public readonly Achievement BOOKCASE = new Achievement(ENCHANTMENTS);
    public readonly Achievement EXPLORE_ALL_BIOMES = new Achievement(END_PORTAL);
    public readonly Achievement SPAWN_WITHER = new Achievement(THE_END);
    public readonly Achievement KILL_WITHER = new Achievement(SPAWN_WITHER);
    public readonly Achievement FULL_BEACON = new Achievement(KILL_WITHER);
    public readonly Achievement BREED_COW = new Achievement(KILL_COW);
    public readonly Achievement DIAMONDS_TO_YOU = new Achievement(GET_DIAMONDS);
    public readonly Achievement OVERPOWERED = new Achievement(BUILD_BETTER_PICKAXE);

    private readonly Achievement parent;

    private Achievement() {
        parent = null;
    }

    private Achievement(Achievement parent) {
        this.parent = parent;
    }

    /**
     * Returns whether or not this achievement has a parent achievement.
     * 
     * @return whether the achievement has a parent achievement
     */
    public bool hasParent() {
        return parent != null;
    }

    /**
     * Returns the parent achievement of this achievement, or null if none.
     * 
     * @return the parent achievement or null
     */
    public Achievement getParent() {
        return parent;
    }
}
