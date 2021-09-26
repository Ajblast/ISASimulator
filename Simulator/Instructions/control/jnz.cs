using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.control
{
	public class jnz : Instruction
	{
		public jnz(Register flag, Register pc, Register rE, Register rF)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

