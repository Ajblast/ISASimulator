using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
    public class negImmediate : Instruction
    {
        public negImmediate(Register dest, ushort imm, Register flag, ALU alu)
        {

        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

