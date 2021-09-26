using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class pushImmediate : Instruction
	{
		public pushImmediate(Memory memory, ushort imm, Register sp1, Register sp2)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

