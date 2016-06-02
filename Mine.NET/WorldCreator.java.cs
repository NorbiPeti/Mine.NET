using Mine.NET;
using Mine.NET.command;
using Mine.NET.generator;
using Mine.NET.plugin;
using Mine.NET.util;
using System;

namespace Mine.NET
{
    /**
    * Represents various types of options that may be used to create a world.
*/
    public class WorldCreator
    {
        private readonly String name;
        private long seed;
        private WorldEnvironment environment = WorldEnvironment.NORMAL;
        private ChunkGenerator generator = null;
        private WorldTypes type = WorldTypes.NORMAL;
        private bool generateStructures = true;
        private String generatorSettings = "";

        /**
         * Creates an empty WorldCreationOptions for the given world name
         *
         * @param name Name of the world that will be created
         */
        public WorldCreator(String name)
        {
            if (name == null)
            {
                throw new ArgumentException("World name cannot be null");
            }

            this.name = name;
            this.seed = new JavaRand().NextLong();
        }

        /**
         * Copies the options from the specified world
         *
         * @param world World to copy options from
         * @return This object, for chaining
         */
        public WorldCreator copy(World world)
        {
            if (world == null)
            {
                throw new ArgumentException("World cannot be null");
            }

            seed = world.getSeed();
            environment = world.getEnvironment();
            generator = world.getGenerator();

            return this;
        }

        /**
         * Copies the options from the specified {@link WorldCreator}
         *
         * @param creator World creator to copy options from
         * @return This object, for chaining
         */
        public WorldCreator copy(WorldCreator creator)
        {
            if (creator == null)
            {
                throw new ArgumentException("Creator cannot be null");
            }

            seed = creator.Seed;
            environment = creator.Environment;
            generator = creator.Generator;

            return this;
        }

        /**
         * Gets the name of the world that is to be loaded or created.
         *
         * @return World name
         */
        public String Name { get; private set; }

        /**
         * Gets the seed that will be used to create this world
         *
         * @return World seed
         */
        public long Seed { get; set; }

        /**
         * Gets the environment that will be used to create or load the world
         *
         * @return World environment
         */
        public WorldEnvironment Environment { get; set; }

        /**
         * Gets the type of the world that will be created or loaded
         *
         * @return World type
         */
        public WorldTypes Type { get; set; }

        /**
         * Gets the generator that will be used to create or load the world.
         * <p>
         * This may be null, in which case the "natural" generator for this
         * environment will be used.
         *
         * @return Chunk generator
         */
        public ChunkGenerator Generator { get; set; }

        /**
         * Sets the generator that will be used to create or load the world.
         * <p>
         * This may be null, in which case the "natural" generator for this
         * environment will be used.
         * <p>
         * If the generator cannot be found for the given name, the natural
         * environment generator will be used instead and a warning will be
         * printed to the console.
         *
         * @param generator Name of the generator to use, in "plugin:id" notation
         * @return This object, for chaining
         */
        public WorldCreator SetGenerator(String generator)
        {
            this.Generator = getGeneratorForName(name, generator, Bukkit.getConsoleSender());

            return this;
        }

        /**
         * Sets the generator that will be used to create or load the world.
         * <p>
         * This may be null, in which case the "natural" generator for this
         * environment will be used.
         * <p>
         * If the generator cannot be found for the given name, the natural
         * environment generator will be used instead and a warning will be
         * printed to the specified output
         *
         * @param generator Name of the generator to use, in "plugin:id" notation
         * @param output {@link CommandSender} that will receive any error
         *     messages
         * @return This object, for chaining
         */
        public WorldCreator SetGenerator(String generator, CommandSender output)
        {
            this.generator = getGeneratorForName(name, generator, output);

            return this;
        }

        /**
         * Sets the generator settings of the world that will be created or loaded
         *
         * @param generatorSettings The settings that should be used by the generator
         * @return This object, for chaining
         */
        public string GeneratorSettings { get; set; }

        /**
         * Sets whether or not worlds created or loaded with this creator will
         * have structures.
         *
         * @param generate Whether to generate structures
         * @return This object, for chaining
         */
        public bool GenerateStructures { get; set; }

        /**
         * Creates a world with the specified options.
         * <p>
         * If the world already exists, it will be loaded from disk and some
         * options may be ignored.
         *
         * @return Newly created or loaded world
         */
        public World createWorld()
        {
            return Bukkit.createWorld(this);
        }

        /**
         * Creates a new {@link WorldCreator} for the given world name
         *
         * @param name Name of the world to load or create
         * @return Resulting WorldCreator
         */
        public static WorldCreator FromName(String name)
        {
            return new WorldCreator(name);
        }

        /**
         * Attempts to get the {@link ChunkGenerator} with the given name.
         * <p>
         * If the generator is not found, null will be returned and a message will
         * be printed to the specified {@link CommandSender} explaining why.
         * <p>
         * The name must be in the "plugin:id" notation, or optionally just
         * "plugin", where "plugin" is the safe-name of a plugin and "id" is an
         * optional unique identifier for the generator you wish to request from
         * the plugin.
         *
         * @param world Name of the world this will be used for
         * @param name Name of the generator to retrieve
         * @param output Where to output if errors are present
         * @return Resulting generator, or null
         */
        public static ChunkGenerator getGeneratorForName(String world, String name, CommandSender output)
        {
            ChunkGenerator result = null;

            if (world == null)
            {
                throw new ArgumentException("World name must be specified");
            }

            if (output == null)
            {
                output = Bukkit.getConsoleSender();
            }

            if (name != null)
            {
                String[] split = name.Split(new char[] { ':' }, 2);
                String id = (split.Length > 1) ? split[1] : null;
                Plugin plugin = Bukkit.getPluginManager().getPlugin(split[0]);

                if (plugin == null)
                {
                    output.sendMessage("Could not set generator for world '" + world + "': Plugin '" + split[0] + "' does not exist");
                }
                else if (!plugin.Enabled)
                {
                    output.sendMessage("Could not set generator for world '" + world + "': Plugin '" + plugin.FullName + "' is not enabled");
                }
                else
                {
                    result = plugin.getDefaultWorldGenerator(world, id);
                }
            }

            return result;
        }
    }
}
