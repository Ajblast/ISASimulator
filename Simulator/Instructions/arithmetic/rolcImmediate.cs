using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
	public class rolcImmediate : Instruction
	{
		public rolcImmediate(Register dest, Register op1, ushort imm, Register flag)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

