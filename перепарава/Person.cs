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
	class Person
	{
		public enum Sides { left, mid, right };
		Sides Side = Sides.left;

		PictureBox Picture;

		public void SetOnRaft()
		{
			Side = Sides.mid;
		}
		public void SetOnLeft()
		{
			Side = Sides.left;
		}
		public void SetOnRight()
		{
			Side = Sides.right;
		}

		public PictureBox picture
		{
			set { Picture = value; }
			get { return Picture; }
		}
		public Sides side
		{
			get { return Side; }
		}
	}
}
