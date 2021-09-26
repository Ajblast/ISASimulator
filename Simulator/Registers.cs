using Simulator;

namespace Simulator
{
	// All of the registers
	public class Registers
	{
		// The registers
		private Register[] regs = new Register[16];

		// Get a register
		public Register this[int index]
		{
			get
			{
				return regs[index];
			}
		}

		// Get the program counter
		public Register PC
        {
			get {
				return this[12];
			}
        }
		// Get the upper order stack pointer bits
		public Register SP1
		{
			get
			{
				return this[13];
			}
		}
		// Get the lower order stack pointer bits
		public Register SP2
		{
			get
			{
				return this[14];
			}
		}
		// Get the flag register
		public Register FLAG
        {
			get
			{
				return this[15];
			}
        }

	}

}

