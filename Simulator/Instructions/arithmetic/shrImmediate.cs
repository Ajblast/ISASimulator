using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
	public class shrImmediate : Instruction
	{
		public shrImmediate(Register dest, Register op1, ushort imm, Register flag)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

