using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulator;

namespace IsaGui
{
    public partial class Form1 : Form
    {
        private Memory simMemory;
        private CPU simCpu;
        private string assemblyFilePath;
        private string binaryInFilePath;
        private Simulator.Encoder binaryEncoder;
        public Form1()
        {
            simMemory = new Memory(0x100000, true);
            simCpu = new CPU(simMemory);
            simCpu.registers.RE.Value = 0xF;
            simCpu.registers.RF.Value = 0xFFFE;
            InitializeComponent();
            updateRegisters();
        }

        private void StepButton_Click(object sender, EventArgs e)
        {
            simCpu.RunClockCycle();
            updateRegisters();
        }

        private void updateRegisters()
        {
            ALU1Box.Text = Convert.ToString(simCpu.alu.dest.Value, 2);
            ALU2Box.Text = Convert.ToString(simCpu.alu.op1.Value, 2);
            ALU3Box.Text = Convert.ToString(simCpu.alu.op2.Value, 2);
            PCBox.Text = Convert.ToString(simCpu.registers.PC1.Value, 2);
            SP1Box.Text = Convert.ToString(simCpu.registers.SP1.Value, 2);
            SP2Box.Text = Convert.ToString(simCpu.registers.SP2.Value, 2);
            rABox.Text = Convert.ToString(simCpu.registers[0].Value, 2);
            rBBox.Text = Convert.ToString(simCpu.registers[1].Value, 2);
            rCBox.Text = Convert.ToString(simCpu.registers[2].Value, 2);
            rDBox.Text = Convert.ToString(simCpu.registers[3].Value, 2);
            rEBox.Text = Convert.ToString(simCpu.registers[4].Value, 2);
            rFBox.Text = Convert.ToString(simCpu.registers[5].Value, 2);
            rGBox.Text = Convert.ToString(simCpu.registers[6].Value, 2);
            rHBox.Text = Convert.ToString(simCpu.registers[7].Value, 2);
            rIBox.Text = Convert.ToString(simCpu.registers[8].Value, 2);
            rJBox.Text = Convert.ToString(simCpu.registers[9].Value, 2);
            rKBox.Text = Convert.ToString(simCpu.registers[10].Value, 2);
            sBox.Text = Convert.ToString((((uint)simCpu.registers.FLAG.Value & 0x0010) >> 4), 2);
            oBox.Text = Convert.ToString((((uint)simCpu.registers.FLAG.Value & 0x0008) >> 3), 2);
            eqBox.Text = Convert.ToString((((uint)simCpu.registers.FLAG.Value & 0x0004) >> 2), 2);
            zBox.Text = Convert.ToString((((uint)simCpu.registers.FLAG.Value & 0x0002) >> 1), 2);
            cBox.Text = Convert.ToString(((uint)simCpu.registers.FLAG.Value & 0x0001), 2);
        }

        private void openBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            assemblyFilePath = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..");
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            binaryInFilePath = openFileDialog.FileName;
            InstallBinary(binaryInFilePath);
        }

        private void openAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            binaryInFilePath = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..");
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            //Load into assembly list box
            assemblyBox.Items.Clear();
            assemblyFilePath = openFileDialog.FileName;
            var fileStream = File.OpenRead(assemblyFilePath);
            var streamReader = new StreamReader(fileStream);
            string pulledLine;
            while ((pulledLine = streamReader.ReadLine()) != null)
            {
                assemblyBox.Items.Add(pulledLine);
            }
            assemblyBox.EndUpdate();
             binaryEncoder = new Simulator.Encoder(assemblyFilePath);
            binaryEncoder.EncodeFile();
            InstallBinary(assemblyFilePath.Replace(".txt",".bin"));
        }

        private void InstallBinary(string binaryInFilePath)
        {
            byte[] array = File.ReadAllBytes(binaryInFilePath);

            for (int i = 0; i < array.Length - 1; i = i + 2)
            {
                ushort pulledShort = (ushort)(array[i] << 8 | array[i + 1]);
                simMemory[i] = pulledShort;
            }

            string hexString = BitConverter.ToString(array);
            hexString = hexString.Replace("-", "");

            int j = 4;
            while (j < hexString.Length)
            {
                hexString = hexString.Insert(j, " ");
                j += 5;
            }

            BinaryTextBox.Text = hexString;
        }
    }
}
