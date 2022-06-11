using dnlib.DotNet.Emit;
using System.Collections.Generic;

namespace DInstaller.Protection.CtrlFlow
{
    public class Block
    {
        public Block()
        {
            Instructions = new List<Instruction>();
        }

        public List<Instruction> Instructions { get; set; }

        public int Number { get; set; }
    }
}