using System;
using System.Collections.Generic;

namespace Mine.NET
{
    /**
     * This designates the warning state for a specific item.
     * <p>
     * When the server settings dictate 'default' warnings, warnings are printed
     * if the {@link #value()} is true.
     */
    //@Target({ElementType.CONSTRUCTOR, ElementType.METHOD, ElementType.TYPE})
    //@Retention(RetentionPolicy.RUNTIME)
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Struct)]
    public class Warning : System.Attribute
    {
        private static readonly Dictionary<String, WarningState> values = new Dictionary<string, WarningState>
    {
        { "off", WarningState.OFF },
        { "false", WarningState.OFF },
        { "f", WarningState.OFF },
        { "no", WarningState.OFF },
        { "n", WarningState.OFF },
        { "on", WarningState.ON },
        { "true", WarningState.ON },
        { "t", WarningState.ON },
        { "yes", WarningState.ON },
        { "y", WarningState.ON },
        { "", WarningState.DEFAULT },
        { "d", WarningState.DEFAULT },
        { "default", WarningState.DEFAULT },
    };

        /**
         * This method checks the provided warning should be printed for this
         * state
         *
         * @param warning The warning annotation added to a deprecated item
         * @return <ul>
         *     <li>WarningState.ON is always True
         *     <li>WarningState.OFF is always false
         *     <li>WarningState.DEFAULT is false if and only if annotation is not null and
         *     specifies false for {@link Warning#value()}, true otherwise.
         *     </ul>
         */
        public static bool printFor(WarningState state, Warning warning)
        {
            if (state == WarningState.DEFAULT)
            {
                return warning == null || warning.value;
            }
            return state == WarningState.ON;
        }

        /**
         * This method returns the corresponding warning state for the given
         * string value.
         *
         * @param value The string value to check
         * @return {@link #WarningState.DEFAULT} if not found, or the respective
         *     WarningState
         */
        public static WarningState FromValue(String value)
        {
            if (value == null)
            {
                return WarningState.DEFAULT;
            }
            WarningState state;
            if (!values.TryGetValue(value, out state))
            {
                return WarningState.DEFAULT;
            }
            return state;
        }

        /**
         * This sets if the deprecation warnings when registering events gets
         * printed when the setting is in the default state.
         *
         * @return false normally, or true to encourage warning printout
         */
        bool value { get; set; } = false;

        /**
         * This can provide detailed information on why the event is deprecated.
         *
         * @return The reason an event is deprecated
         */
        String reason { get; set; } = "";
    }

    /**
     * This represents the states that server verbose for warnings may be.
     */
    public enum WarningState
    {

        /**
         * Indicates all warnings should be printed for deprecated items.
         */
        ON,
        /**
         * Indicates no warnings should be printed for deprecated items.
         */
        OFF,
        /**
         * Indicates each warning would default to the configured {@link
         * Warning} annotation, or always if annotation not found.
         */
        DEFAULT
    }
}