using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
    public class xorImmediate : Instruction
    {
        public xorImmediate(Register dest, Register op1, ushort imm, Register flag)
        {

        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

