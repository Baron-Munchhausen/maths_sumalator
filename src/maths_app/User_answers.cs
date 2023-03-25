using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maths_app
{
	class User_answer
	{
		public int id { get; set; }

		private int user_id, is_right;
		private string answer, task_id;

		public int User_id
		{
			get { return user_id; }
			set { user_id = value; }
		}
		public string Task_id
		{
			get { return task_id; }
			set { task_id = value; }
		}
		public int Is_right
		{
			get { return is_right; }
			set { is_right = value; }
		}
		public string Answer
		{
			get { return answer; }
			set { answer = value; }
		}

		public User_answer() { }

		public User_answer(int user_id, string task_id, string answer, int is_right)
		{
			this.user_id = user_id;
			this.task_id = task_id;
			this.answer = answer;
			this.is_right = is_right;
		}
	}
}
