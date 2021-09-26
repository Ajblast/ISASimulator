using Simulator;

namespace Simulator
{
	// An instruction fetcher
	public class Fetcher
	{
		Register PC;	// The program counter
		Memory memory;	// Memory

		// Fecther constructor
		public Fetcher(Register PC, Memory memory)
		{
			this.PC = PC;
			this.memory = memory;
		}

		// Fetch an instruction from memory and increment the program counter
		public ushort Fetch()
		{
			// Retrieve the instruction from memory
			ushort instruction = memory[PC.Value];
			// Increment the program counter
			PC.Value += 2;

			return instruction;
		}

	}

}

