using System;
using System.IO;

namespace Decoder
{
	// An instruction decoder
	public class Decoder
	{
		private const ushort OpCodeMask = 0xFE00;
		private const ushort DestRegMask = 0x00F0;
		private const ushort ImmediateIdentifierMask = 0x0100;
		private string filePath;
		private BinaryReader br;
		private StreamWriter sw;

		private int totalInstructions = 0;
		private int addInstructions = 0;
		private int addcInstructions = 0;
		private int subbInstructions = 0;
		private int andInstructions = 0;
		private int orInstructions = 0;
		private int norInstructions = 0;
		private int shlInstructions = 0;
		private int shrInstructions = 0;
		private int sharInstructions = 0;
		private int rorInstructions = 0;
		private int rolInstructions = 0;
		private int rorcInstructions = 0;
		private int rolcInstructions = 0;

		private int loadInstructions = 0;
		private int storInstructions = 0;
		private int movInstructions = 0;
		private int pushInstructions = 0;
		private int popInstructions = 0;

		private int cmpInstructions = 0;
		private int jumpInstructions = 0;

		private int ldaInstructions = 0;
		private int negInstructions = 0;
		private int haltInstructions = 0;
		private int subInstructions = 0;
		private int xorInstructions = 0;

		private int immediateInstructions = 0;
		private int registerInstructions = 0;
		private int nopInstructions = 0;
		private int controlInstructions = 0;

		//Arithmetic Masks
		private const ushort ArithDestRegMask = DestRegMask;
		private const ushort Arith1RegOP = 0x000F;
		private const ushort RegOP2 = 0x000F; //used for any instruction format where 2nd op is 4 LSB of 32-bit LBSs

		// Create the decoder with the fetcher, registers, and memory
		public Decoder(string filePath)
		{
			this.filePath = filePath;
			br = new BinaryReader(File.Open(filePath, FileMode.Open));
			sw = new StreamWriter(@"C:\Users\NdeBe\Desktop\test.txt");
		}

		// Decode an instruction
		public string Decode(ushort EncodedInstruction)
		{
			ushort OpCode = ExtractInstruction(EncodedInstruction);
			string CreatedInstruction;
			switch (OpCode)
			{
				case (ushort)Operation.NOP:
					sw.WriteLine(CreatedInstruction = CreateNOPInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.ADD:
					sw.WriteLine(CreatedInstruction = CreateADDInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.ADDC:
					sw.WriteLine(CreatedInstruction = CreateADDCInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.SUBB:
					sw.WriteLine(CreatedInstruction = CreateSUBBInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.AND:
					sw.WriteLine(CreatedInstruction = CreateANDInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.OR:
					sw.WriteLine(CreatedInstruction = CreateORInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.NOR:
					sw.WriteLine(CreatedInstruction = CreateNORInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.SHL:
					sw.WriteLine(CreatedInstruction = CreateSHLInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.SHR:
					sw.WriteLine(CreatedInstruction = CreateSHRInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.SHAR:
					sw.WriteLine(CreatedInstruction = CreateSHARInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.ROR:
					sw.WriteLine(CreatedInstruction = CreateRORInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.ROL:
					sw.WriteLine(CreatedInstruction = CreateROLInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.RORC:
					sw.WriteLine(CreatedInstruction = CreateRORCInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.ROLC:
					sw.WriteLine(CreatedInstruction = CreateROLCInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.LOAD:
					sw.WriteLine(CreatedInstruction = CreateLOADInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.STOR:
					sw.WriteLine(CreatedInstruction = CreateSTORInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.MOV:
					sw.WriteLine(CreatedInstruction = CreateMOVInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.PUSH:
					sw.WriteLine(CreatedInstruction = CreatePUSHInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.POP:
					sw.WriteLine(CreatedInstruction = CreatePOPInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.CMP:
					sw.WriteLine(CreatedInstruction = CreateCMPInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JZ:
					sw.WriteLine(CreatedInstruction = CreateJZInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JNZ:
					sw.WriteLine(CreatedInstruction = CreateJNZInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JG:
					sw.WriteLine(CreatedInstruction = CreateJGInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JGE:
					sw.WriteLine(CreatedInstruction = CreateJGEInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JL:
					sw.WriteLine(CreatedInstruction = CreateJLInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JLE:
					sw.WriteLine(CreatedInstruction = CreateJLEInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JA:
					sw.WriteLine(CreatedInstruction = CreateJAInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JAE:
					sw.WriteLine(CreatedInstruction = CreateJAEInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JB:
					sw.WriteLine(CreatedInstruction = CreateJBInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.JBE:
					sw.WriteLine(CreatedInstruction = CreateJBEInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.LDA:
					sw.WriteLine(CreatedInstruction = CreateLDAInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.NEG:
					sw.WriteLine(CreatedInstruction = CreateNEGInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.XOR:
					sw.WriteLine(CreatedInstruction = CreateXORInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.SUB:
					sw.WriteLine(CreatedInstruction = CreateSUBInstruction(EncodedInstruction));
					break;
				case (ushort)Operation.HALT:
					sw.WriteLine(CreatedInstruction = CreateHALTInstruction(EncodedInstruction));
					break;
				default:
					throw new Exception("Invalid Instruction OP code Dedcoded");
			}

			return CreatedInstruction;
		}

		private string CreateADDInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "add " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			addInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateADDCInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "addc " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			addcInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSUBBInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "subb " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			subbInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateANDInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "and " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			andInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "or " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			orInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateNORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "nor " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			norInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSHLInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "shl " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			shlInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSHRInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "shr " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			shrInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSHARInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "shar " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			sharInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateRORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "ror " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			rorInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateROLInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "rol " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			rolInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateRORCInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "rorc " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			rorcInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateROLCInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "rolc " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			rolcInstructions++;
			totalInstructions++;
			return retValue;
		}

		private string CreateLOADInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "load " + returnRegister(destination);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + ((short)(op1 << 16 | temp));
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1) + returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			loadInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSTORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "stor " + returnRegister(destination);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + ((short)(op1 << 16 | temp));
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1) + returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			storInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateMOVInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "mov " + returnRegister(destination);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1);
				registerInstructions++;
			}
			movInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreatePUSHInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "push ";
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1);
				registerInstructions++;
			}
			pushInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreatePOPInstruction(ushort encodedInstruction)
		{
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);

			string retValue = "pop " + returnRegister(destination);
			popInstructions++;
			totalInstructions++;
			controlInstructions++;
			return retValue;
		}

		private string CreateCMPInstruction(ushort encodedInstruction)
		{
			byte op1 = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op2 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "cmp " + returnRegister(op1) + returnRegister(op2);
			cmpInstructions++;
			totalInstructions++;
			controlInstructions++;
			return retValue;
		}
		private string CreateJZInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jz ";
			return retValue;
		}
		private string CreateJNZInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jnz ";
			return retValue;
		}
		private string CreateJGInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jg ";
			return retValue;
		}
		private string CreateJGEInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			controlInstructions++;
			totalInstructions++;
			string retValue = "jge ";
			return retValue;
		}
		private string CreateJLInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jl ";
			return retValue;
		}
		private string CreateJLEInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jle ";
			return retValue;
		}
		private string CreateJAInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "ja ";
			return retValue;
		}
		private string CreateJAEInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jae ";
			return retValue;
		}
		private string CreateJBInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jb ";
			return retValue;
		}
		private string CreateJBEInstruction(ushort encodedInstruction)
		{
			jumpInstructions++;
			totalInstructions++;
			controlInstructions++;
			string retValue = "jbe ";
			return retValue;
		}

		private string CreateLDAInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte immediate20 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "lda ";
			retValue += "#" + ((int)(immediate20 << 16 | temp));
			ldaInstructions++;
			totalInstructions++;
			immediateInstructions++;
			return retValue;
		}

		private string CreateNEGInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "neg " + returnRegister(destination);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister(op1);
				registerInstructions++;
			}
			negInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateXORInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "xor " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			xorInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateSUBInstruction(ushort encodedInstruction)
		{
			byte[] bytes = br.ReadBytes(2);
			ushort temp = (ushort)(bytes[1] << 8 | bytes[2]);
			byte destination = (byte)((uint)(encodedInstruction & DestRegMask) >> 4);
			byte op1 = (byte)(encodedInstruction & Arith1RegOP);

			string retValue = "sub " + returnRegister(destination) + returnRegister(op1);
			if (immediateBitSet(encodedInstruction))
			{
				retValue += "#" + (short)temp;
				immediateInstructions++;
			}
			else
			{
				retValue += returnRegister((byte)(temp & RegOP2));
				registerInstructions++;
			}
			subInstructions++;
			totalInstructions++;
			return retValue;
		}
		private string CreateNOPInstruction(ushort encodedInstruction)
		{
			string retValue = "nop ";
			nopInstructions++;
			totalInstructions++;
			controlInstructions++;
			return retValue;
		}
		private string CreateHALTInstruction(ushort encodedInstruction)
		{
			string retValue = "halt ";
			haltInstructions++;
			totalInstructions++;
			controlInstructions++;
			return retValue;
		}

		private ushort ExtractInstruction(ushort EncodedInstruction)
		{
			//I am using ASL due to c#, bitwise & produces int. Cast applies after all operations, so ASL should act as LSL
			return (ushort)((uint)(EncodedInstruction & OpCodeMask) >> 9);

		}

		private bool immediateBitSet(ushort EncodedInstruction)
		{
			return Convert.ToBoolean(EncodedInstruction & ImmediateIdentifierMask);
		}
		string returnRegister(byte number)
		{
			switch (number)
			{
				case 0:
					return "rA";
				case 1:
					return "rB";
				case 2:
					return "rC";
				case 3:
					return "rD";
				case 4:
					return "rE";
				case 5:
					return "rF";
				case 6:
					return "rG";
				case 7:
					return "rH";
				case 8:
					return "rI";
				case 9:
					return "rJ";
				case 10:
					return "rK";
				case 11:
					return "PC1";
				case 12:
					return "PC2";
				case 13:
					return "SP1";
				case 14:
					return "SP2";
				case 15:
					return "FLAG";
				default:
					throw new Exception("Register out of bounds.");
			}
		}

		void summaryStats()
		{

		}

	}

	/// <summary>
	/// Improves readability and maintainability in case Op codes change
	/// </summary>
	enum Operation : ushort
	{
		NOP = 0b0000000,
		ADD = 0b0000001,
		ADDC = 0b0000010,
		SUBB = 0b0000011,
		AND = 0b0000100,
		OR = 0b0000101,
		NOR = 0b0000110,
		SHL = 0b0000111,
		SHR = 0b0001000,
		SHAR = 0b0001001,
		ROR = 0b0001010,
		ROL = 0b0001011,
		RORC = 0b0001100,
		ROLC = 0b0001101,
		LOAD = 0b0001110,
		STOR = 0b0001111,
		MOV = 0b0010000,
		PUSH = 0b0010001,
		POP = 0b0010010,
		CMP = 0b0010011,
		JZ = 0b0010100,
		JNZ = 0b0010101,
		JG = 0b0010110,
		JGE = 0b0010111,
		JL = 0b0011000,
		JLE = 0b0011001,
		JA = 0b0011010,
		JAE = 0b0011011,
		JB = 0b0011100,
		JBE = 0b0011101,
		LDA = 0b0011110,
		NEG = 0b0011111,
		XOR = 0b0100000,
		SUB = 0b0100001,
		HALT = 0b1111111
	}
}