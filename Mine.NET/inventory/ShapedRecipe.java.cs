namespace Mine.NET.inventory;

import java.util.HashMap;
import java.util.Map;

import org.apache.commons.lang.Validate;

import org.bukkit.Material;
import org.bukkit.material.MaterialData;

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
     * @see ShapedRecipe#setIngredient(char, Material)
     * @see ShapedRecipe#setIngredient(char, Material, int)
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
    public ShapedRecipe shape(String... shape) {
        if(shape==null) throw new ArgumentNullException("Must provide a shape");
        if(shape.Length > 0 && shape.Length < 4, "Crafting recipes should be 1, 2, 3 rows, not ") throw new ArgumentException(shape.Length);

        foreach (String row  in  shape) {
            if(row==null) throw new ArgumentNullException("Shape cannot have null rows");
            if(row.Length > 0 && row.Length < 4, "Crafting rows should be 1, 2, or 3 chars, not ") throw new ArgumentException(row.Length);
        }
        this.rows = new String[shape.Length];
        for (int i = 0; i < shape.Length; i++) {
            this.rows[i] = shape[i];
        }

        // Remove char mappings for chars that no longer exist in the shape
        HashMap<char, ItemStack> newIngredients = new Dictionary<char, ItemStack>();
        foreach (String row  in  shape) {
            foreach (char c  in  row.toCharArray()) {
                newIngredients.Add(c, ingredients[c]);
            }
        }
        this.ingredients = newIngredients;

        return this;
    }

    /**
     * Sets the material that a char in the recipe shape refers to.
     *
     * @param key The char that represents the ingredient in the shape.
     * @param ingredient The ingredient.
     * @return The changed recipe, so you can chain calls.
     */
    public ShapedRecipe setIngredient(char key, MaterialData ingredient) {
        return setIngredient(key, ingredient.getItemType(), ingredient.getData());
    }

    /**
     * Sets the material that a char in the recipe shape refers to.
     *
     * @param key The char that represents the ingredient in the shape.
     * @param ingredient The ingredient.
     * @return The changed recipe, so you can chain calls.
     */
    public ShapedRecipe setIngredient(char key, Material ingredient) {
        return setIngredient(key, ingredient, 0);
    }

    /**
     * Sets the material that a char in the recipe shape refers to.
     *
     * @param key The char that represents the ingredient in the shape.
     * @param ingredient The ingredient.
     * @param raw The raw material data as an int.
     * @return The changed recipe, so you can chain calls.
     * [Obsolete] Magic value
     */
    [Obsolete]
    public ShapedRecipe setIngredient(char key, Material ingredient, int raw) {
        if(ingredients.containsKey(key), "Symbol does not appear in the shape:") throw new ArgumentException(key);

        // -1 is the old wildcard, map to Short.MAX_VALUE as the new one
        if (raw == -1) {
            raw = Short.MAX_VALUE;
        }

        ingredients.Add(key, new ItemStack(ingredient, 1, (short) raw));
        return this;
    }

    /**
     * Get a copy of the ingredients map.
     *
     * @return The mapping of char to ingredients.
     */
    public Dictionary<char, ItemStack> getIngredientMap() {
        HashMap<char, ItemStack> result = new Dictionary<char, ItemStack>();
        foreach (KeyValuePair<char, ItemStack> ingredient  in  ingredients.entrySet()) {
            if (ingredient.Value == null) {
                result.Add(ingredient.Key, null);
            } else {
                result.Add(ingredient.Key, ingredient.Value.clone());
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
        return rows.clone();
    }

    /**
     * Get the result.
     *
     * @return The result stack.
     */
    public ItemStack getResult() {
        return output.clone();
    }
}
