using dnlib.DotNet;
using dnlib.DotNet.Emit;
using DInstaller.Protection.Arithmetic.Utils;

namespace DInstaller.Protection.Arithmetic.Functions
{
    public class Mul : Function
    {
        public virtual ArithmeticTypes ArithmeticTypes => ArithmeticTypes.Mul;

        public override ArithmeticVt Arithmetic(Instruction instruction, ModuleDef module)
        {
            if (!ArithmeticUtils.CheckArithmetic(instruction)) return null!;
            var arithmeticEmulator = new ArithmeticEmulator(instruction.GetLdcI4Value(), ArithmeticUtils.GetY(instruction.GetLdcI4Value()), ArithmeticTypes);
            return new ArithmeticVt(new Value(arithmeticEmulator.GetValue(), arithmeticEmulator.GetY()), new Token(OpCodes.Mul), ArithmeticTypes);
        }
    }
}