using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingWorkoutRobot
{
    public class Models
    {
		

		public enum LanguageOption
		{
			English,
			日本語,
			简体中文,
			繁體中文,
			
		}

		public class WorkoutData
		{
			public bool ChosenToUpload { get; set; }//是否要上傳
			public string WorkoutName { get; set; }
			//public int DurationTimeInSecond { get; set; }
			public string WorkoutLength { get; set; }
			public Image WorkoutCurve { get; set; }
			public string FullFilePath { get; set; }
		}

	}
}
