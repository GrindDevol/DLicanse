using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DInstaller.Protection.Arithmetic
{
    public abstract class Function
    {
        public abstract ArithmeticVt Arithmetic(Instruction instruction, ModuleDef module);
    }
}