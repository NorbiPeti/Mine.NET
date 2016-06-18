using SharpDiff.FileStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildTools
{
    public class UnifiedDiffHeader : Header
    {
        public UnifiedDiffHeader(bool newfile, bool deletion) : base(null, null)
        {
            IsNewFile = newfile;
            IsDeletion = deletion;
        }

        public override bool IsNewFile { get; protected set; }

        public override bool IsDeletion { get; protected set; }
    }
}
