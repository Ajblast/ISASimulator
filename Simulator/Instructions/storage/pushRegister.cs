using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class pushRegister : Instruction
	{
		public pushRegister(Memory memory, Register src, Register sp1, Register sp2)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

