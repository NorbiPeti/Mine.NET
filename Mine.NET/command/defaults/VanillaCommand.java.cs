using System;
using System.Collections.Generic;
using System.Text;

namespace Mine.NET.command.defaults
{
    //[Obsolete] //TODO: Obsolete?
    public abstract class VanillaCommand : Command {
        static readonly int MAX_COORD = 30000000;
        static readonly int MIN_COORD_MINUS_ONE = -30000001;
        static readonly int MIN_COORD = -30000000;

        protected VanillaCommand(String name) : base(name) {
        }

        protected VanillaCommand(String name, String description, String usageMessage, List<String> aliases) : base(name, description, usageMessage, aliases)
        {
        }

        public bool matches(String input) {
            return input.Equals(this.getName(), StringComparison.InvariantCultureIgnoreCase);
        }

        protected int getInteger(CommandSender sender, String value, int min) {
            return getInteger(sender, value, min, int.MaxValue);
        }

        int getInteger(CommandSender sender, String value, int min, int max) {
            return getInteger(sender, value, min, max, false);
        }

        int getInteger(CommandSender sender, String value, int min, int max, bool Throws) {
            int i = min;

            try {
                i = int.Parse(value);
            } catch (FormatException) {
                if (Throws) {
                    throw new FormatException(String.Format("{0} is not a valid number", value));
                }
            }

            if (i < min) {
                i = min;
            } else if (i > max) {
                i = max;
            }

            return i;
        }

        int getInteger(String value) {
            try {
                return int.Parse(value);
            } catch (FormatException) {
                return 0; //TODO: Replace null
            }
        }

        public static double getRelativeDouble(double original, CommandSender sender, String input) {
            if (input.StartsWith("~")) {
                double value = getDouble(sender, input.Substring(1));
                if (value == MIN_COORD_MINUS_ONE) {
                    return MIN_COORD_MINUS_ONE;
                }
                return original + value;
            } else {
                return getDouble(sender, input);
            }
        }

        public static double getDouble(CommandSender sender, String input) {
            try {
                return Double.Parse(input);
            } catch (FormatException) { //TODO: Change these to TryParse
                return MIN_COORD_MINUS_ONE;
            }
        }

        public static double getDouble(CommandSender sender, String input, double min, double max) {
            double result = getDouble(sender, input);

            // TODO: This should throw an exception instead.
            if (result < min) {
                throw new ArgumentException("The result is less than min.");
            } else if (result > max) {
                throw new ArgumentException("The result is more than max.");
            }

            return result;
        }

        String createString(String[] args, int start) {
            return createString(args, start, " ");
        }

        String createString(String[] args, int start, String glue) {
            StringBuilder string_ = new StringBuilder();

            for (int x = start; x < args.Length; x++) {
                string_.Append(args[x]);
                if (x != args.Length - 1) {
                    string_.Append(glue);
                }
            }

            return string_.ToString();
        }
    }
}
