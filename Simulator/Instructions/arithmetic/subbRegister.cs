using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.arithmetic
{
	public class subbRegister : Instruction
	{
        private Register dest;
        private Register op1;
        private Register op2;
        private Register flag;
        private ALU alu;

        public subbRegister(Register dest, Register op1, Register op2, Register flag, ALU alu)
        {
            this.dest = dest;
            this.op1 = op1;
            this.op2 = op2;
            this.flag = flag;
            this.alu = alu;
        }

        public override void Execute()
        {
            // OP2 + CF
            // Add the second op and the carry flag
            alu.op1.Value = op2.Value;
            alu.op2.Value = 0;
            alu.Add();


            //// -(OP2 + CF)
            //// Negate the result
            //alu.op1.Value = alu.dest.Value;
            //alu.Neg();
            //// And add 1
            //flag.Value = (short)(flag.Value & ~(1 << 0) | 0);
            //alu.op1.Value = alu.dest.Value;
            //alu.op2.Value = 1;
            //alu.Add();


            // OP1 - (OP2 + CF)
            // Add the first op and the result of the carry
            alu.op1.Value = op1.Value;
            alu.op2.Value = alu.dest.Value;
            flag.Value = (short)(flag.Value & ~(1 << 0) | 0);
            alu.Sub();

            // Get the result from the alu
            dest.Value = alu.dest.Value;
        }
    }

}

