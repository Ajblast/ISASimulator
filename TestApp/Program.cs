using System;
using Simulator;
using Simulator.Instructions.arithmetic;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Create memory
            //Memory memory = new Memory(0x100000);

            //// Create the CPU
            //CPU cpu = new CPU(memory);


            Register dest = new Register();
            Register op1 = new Register();
            Register op2 = new Register();
            Register flag = new Register();
            ALU alu = new ALU(flag);
            short immValue = 1;

            op1.Value = 0x7FFF;
            op2.Value = (ushort) immValue;
            addcRegister add = new addcRegister(dest, op1, op2, flag, alu);
            addcImmediate add2 = new addcImmediate(dest, op1, (ushort) immValue, flag, alu);
            addRegister add3 = new addRegister(dest, op1, op2, flag, alu);
            addImmediate add4 = new addImmediate(dest, op1, (ushort) immValue, flag, alu);

            // Test adding with carry
            flag.Value = 1;
            add.Execute();
            Console.WriteLine("Addc reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            flag.Value = 1;
            add2.Execute();
            Console.WriteLine("Addc reg {0:X4} and imm {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, immValue, dest.Value, flag.Value);

            // Test add
            flag.Value = 0;
            add3.Execute();
            Console.WriteLine("Add reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            flag.Value = 0;
            add4.Execute();
            Console.WriteLine("Add reg {0:X4} and imm {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, immValue, dest.Value, flag.Value);


            // Test subtracting greater than
            uint op3 = 0x00010000;
            uint op4 = 0x00000001;
            op1.Value = (ushort) (op3 & 0x0000FFFF); // Get the lower order bits of the first operand
            op2.Value = (ushort) (op4 & 0x0000FFFF); // Get the lower order bits of the second operand
            subRegister testForSub = new subRegister(dest, op1, op2, flag, alu); // Sub the lower bits to get the carry
            testForSub.Execute();
            ushort lowerResult = dest.Value;
            Console.WriteLine("Sub reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, lowerResult, flag.Value);

            op1.Value = (ushort)((op3 & 0xFFFF0000) >> 16);    // Get the upper order bits of the first operand
            op2.Value = (ushort)((op4 & 0xFFFF0000) >> 16);    // Get the upper order bits of the second operand

            subbRegister sub = new subbRegister(dest, op1, op2, flag, alu);  // Subtract with borrow
            sub.Execute();
            ushort upperResult = dest.Value;

            Console.WriteLine("Subb {0:X8} and {1:X8} = {2:X4}{3:X4}, FLAGS {4:X}", op3, op4, upperResult, lowerResult, flag.Value);

            Console.WriteLine();

            // Test shifting
            op1.Value = 0xFFFF;
            op2.Value = 1;
            shlRegister shl = new shlRegister(dest, op1, op2, flag, alu);
            shl.Execute();
            Console.WriteLine("Shl reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

            shrRegister shr = new shrRegister(dest, op1, op2, flag, alu);
            shr.Execute();
            Console.WriteLine("Shr reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);
            sharRegister shar = new sharRegister(dest, op1, op2, flag, alu);
            shar.Execute();
            Console.WriteLine("Shar reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);


            // Test Rotates
            op1.Value = 0x00FF;
            op2.Value = 4;
            rorRegister ror = new rorRegister(dest, op1, op2, flag, alu);
            ror.Execute();
            Console.WriteLine("Ror reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

            rolRegister rol = new rolRegister(dest, op1, op2, flag, alu);
            rol.Execute();
            Console.WriteLine("Rol reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

            flag.Value = 0;
            rorcRegister rorc = new rorcRegister(dest, op1, op2, flag, alu);
            rorc.Execute();
            Console.WriteLine("Rorc reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

            flag.Value = 0;
            op1.Value = 0xFF00;
            rolcRegister rolc = new rolcRegister(dest, op1, op2, flag, alu);
            rolc.Execute();
            Console.WriteLine("Rolc reg {0:X4} and reg {1:X4} = {2:X4}, FLAGS {3:X}", op1.Value, op2.Value, dest.Value, flag.Value);

        }
    }
}
