namespace Mine.NET.Event.block
/**
 * Called when a piston :
 */
public class BlockPistonExtendEvent : BlockPistonEvent {
    private static readonly HandlerList handlers = new HandlerList();
    private readonly int length;
    private List<Block> blocks;

    [Obsolete]
    public BlockPistonExtendEvent(Block block, readonly int length, readonly BlockFace direction) {
        base(block, direction);

        this.Length = length;
    }

    public BlockPistonExtendEvent(Block block, readonly List<Block> blocks, readonly BlockFace direction) {
        base(block, direction);

        this.Length = blocks.Count;
        this.blocks = blocks;
    }

    /**
     * Get the amount of blocks which will be moved while extending.
     *
     * @return the amount of moving blocks
     * [Obsolete] slime blocks make the value of this method
     *          inaccurate due to blocks being pushed at the side
     */
    [Obsolete]
    public int getLength {
        return this.Length;
    }

    /**
     * Get an immutable list of the blocks which will be moved by the
     * extending.
     *
     * @return Immutable list of the moved blocks.
     */
    public List<Block> getBlocks() {
        if (blocks == null) {
            List<Block> tmp = new List<Block>();
            for (int i = 0; i < this.getLength; i++) {
                tmp.add(block.getRelative(getDirection(), i + 1));
            }
            blocks = Collections.unmodifiableList(tmp);
        }
        return blocks;
    }

    public override HandlerList getHandlers() {
        return handlers;
    }

    public static HandlerList getHandlerList() {
        return handlers;
    }
}
