using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mine.NET.command.defaults
{
    public class HelpCommand : VanillaCommand
    {
        public HelpCommand() : base("help")
        {
            this.description = "Shows the help menu";
            this.usageMessage = "/help <pageNumber>\n/help <topic>\n/help <topic> <pageNumber>";
            this.setAliases(new List<string> { "?" });
            this.setPermission("bukkit.command.help");
        }

        public override bool execute(CommandSender sender, String currentAlias, String[] args)
        {
            if (!testPermission(sender)) return true;

            String command;
            int pageNumber;
            int pageHeight;
            int pageWidth;

            if (args.Length == 0)
            {
                command = "";
                pageNumber = 1;
            }
            else if (int.TryParse(args[args.Length - 1], out pageNumber))
            {
                command = args.Take(args.Length - 1).Aggregate((a, b) => a + " " + b);
                if (pageNumber <= 0)
                {
                    pageNumber = 1;
                }
            }
            else
            {
                command = args.Aggregate((a, b) => a + " " + b);
                pageNumber = 1;
            }

            if (sender is ConsoleCommandSender)
            {
                pageHeight = ChatPaginator.UNBOUNDED_PAGE_HEIGHT;
                pageWidth = ChatPaginator.UNBOUNDED_PAGE_WIDTH;
            }
            else
            {
                pageHeight = ChatPaginator.CLOSED_CHAT_PAGE_HEIGHT - 1;
                pageWidth = ChatPaginator.GUARANTEED_NO_WRAP_CHAT_PAGE_WIDTH;
            }

            HelpMap helpMap = Bukkit.getServer().getHelpMap();
            HelpTopic topic = helpMap.getHelpTopic(command);

            if (topic == null)
            {
                topic = helpMap.getHelpTopic("/" + command);
            }

            if (topic == null)
            {
                topic = findPossibleMatches(command);
            }

            if (topic == null || !topic.canSee(sender))
            {
                sender.sendMessage(ChatColors.RED + "No help for " + command);
                return true;
            }

            ChatPaginator.ChatPage page = ChatPaginator.paginate(topic.getFullText(sender), pageNumber, pageWidth, pageHeight);

            StringBuilder header = new StringBuilder();
            header.Append(ChatColors.YELLOW);
            header.Append("--------- ");
            header.Append(ChatColors.WHITE);
            header.Append("Help: ");
            header.Append(topic.getName());
            header.Append(" ");
            if (page.getTotalPages() > 1)
            {
                header.Append("(");
                header.Append(page.getPageNumber());
                header.Append("/");
                header.Append(page.getTotalPages());
                header.Append(") ");
            }
            header.Append(ChatColors.YELLOW);
            for (int i = header.Length; i < ChatPaginator.GUARANTEED_NO_WRAP_CHAT_PAGE_WIDTH; i++)
            {
                header.Append("-");
            }
            sender.sendMessage(header.ToString());

            sender.sendMessage(page.getLines());

            return true;
        }

        public override List<String> tabComplete(CommandSender sender, String alias, String[] args)
        {
            if (sender == null) throw new ArgumentNullException("Sender cannot be null");
            if (args == null) throw new ArgumentNullException("Arguments cannot be null");
            if (alias == null) throw new ArgumentNullException("Alias cannot be null");

            if (args.Length == 1)
            {
                List<String> matchedTopics = new List<String>();
                String searchString = args[0];
                foreach (HelpTopic topic in Bukkit.getServer().getHelpMap().getHelpTopics())
                {
                    String trimmedTopic = topic.getName().StartsWith("/") ? topic.getName().Substring(1) : topic.getName();

                    if (trimmedTopic.StartsWith(searchString))
                    {
                        matchedTopics.Add(trimmedTopic);
                    }
                }
                return matchedTopics;
            }
            return new List<string>();
        }

        protected HelpTopic findPossibleMatches(String searchString)
        {
            int maxDistance = (searchString.Length) / 5 + 3;
            SortedSet<HelpTopic> possibleMatches = new SortedSet<HelpTopic>(HelpTopicComparator.helpTopicComparatorInstance());

            if (searchString.StartsWith("/"))
            {
                searchString = searchString.Substring(1);
            }

            foreach (HelpTopic topic in Bukkit.getServer().getHelpMap().getHelpTopics())
            {
                String trimmedTopic = topic.getName().StartsWith("/") ? topic.getName().Substring(1) : topic.getName();

                if (trimmedTopic.Length < searchString.Length)
                {
                    continue;
                }
                //Find: "\.charAt\(([^\)]+)\)" - Replace: "[$1]"
                if (char.ToLower(trimmedTopic[0]) != char.ToLower(searchString[0]))
                {
                    continue;
                }

                if (damerauLevenshteinDistance(searchString, trimmedTopic.Substring(0, searchString.Length)) < maxDistance)
                {
                    possibleMatches.add(topic);
                }
            }

            if (possibleMatches.Count > 0)
            {
                return new IndexHelpTopic("Search", null, null, possibleMatches, "Search for: " + searchString);
            }
            else
            {
                return null;
            }
        }

        /**
         * Computes the Dameraur-Levenshtein Distance between two strings. Adapted
         * from the algorithm at <a href="http://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance">Wikipedia: Damerau–Levenshtein distance</a>
         *
         * @param s1 The first string being compared.
         * @param s2 The second string being compared.
         * @return The number of substitutions, deletions, insertions, and
         * transpositions required to get from s1 to s2.
         */
        protected static int damerauLevenshteinDistance(String s1, String s2)
        {
            if (s1 == null && s2 == null)
            {
                return 0;
            }
            if (s1 != null && s2 == null)
            {
                return s1.Length;
            }
            if (s1 == null && s2 != null)
            {
                return s2.Length;
            }

            int s1Len = s1.Length;
            int s2Len = s2.Length;
            int[,] H = new int[s1Len + 2, s2Len + 2];

            int INF = s1Len + s2Len;
            H[0, 0] = INF;
            for (int i = 0; i <= s1Len; i++)
            {
                H[i + 1, 1] = i;
                H[i + 1, 0] = INF;
            }
            for (int j = 0; j <= s2Len; j++)
            {
                H[1, j + 1] = j;
                H[0, j + 1] = INF;
            }

            Dictionary<char, int> sd = new Dictionary<char, int>();
            foreach (char Letter in (s1 + s2).ToCharArray())
            {
                if (!sd.ContainsKey(Letter))
                {
                    sd.Add(Letter, 0);
                }
            }

            for (int i = 1; i <= s1Len; i++)
            {
                int DB = 0;
                for (int j = 1; j <= s2Len; j++)
                {
                    int i1 = sd[s2[j - 1]]; //Find: "\.get\(([^\)]+)\)" - Replace: "[$1]"
                    int j1 = DB;

                    if (s1[i - 1] == s2[j - 1])
                    {
                        H[i + 1, j + 1] = H[i, j];
                        DB = j;
                    }
                    else
                    {
                        H[i + 1, j + 1] = Math.Min(H[i, j], Math.Min(H[i + 1, j], H[i, j + 1])) + 1;
                    }

                    H[i + 1, j + 1] = Math.Min(H[i + 1, j + 1], H[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1));
                }
                sd.Add(s1[i - 1], i);
            }

            return H[s1Len + 1, s2Len + 1];
        }
    }
}
