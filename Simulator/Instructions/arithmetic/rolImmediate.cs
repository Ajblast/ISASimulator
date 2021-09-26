using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
	public class rolImmediate : Instruction
	{
		public rolImmediate(Register dest, Register op1, ushort imm, Register flag)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

