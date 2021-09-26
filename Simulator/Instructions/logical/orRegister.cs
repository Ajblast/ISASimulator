using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
	public class orRegister : Instruction
	{
		public orRegister(Register dest, Register op1, Register op2, Register flag)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

