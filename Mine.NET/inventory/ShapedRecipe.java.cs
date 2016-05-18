using Mine.NET.material;
using System;
using System.Collections.Generic;

namespace Mine.NET.inventory
{
    /**
     * Represents a shaped (ie normal) crafting recipe.
     */
    public class ShapedRecipe : Recipe {
        private ItemStack output;
        private String[] rows;
        private Dictionary<char, ItemStack> ingredients = new Dictionary<char, ItemStack>();

        /**
         * Create a shaped recipe to craft the specified ItemStack. The
         * constructor merely determines the result and type; to set the actual
         * recipe, you'll need to call the appropriate methods.
         *
         * @param result The item you want the recipe to create.
         * @see ShapedRecipe#shape(String...)
         * @see ShapedRecipe#setIngredient(char, Materials)
         * @see ShapedRecipe#setIngredient(char, Materials, int)
         * @see ShapedRecipe#setIngredient(char, MaterialData)
         */
        public ShapedRecipe(ItemStack result) {
            this.output = new ItemStack(result);
        }

        /**
         * Set the shape of this recipe to the specified rows. Each char
         * represents a different ingredient; exactly what each char
         * represents is set separately. The first row supplied corresponds with
         * the upper most part of the recipe on the workbench e.g. if all three
         * rows are supplies the first string represents the top row on the
         * workbench.
         *
         * @param shape The rows of the recipe (up to 3 rows).
         * @return The changed recipe, so you can chain calls.
         */
        public ShapedRecipe shape(params String[] shape) {
            if (shape == null) throw new ArgumentNullException("Must provide a shape");
            if (shape.Length > 0 && shape.Length < 4) throw new ArgumentException("Crafting recipes should be 1, 2, 3 rows, not ", nameof(shape.Length));

            foreach (String row in shape) {
                if (row == null) throw new ArgumentNullException("Shape cannot have null rows");
                if (row.Length > 0 && row.Length < 4) throw new ArgumentException("Crafting rows should be 1, 2, or 3 chars, not ", nameof(row.Length));
            }
            this.rows = new String[shape.Length];
            for (int i = 0; i < shape.Length; i++) {
                this.rows[i] = shape[i];
            }

            // Remove char mappings for chars that no longer exist in the shape
            Dictionary<char, ItemStack> newIngredients = new Dictionary<char, ItemStack>();
            foreach (String row in shape) {
                foreach (char c in row.ToCharArray()) {
                    newIngredients.Add(c, ingredients[c]);
                }
            }
            this.ingredients = newIngredients;

            return this;
        }

        /**
         * Sets the Materials that a char in the recipe shape refers to.
         *
         * @param key The char that represents the ingredient in the shape.
         * @param ingredient The ingredient.
         * @return The changed recipe, so you can chain calls.
         */
        public ShapedRecipe setIngredient(char key, MaterialData ingredient) {
            return setIngredient(key, ingredient.getItemType(), ingredient.getData());
        }

        /**
         * Sets the Materials that a char in the recipe shape refers to.
         *
         * @param key The char that represents the ingredient in the shape.
         * @param ingredient The ingredient.
         * @return The changed recipe, so you can chain calls.
         */
        public ShapedRecipe setIngredient(char key, Materials ingredient)
        {
            if (ingredients.ContainsKey(key)) throw new ArgumentException("Symbol does not appear in the shape:", nameof(key));

            ingredients.Add(key, new ItemStack(ingredient, 1));
            return this;
        }

        /**
         * Get a copy of the ingredients map.
         *
         * @return The mapping of char to ingredients.
         */
        public Dictionary<char, ItemStack> getIngredientMap() {
            Dictionary<char, ItemStack> result = new Dictionary<char, ItemStack>();
            foreach (KeyValuePair<char, ItemStack> ingredient in ingredients) {
                if (ingredient.Value == null) {
                    result.Add(ingredient.Key, null);
                } else {
                    result.Add(ingredient.Key, (ItemStack)ingredient.Value.Clone());
                }
            }
            return result;
        }

        /**
         * Get the shape.
         *
         * @return The recipe's shape.
         */
        public String[] getShape() {
            string[] ret = new string[rows.Length];
            Array.Copy(rows, ret, rows.Length);
            return ret;
        }

        /**
         * Get the result.
         *
         * @return The result stack.
         */
        public ItemStack getResult() {
            return (ItemStack)output.Clone();
        }
    }
}
