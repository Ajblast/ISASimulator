namespace Simulator
{
	// The memory of the simulator
	public class Memory
	{
		private byte[] TheWholeNineYards;

		// Create a new memory
		public Memory(int size)
        {
			// Create the byte array
			TheWholeNineYards = new byte[size];
        }

		// Index into the whole nine yards
		public ushort this[int index]
		{
			get
			{
				// Return a short
				return (ushort)((TheWholeNineYards[index] << 8) | TheWholeNineYards[index + 1]);
			}
			set
            {
				// Set the individual bytes
				TheWholeNineYards[index] = (byte) ((value & 0xFF00) >> 8);
				TheWholeNineYards[index+1] = (byte) (value & 0x00FF);
            }
		}

	}

}

