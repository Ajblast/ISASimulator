﻿using Simulator.Instructions;
using Simulator;

namespace Simulator.Instructions.logical
{
    public class negRegister : Instruction
    {
        public negRegister(Register dest, Register op1, Register flag, ALU alu)
        {

        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }

}

