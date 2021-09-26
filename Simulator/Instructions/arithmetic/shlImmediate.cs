using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
	public class shlImmediate : Instruction
	{
		public shlImmediate(Register dest, Register op1, ushort imm, Register flag)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

