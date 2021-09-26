using Simulator;
using Simulator.Instructions;

namespace Simulator
{
	// An instruction decoder
	public class Decoder
	{
		private Fetcher fetcher;
		private Registers registers;
		private Memory memory;

		// Create the decoder with the fetcher, registers, and memory
		public Decoder(Fetcher fetcher, Registers registers, Memory memory)
		{
			this.fetcher = fetcher;
			this.registers = registers;
			this.memory = memory;
		}

		// Decode an instruction
		public Instruction Decode(ushort EncodedInstruction)
		{
			return null;
		}

	}

}

