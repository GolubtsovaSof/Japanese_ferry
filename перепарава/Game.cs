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
	class Game
	{
		FlowLayoutPanel LeftShore;
		FlowLayoutPanel Raft;
		FlowLayoutPanel RightShore;

		List<PictureBox> Pics;
		Dictionary<PictureBox, Person> dict;

		List<Person> Persons;

		List<Person> LeftPerson = new List<Person>();
		List<Person> RightPerson = new List<Person>();
		List<Person> RaftPerson = new List<Person>();

		Person Man = new Person(); Person Son1 = new Person(); Person Son2 = new Person();
		Person Woman = new Person(); Person Girl1 = new Person(); Person Girl2 = new Person();
		Person Police = new Person(); Person Killer = new Person();

		public Game (FlowLayoutPanel left, FlowLayoutPanel raft, FlowLayoutPanel right, List<PictureBox> pics)
		{
			LeftShore = left;
			Raft = raft;
			RightShore = right;
			Pics = pics;

			Persons = new List<Person> { Man, Son1, Son2, Woman, Girl1, Girl2, Police, Killer };
			for (int i=0; i<Pics.Count; i++)
			{
				Persons[i].picture = Pics[i];
				LeftPerson.Add(Persons[i]);
			}
			dict = Enumerable.Range(0, Pics.Count()).ToDictionary(i => Pics[i], i => Persons[i]);
		}
		public void Redraw()
		{
			LeftPerson.Clear();
			RaftPerson.Clear();
			RightPerson.Clear();
			LeftShore.Controls.Clear();
			Raft.Controls.Clear();
			RightShore.Controls.Clear();
			foreach (Person p in Persons)
			{
				if (p.side == Person.Sides.left)
				{
					LeftPerson.Add(p);
					LeftShore.Controls.Add(p.picture);
				}
				else if (p.side == Person.Sides.right)
				{
					RightShore.Controls.Add(p.picture);
					RightPerson.Add(p);
				}
				else
				{
					RaftPerson.Add(p);
					Raft.Controls.Add(p.picture);
				}
			}
		}
		public void ToRaft(PictureBox picture)
		{
			dict[picture].SetOnRaft();
		}
		public void ToLeft(PictureBox picture)
		{
			dict[picture].SetOnLeft();
		}
		public void ToRight(PictureBox picture)
		{
			dict[picture].SetOnRight();
		}

		public bool Check(FlowLayoutPanel flow)
		{
			bool CheckAdultOnRaft = (RaftPerson.Contains(Man) || RaftPerson.Contains(Woman) || RaftPerson.Contains(Police) || RaftPerson.Contains(Killer));
			bool CheckAdult = true;
			bool CheckKiller = true;
			bool CheckFuture = true;
			if (flow == LeftShore)
			{
				CheckFuture = ((RaftPerson.Contains(Woman)&&!RaftPerson.Contains(Man)) && (RightPerson.Contains(Son1) || RightPerson.Contains(Son2)) && !RightPerson.Contains(Man)
								||(RaftPerson.Contains(Man) && !RaftPerson.Contains(Woman)) && (RightPerson.Contains(Girl1) || RightPerson.Contains(Girl2)) && !RightPerson.Contains(Woman)); 
				CheckAdult = (LeftPerson.Contains(Woman) && (LeftPerson.Contains(Son1) || LeftPerson.Contains(Son1)) && !LeftPerson.Contains(Man) ||
								LeftPerson.Contains(Man)&&(LeftPerson.Contains(Girl1) || LeftPerson.Contains(Girl2)) && !LeftPerson.Contains(Woman));
				CheckKiller = (LeftPerson.Contains(Man) && LeftPerson.Contains(Killer) && !LeftPerson.Contains(Police) && LeftPerson.Count>1);
			}
			if (flow == RightShore)
			{
				CheckFuture = ((RaftPerson.Contains(Woman) && !RaftPerson.Contains(Man)) && (LeftPerson.Contains(Son1) || LeftPerson.Contains(Son2)) && !LeftPerson.Contains(Man)
								|| (RaftPerson.Contains(Man) && !RaftPerson.Contains(Woman)) && (LeftPerson.Contains(Girl1) || LeftPerson.Contains(Girl2)) && !LeftPerson.Contains(Woman));
				CheckAdult = (RightPerson.Contains(Woman) && (RightPerson.Contains(Son1) || RightPerson.Contains(Son1)) && !RightPerson.Contains(Man) ||
								RightPerson.Contains(Man)&&(RightPerson.Contains(Girl1) || RightPerson.Contains(Girl2)) && !RightPerson.Contains(Woman));
				CheckKiller = (RightPerson.Contains(Killer) && !RightPerson.Contains(Police) && RightPerson.Count > 1);
			}
			if (!CheckAdultOnRaft || CheckFuture || CheckKiller || CheckAdult) return false;
			return true;
		}

		public void MoveRaft(FlowLayoutPanel flow)
		{
			if (flow == RightShore) foreach (Person p in RaftPerson) p.SetOnRight();
			else foreach (Person p in RaftPerson) p.SetOnLeft();
		}
	}
}
