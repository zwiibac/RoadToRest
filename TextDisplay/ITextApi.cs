using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDisplay
{
    public interface ITextApi
    {
        void AppendLine(string line);
        string[] Content { get; }
        string Status { set; }
        void Clear();
        string GetLine(int lineNumber);
        void SetLine(int lineNumber, string line);
    }
}
