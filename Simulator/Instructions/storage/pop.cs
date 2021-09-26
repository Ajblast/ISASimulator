using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class pop : Instruction
	{
		public pop(Memory memory, Register dest, Register sp1, Register sp2)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

