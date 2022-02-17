using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace перепарава
{
	public partial class Form1 : Form
	{
		List<PictureBox> pics = new List<PictureBox>();
		Game game;
		public Form1()
		{
			InitializeComponent();
			DoubleBuffered = true;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			foreach (PictureBox p in flowLayoutPanel1.Controls.OfType<PictureBox>())
			{
				pics.Add(p);
				p.Click += P_Click;
			}
			game = new Game(flowLayoutPanel1, flowLayoutPanel3, flowLayoutPanel2, pics);

			pictureBox2.Visible = false;
			flowLayoutPanel2.Enabled = false;
		}

		private void P_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel3.Controls.Count<2 && !flowLayoutPanel3.Controls.Contains(sender as PictureBox))
			{
				game.ToRaft(sender as PictureBox);
				Refresh();
			}
			else if (flowLayoutPanel3.Contains(sender as PictureBox))
			{
				if(flowLayoutPanel2.Enabled==false) game.ToLeft(sender as PictureBox);
				else game.ToRight(sender as PictureBox);
				Refresh();
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			if (game.Check(flowLayoutPanel1))
			{
				game.MoveRaft(flowLayoutPanel2);

				flowLayoutPanel1.Enabled = false;
				flowLayoutPanel2.Enabled = true;

				pictureBox2.Visible = true;
				pictureBox1.Visible = false;

				Refresh();
			}
			else MessageBox.Show("Нарушены правила игры!");
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			if (game.Check(flowLayoutPanel2))
			{
				game.MoveRaft(flowLayoutPanel1);

				flowLayoutPanel1.Enabled = true;
				flowLayoutPanel2.Enabled = false;

				pictureBox2.Visible = false;
				pictureBox1.Visible = true;

				Refresh();
			}
			else MessageBox.Show("Нарушены правила игры!");
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			game.Redraw();
		}
	}
}
