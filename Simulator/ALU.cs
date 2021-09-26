using System;
using System.Collections.Generic;
using System.Text;

namespace Simulator
{
    public class ALU
    {
        public Register dest;
        public Register op1;
        public Register op2;
        private Register flag;

        public ALU(Register flag)
        {
            dest = new Register();
            op1 = new Register();
            op2 = new Register();
            this.flag = flag;
        }
        public void Add() 
        {
            // Get the carry in flag
            int carryIn = flag.Value & 0x1;

            int result = 0;

            // Do a full adder for every single bit to account for the carry in
            // Shift the input values to the very first bit because we just want to do single bit things
            for (int i = 0; i < 16; i++)
            {
                int aBit = (op1.Value >> i) & 0x1;    // Get single bit
                int bBit = (op2.Value >> i) & 0x1;    // Get single bit

                // Determine the sum bit
                int sum = carryIn ^ (aBit ^ bBit);

                // Determine the carry out bit
                carryIn = (aBit & bBit) | (bBit & carryIn) | (aBit & carryIn);

                // Or the result with the sum shifted because we don't actually have full adders
                result = result | sum << i;
            }

            // Set the final value
            dest.Value = (short)result;

            // Set the carry flag
            flag.Value = (short)(flag.Value & ~(1 << 0) | carryIn);

            // Set the zero bit
            if (dest.Value == 0)
                flag.Value = (short)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (short)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (short)(flag.Value & ~(1 << 2));

            // Set the overflow flag
            if ((op1.Value & 0x8000) == (op2.Value & 0x8000) && (op1.Value & 0x8000) != (dest.Value & 0x8000))
                flag.Value = (short)(flag.Value & ~(1 << 3) | (1 << 3));
            else
                flag.Value = (short)(flag.Value & ~(1 << 3));   // Clear the overflow flag if there wasn't an overflow

            // Set the sign flag
            flag.Value = (short)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Sub() 
        {
            // Get the carry in flag
            int carryIn = flag.Value & 0x1;

            int result = 0;

            // Do a full adder for every single bit to account for the carry in
            // Shift the input values to the very first bit because we just want to do single bit things
            for (int i = 0; i < 16; i++)
            {
                int aBit = (op1.Value >> i) & 0x1;    // Get single bit
                int bBit = (op2.Value >> i) & 0x1;    // Get single bit

                // Determine the sum bit
                int diff = carryIn ^ (aBit ^ bBit);

                // Determine the carry out bit
                carryIn = ((~aBit & 1) & carryIn) | ((~aBit & 1) & bBit) | (bBit & carryIn);

                // Or the result with the sum shifted because we don't actually have full adders
                result = result | diff << i;
            }

            // Set the final value
            dest.Value = (short)result;

            // Set the carry flag
            flag.Value = (short)(flag.Value & ~(1 << 0) | carryIn);

            // Set the zero bit
            if (dest.Value == 0)
                flag.Value = (short)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (short)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (short)(flag.Value & ~(1 << 2));

            // Set the overflow flag
            if ((op1.Value & 0x8000) == (op2.Value & 0x8000) && (op1.Value & 0x8000) != (dest.Value & 0x8000))
                flag.Value = (short)(flag.Value & ~(1 << 3) | (1 << 3));
            else
                flag.Value = (short)(flag.Value & ~(1 << 3));   // Clear the overflow flag if there wasn't an overflow

            // Set the sign flag
            flag.Value = (short)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }


        public void Rol() { }
        public void Rolc() { }
        public void Ror() { }
        public void Rorc() { }
        public void Shl() { }
        public void Shr() { }
        public void Shar() { }
        public void And() 
        {
            // And the two values
            dest.Value = (short)(op1.Value & op2.Value);

            // Set the carry flag
            flag.Value = (short)(flag.Value & ~(1 << 0) | 0);

            // Set the zero bit
            if (dest.Value == 0)
                flag.Value = (short)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (short)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (short)(flag.Value & ~(1 << 2));

            // Set the overflow flag
            if ((op1.Value & 0x8000) == (op2.Value & 0x8000) && (op1.Value & 0x8000) != (dest.Value & 0x8000))
                flag.Value = (short)(flag.Value & ~(1 << 3) | (1 << 3));
            else
                flag.Value = (short)(flag.Value & ~(1 << 3));   // Clear the overflow flag if there wasn't an overflow

            // Set the sign flag
            flag.Value = (short)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Or() 
        {
            // Or the two values
            dest.Value = (short)(op1.Value | op2.Value);

            // Set the carry flag
            flag.Value = (short)(flag.Value & ~(1 << 0) | 0);

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (short)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (short)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (short)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (short)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (short)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Nor() { }
        public void Xor()
        {
            // Xor the two values
            dest.Value = (short)(op1.Value ^ op2.Value);

            // Set the carry flag
            flag.Value = (short)(flag.Value & ~(1 << 0) | 0);

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (short)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (short)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (short)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (short)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (short)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
        public void Neg() 
        {
            // Negate the value
            dest.Value = (short) ~op1.Value;

            // Set the carry flag
            flag.Value = (short)(flag.Value & ~(1 << 0) | 0);

            // Set the zero flag
            if (dest.Value == 0)
                flag.Value = (short)(flag.Value & ~(1 << 1) | (1 << 1));
            else
                flag.Value = (short)(flag.Value & ~(1 << 1));   // Clear the zero flag if the result wasn't zero

            // Clear the Equality flag
            flag.Value = (short)(flag.Value & ~(1 << 2));

            // Clear the overflow flag
            flag.Value = (short)(flag.Value & ~(1 << 3));

            // Set the sign flag
            flag.Value = (short)(flag.Value & ~(1 << 4) | ((dest.Value >> 15) << 4));

            flag.Value &= 0x1f;
        }
    }
}
