using Simulator;
using Simulator.Instructions;

namespace Simulator
{
	// The CPU
	public class CPU
	{
		private Fetcher fetcher;		// The instruction fetcher
		private Decoder decoder;		// The instruction decoder
		private Executor executor;		// The instruction executor
		private Registers registers;	// The registers

		// A CPU is created knowing what the memory is
		public CPU(Memory memory)
		{
			// Create the registers
			registers = new Registers();

			// Create the fetcher
			fetcher = new Fetcher(registers.PC, memory);

			// Create the decoder
			decoder = new Decoder(fetcher, registers, memory);

			// Create the executor
			executor = new Executor();
		}

		// Run a clock cycle
		public void RunClockCycle()
		{
			// Fetch from memory
			ushort encodedInstruction = fetcher.Fetch();

			// Decode the instruction
			Instruction instruction = decoder.Decode(encodedInstruction);

			// Execute the instruction
			executor.ExecuteInstruction(instruction);
		}

	}

}

