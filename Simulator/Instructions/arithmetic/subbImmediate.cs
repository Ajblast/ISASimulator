using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
	public class subbImmediate : Instruction
	{
		public subbImmediate(Register dest, Register op1, ushort imm, Register flag)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

