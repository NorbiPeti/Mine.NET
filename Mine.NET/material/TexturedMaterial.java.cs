using System;
using System.Collections.Generic;

namespace Mine.NET.material
{

    /**
     * Represents textured materials like steps and smooth bricks
     */
    public abstract class TexturedMaterial : MaterialData
    {

        public TexturedMaterial(Materials m) : base(m)
        {
        }

        /**
         * Retrieve a list of possible textures. The first element of the list
         * will be used as a default.
         *
         * @return a list of possible textures for this block
         */
        public abstract List<Materials> getTextures();

        private Materials texture;
        /**
         * Gets the current Materials this block is made of
         *
         * @return Materials of this block
         */
        public Materials getMaterial()
        {
            return texture;
        }

        /**
         * Sets the Materials this block is made of
         *
         * @param Materials
         *            New Materials of this block
         */
        public void setMaterial(Materials material)
        {
            if (getTextures().Contains(material))
            {
                //setTextureIndex(getTextures().IndexOf(Materials));
                texture = material;
            }
            else
            {
                //setTextureIndex(0x0);
                texture = getTextures()[0];
            }
        }

        public override string ToString()
        {
            return getMaterial() + " " + base.ToString();
        }

        public new TexturedMaterial Clone() { return (TexturedMaterial)base.Clone(); }
    }
}
