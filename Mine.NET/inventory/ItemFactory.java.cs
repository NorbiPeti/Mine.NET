using Mine.NET.inventory.meta;

namespace Mine.NET.inventory
{
    /**
     * An instance of the ItemFactory can be obtained with {@link
     * Server#getItemFactory()}.
     * <p>
     * The ItemFactory is solely responsible for creating item meta containers to
     * apply on item stacks.
     */
    public interface ItemFactory {

        /**
         * This creates a new item meta for the Materials.
         *
         * @param Materials The Materials to consider as base for the meta
         * @return a new ItemMeta that could be applied to an item stack of the
         *     specified Materials
         */
        ItemMeta getItemMeta(Materials Materials);

        /**
         * This method checks the item meta to confirm that it is applicable (no
         * data lost if applied) to the specified ItemStack.
         * <p>
         * A {@link SkullMeta} would not be valid for a sword, but a normal {@link
         * ItemMeta} from an enchanted dirt block would.
         *
         * @param meta Meta to check
         * @param stack Item that meta will be applied to
         * @return true if the meta can be applied without losing data, false
         *     otherwise
         * @throws ArgumentException if the meta was not created by this
         *     factory
         */
        bool isApplicable(ItemMeta meta, ItemStack stack);

        /**
         * This method checks the item meta to confirm that it is applicable (no
         * data lost if applied) to the specified Materials.
         * <p>
         * A {@link SkullMeta} would not be valid for a sword, but a normal {@link
         * ItemMeta} from an enchanted dirt block would.
         *
         * @param meta Meta to check
         * @param Materials Materials that meta will be applied to
         * @return true if the meta can be applied without losing data, false
         *     otherwise
         * @throws ArgumentException if the meta was not created by this
         *     factory
         */
        bool isApplicable(ItemMeta meta, Materials Materials);

        /**
         * This method is used to compare two item meta data objects.
         *
         * @param meta1 First meta to compare, and may be null to indicate no data
         * @param meta2 Second meta to compare, and may be null to indicate no
         *     data
         * @return false if one of the meta has data the other does not, otherwise
         *     true
         * @throws ArgumentException if either meta was not created by this
         *     factory
         */
        bool equals(ItemMeta meta1, ItemMeta meta2);

        /**
         * Returns an appropriate item meta for the specified stack.
         * <p>
         * The item meta returned will always be a valid meta for a given
         * ItemStack of the specified Materials. It may be a more or less specific
         * meta, and could also be the same meta or meta type as the parameter.
         * The item meta returned will also always be the most appropriate meta.
         * <p>
         * Example, if a {@link SkullMeta} is being applied to a book, this method
         * would return a {@link BookMeta} containing all information in the
         * specified meta that is applicable to an {@link ItemMeta}, the highest
         * common interface.
         *
         * @param meta the meta to convert
         * @param stack the stack to convert the meta for
         * @return An appropriate item meta for the specified item stack. No
         *     guarantees are made as to if a copy is returned. This will be null
         *     for a stack of air.
         * @throws ArgumentException if the specified meta was not created
         *     by this factory
         */
        ItemMeta asMetaFor(ItemMeta meta, ItemStack stack);

        /**
         * Returns an appropriate item meta for the specified Materials.
         * <p>
         * The item meta returned will always be a valid meta for a given
         * ItemStack of the specified Materials. It may be a more or less specific
         * meta, and could also be the same meta or meta type as the parameter.
         * The item meta returned will also always be the most appropriate meta.
         * <p>
         * Example, if a {@link SkullMeta} is being applied to a book, this method
         * would return a {@link BookMeta} containing all information in the
         * specified meta that is applicable to an {@link ItemMeta}, the highest
         * common interface.
         *
         * @param meta the meta to convert
         * @param Materials the Materials to convert the meta for
         * @return An appropriate item meta for the specified item Materials. No
         *     guarantees are made as to if a copy is returned. This will be null for air.
         * @throws ArgumentException if the specified meta was not created
         *     by this factory
         */
        ItemMeta asMetaFor(ItemMeta meta, Materials Materials);

        /**
         * Returns the default color for all leather armor.
         *
         * @return the default color for leather armor
         */
        Color getDefaultLeatherColor();
    }
}
