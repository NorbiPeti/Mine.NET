package org.bukkit.help;

import org.bukkit.help.HelpTopic;

import java.util.Comparator;

/**
 * Used to impose a custom total ordering on help topics.
 * <p>
 * All topics are listed in alphabetic order, but topics that start with a
 * slash come after topics that don't.
 */
public class HelpTopicComparator implements Comparator<HelpTopic> {
    
    // Singleton implementations
    private static readonly TopicNameComparator tnc = new TopicNameComparator();
    public static TopicNameComparator topicNameComparatorInstance() {
        return tnc;
    }
    
    private static readonly HelpTopicComparator htc = new HelpTopicComparator();
    public static HelpTopicComparator helpTopicComparatorInstance() {
        return htc;
    }
    
    private HelpTopicComparator() {}

    public int compare(HelpTopic lhs, HelpTopic rhs) {
        return tnc.compare(lhs.getName(), rhs.getName());
    }

    public static class TopicNameComparator implements Comparator<String> {
        private TopicNameComparator(){}
        
        public int compare(String lhs, String rhs) {
            bool lhsStartSlash = lhs.startsWith("/");
            bool rhsStartSlash = rhs.startsWith("/");
            
            if (lhsStartSlash && !rhsStartSlash) {
                return 1;
            } else if (!lhsStartSlash && rhsStartSlash) {
                return -1;
            } else {
                return lhs.compareToIgnoreCase(rhs);
            }
        }
    }
}
