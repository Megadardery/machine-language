﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MachineLanguage
{
	public partial class frmMemoryEdit : Form
	{
		frmMain mainform;
		public frmMemoryEdit(frmMain myparent)
		{
			mainform = myparent;
			this.Icon = MachineLanguage.Properties.Resources.icon_programming;
			InitializeComponent();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			try {
				var builder = new StringBuilder();

				byte[] result; 
				switch (Extra.StringToData(textBox1.Text,out result))
				{
					case Extra.errors.InvalidCharacter:
						MessageBox.Show("Couldn't interpret the added memory.", "Batch Add Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					case Extra.errors.Incomplete:
						MessageBox.Show("The hex code you entered does not form complete bytes. Please check your code.", "Batch Add Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					case Extra.errors.NotProperCode:
						if (MessageBox.Show("The code you added does not form a complete instruction set. Are you sure you want to add this code?", "Batch Add Code"
						, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No) return;
						break;
				}
				if (!Extra.IsHexable(textBox2.Text))
				{
					MessageBox.Show("The address is Invalid", "Batch Add Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				byte address = byte.Parse(textBox2.Text, System.Globalization.NumberStyles.HexNumber);
				if (0xFF - address < result.Length - 1)
				{
					MessageBox.Show("The Memory cannot be modified past 0xFF.", "Batch Add Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				mainform.FillMemory(result,address);
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("An unexpected error has occurred. The error is: " + ex.Message, "Batch Add Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void frmMemoryEdit_Load(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			button1.Enabled = (textBox1.TextLength > 1);
		}
	}
}