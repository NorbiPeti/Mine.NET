using Mine.NET.material;
using System;

namespace Mine.NET.inventory
{
    /**
     * Represents a smelting recipe.
     */
    public class FurnaceRecipe : Recipe
    {
        private ItemStack output;
        private ItemStack ingredient;
        private float experience;

        /**
         * Create a furnace recipe to craft the specified ItemStack.
         *
         * @param result The item you want the recipe to create.
         * @param source The input Materials.
         */
        public FurnaceRecipe(ItemStack result, Materials source) :
            this(result, source, 0, 0)
        {
        }

        /**
         * Create a furnace recipe to craft the specified ItemStack.
         *
         * @param result The item you want the recipe to create.
         * @param source The input Materials.
         */
        public FurnaceRecipe(ItemStack result, MaterialData source) :
            this(result, source.getItemType(), source.getData(), 0)
        {
        }

        /**
         * Create a furnace recipe to craft the specified ItemStack.
         *
         * @param result The item you want the recipe to create.
         * @param source The input Materials.
         * @param experience The experience given by this recipe
         */
        public FurnaceRecipe(ItemStack result, MaterialData source, float experience) :
            this(result, source.getItemType(), source.getData(), experience)
        {
        } //TODO

        /**
         * Create a furnace recipe to craft the specified ItemStack.
         *
         * @param result The item you want the recipe to create.
         * @param source The input Materials.
         * @param data The data value. (Note: This is currently ignored by the
         *     CraftBukkit server.)
         * [Obsolete] Magic value
         */
        [Obsolete]
        public FurnaceRecipe(ItemStack result, Materials source, int data) :
            this(result, source, data, 0)
        {
        }

        /**
         * Create a furnace recipe to craft the specified ItemStack.
         *
         * @param result The item you want the recipe to create.
         * @param source The input Materials.
         * @param data The data value. (Note: This is currently ignored by the
         *     CraftBukkit server.)
         * @param experience The experience given by this recipe
         * [Obsolete] Magic value
         */
        [Obsolete]
        public FurnaceRecipe(ItemStack result, Materials source, int data, float experience)
        {
            this.output = new ItemStack(result);
            this.ingredient = new ItemStack(source, 1, (short)data);
            this.experience = experience;
        }

        /**
         * Sets the input of this furnace recipe.
         *
         * @param input The input Materials.
         * @return The changed recipe, so you can chain calls.
         */
        public FurnaceRecipe setInput(MaterialData input)
        {
            return setInput(input.getItemType(), input.getData());
        }

        /**
         * Sets the input of this furnace recipe.
         *
         * @param input The input Materials.
         * @return The changed recipe, so you can chain calls.
         */
        public FurnaceRecipe setInput(Materials input)
        {
            return setInput(input, 0);
        }

        /**
         * Sets the input of this furnace recipe.
         *
         * @param input The input Materials.
         * @param data The data value. (Note: This is currently ignored by the
         *     CraftBukkit server.)
         * @return The changed recipe, so you can chain calls.
         * [Obsolete] Magic value
         */
        [Obsolete]
        public FurnaceRecipe setInput(Materials input, int data)
        {
            this.ingredient = new ItemStack(input, 1, (short)data);
            return this;
        }

        /**
         * Get the input Materials.
         *
         * @return The input Materials.
         */
        public ItemStack getInput()
        {
            return this.ingredient.clone();
        }

        /**
         * Get the result of this recipe.
         *
         * @return The resulting stack.
         */
        public ItemStack getResult()
        {
            return output.clone();
        }

        /**
         * Sets the experience given by this recipe.
         *
         * @param experience the experience level
         */
        public void setExperience(float experience)
        {
            this.experience = experience;
        }

        /**
         * Get the experience given by this recipe.
         *
         * @return experience level
         */
        public float getExperience()
        {
            return experience;
        }
    }
}
