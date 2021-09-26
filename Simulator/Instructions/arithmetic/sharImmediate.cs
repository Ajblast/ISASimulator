using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
	public class sharImmediate : Instruction
	{
		public sharImmediate(Register dest, Register op1, ushort imm, Register flag)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

