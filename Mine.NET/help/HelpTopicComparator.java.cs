using System;
using System.Collections.Generic;

namespace Mine.NET
{
    /**
     * Used to impose a custom total ordering on help topics.
     * <p>
     * All topics are listed in alphabetic order, but topics that start with a
     * slash come after topics that don't.
     */
    public class HelpTopicComparator : IComparer<HelpTopic>
    {

        // Singleton implementations
        private static readonly TopicNameComparator tnc = new TopicNameComparator();
        public static TopicNameComparator topicNameComparatorInstance()
        {
            return tnc;
        }

        private static readonly HelpTopicComparator htc = new HelpTopicComparator();
        public static HelpTopicComparator helpTopicComparatorInstance()
        {
            return htc;
        }

        private HelpTopicComparator() { }

        public int Compare(HelpTopic lhs, HelpTopic rhs)
        {
            return tnc.Compare(lhs.getName(), rhs.getName());
        }

        public class TopicNameComparator : IComparer<String>
        {
            internal TopicNameComparator() { }

            public int Compare(String lhs, String rhs)
            {
                bool lhsStartSlash = lhs.StartsWith("/");
                bool rhsStartSlash = rhs.StartsWith("/");

                if (lhsStartSlash && !rhsStartSlash)
                {
                    return 1;
                }
                else if (!lhsStartSlash && rhsStartSlash)
                {
                    return -1;
                }
                else
                {
                    return lhs.ToLower().CompareTo(rhs.ToLower());
                }
            }
        }
    }
}
