using Mine.NET.material;
using System;
using System.Collections.Generic;

namespace Mine.NET.inventory
{
    /**
     * Represents a shapeless recipe, where the arrangement of the ingredients on
     * the crafting grid does not matter.
     */
    public class ShapelessRecipe : Recipe
    {
        private ItemStack output;
        private List<ItemStack> ingredients = new List<ItemStack>();

        /**
         * Create a shapeless recipe to craft the specified ItemStack. The
         * constructor merely determines the result and type; to set the actual
         * recipe, you'll need to call the appropriate methods.
         *
         * @param result The item you want the recipe to create.
         * @see ShapelessRecipe#addIngredient(Material)
         * @see ShapelessRecipe#addIngredient(MaterialData)
         * @see ShapelessRecipe#addIngredient(Material,int)
         * @see ShapelessRecipe#addIngredient(int,Material)
         * @see ShapelessRecipe#addIngredient(int,MaterialData)
         * @see ShapelessRecipe#addIngredient(int,Material,int)
         */
        public ShapelessRecipe(ItemStack result)
        {
            this.output = new ItemStack(result);
        }

        /**
         * Adds the specified ingredient.
         *
         * @param ingredient The ingredient to add.
         * @return The changed recipe, so you can chain calls.
         */
        public ShapelessRecipe addIngredient(MaterialData ingredient)
        {
            return addIngredient(1, ingredient);
        }

        /**
         * Adds the specified ingredient.
         *
         * @param ingredient The ingredient to add.
         * @return The changed recipe, so you can chain calls.
         */
        public ShapelessRecipe addIngredient(Material ingredient)
        {
            return addIngredient(1, ingredient);
        }

        /**
         * Adds multiples of the specified ingredient.
         *
         * @param count How many to add (can't be more than 9!)
         * @param ingredient The ingredient to add.
         * @return The changed recipe, so you can chain calls.
         */
        public ShapelessRecipe addIngredient(int count, MaterialData ingredient)
        {
            return addIngredient(count, ingredient.getItemType(), ingredient.getData());
        }

        /**
         * Adds multiples of the specified ingredient.
         *
         * @param count How many to add (can't be more than 9!)
         * @param ingredient The ingredient to add.
         * @return The changed recipe, so you can chain calls.
         */
        public ShapelessRecipe addIngredient(int count, Material ingredient)
        {
            if (ingredients.Count + count <= 9) throw new ArgumentException("Shapeless recipes cannot have more than 9 ingredients");

            while (count-- > 0)
            {
                ingredients.Add(new ItemStack(ingredient, 1));
            }
            return this;
        }

        /**
         * Removes an ingredient from the list. If the ingredient occurs multiple
         * times, only one instance of it is removed. Only removes exact matches,
         * with a data value of 0.
         *
         * @param ingredient The ingredient to remove
         * @return The changed recipe.
         */
        public ShapelessRecipe removeIngredient(Material ingredient)
        {
            return removeIngredient(1, ingredient); //TODO: Only remove with data value of 0
        }

        /**
         * Removes an ingredient from the list. If the ingredient occurs multiple
         * times, only one instance of it is removed. If the data value is -1,
         * only ingredients with a -1 data value will be removed.
         *
         * @param ingredient The ingredient to remove
         * @return The changed recipe.
         */
        public ShapelessRecipe removeIngredient(MaterialData ingredient)
        {
            return removeIngredient(ingredient.getItemType(), ingredient.getData());
        }

        /**
         * Removes multiple instances of an ingredient from the list. If there are
         * less instances then specified, all will be removed. Only removes exact
         * matches, with a data value of 0.
         *
         * @param count The number of copies to remove.
         * @param ingredient The ingredient to remove
         * @return The changed recipe.
         */
        public ShapelessRecipe removeIngredient(int count, Material ingredient)
        {
            IEnumerator<ItemStack> iterator = ingredients.GetEnumerator();
            ingredients.RemoveAll(i => i.getType() == ingredient && count-- > 0);
            return this;
        }

        /**
         * Removes multiple instances of an ingredient from the list. If there are
         * less instances then specified, all will be removed. If the data value
         * is -1, only ingredients with a -1 data value will be removed.
         *
         * @param count The number of copies to remove.
         * @param ingredient The ingredient to remove.
         * @return The changed recipe.
         */
        public ShapelessRecipe removeIngredient(int count, MaterialData ingredient)
        {
            return removeIngredient(count, ingredient.getItemType(), ingredient.getData());
        }

        /**
         * Get the result of this recipe.
         *
         * @return The result stack.
         */
        public ItemStack getResult()
        {
            return (ItemStack)output.Clone();
        }

        /**
         * Get the list of ingredients used for this recipe.
         *
         * @return The input list
         */
        public List<ItemStack> getIngredientList()
        {
            List<ItemStack> result = new List<ItemStack>(ingredients.Count);
            foreach (ItemStack ingredient in ingredients)
            {
                result.Add((ItemStack)ingredient.Clone());
            }
            return result;
        }
    }
}
