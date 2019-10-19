using System.Linq;
using System.Runtime.InteropServices;

namespace RomTool
{
    public interface IBlock
    {
        uint Size { get; }
    }
}