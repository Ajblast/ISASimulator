using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
	public class norRegister : Instruction
	{
		public norRegister(Register dest, Register op1, Register op2, Register flag, ALU alu)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

