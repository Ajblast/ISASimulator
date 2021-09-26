using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
	public class addImmediate : Instruction
	{
        private Register dest;
        private Register op1;
        private ushort op2;
        private Register flag;
        private ALU alu;
        
        public addImmediate(Register dest, Register op1, ushort imm, Register flag, ALU alu)
		{
            this.dest = dest;
            this.op1 = op1;
            this.op2 = imm;
            this.flag = flag;
            this.alu = alu;
        }

        public override void Execute()
        {
            // The carry in is always 0
            flag.Value = (short)(flag.Value & ~(1 << 0) | 0);

            // Set the op values;
            alu.op1.Value = op1.Value;
            alu.op2.Value = (short) op2;

            // Add the values
            alu.Add();

            // Get the result from the alu
            dest.Value = alu.dest.Value;
        }
    }

}

