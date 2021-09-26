using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.storage
{
	public class loadImmediate : Instruction
	{
		public loadImmediate(Memory memory, Register dest, uint imm)
		{

		}

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

