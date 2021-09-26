using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
	public class andImmediate : Instruction
	{
		public andImmediate(Register dest, Register op1, ushort imm, Register flag)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

