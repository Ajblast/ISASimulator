using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
	public class orImmediate : Instruction
	{
		public orImmediate(Register dest, Register op1, ushort imm, Register flag, ALU alu)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

