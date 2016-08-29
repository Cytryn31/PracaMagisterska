using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaMagisterska
{
	public class MinutiaeTuple
	{
		public double Rot { get; set; }
		public double Scaling { get; set; }
		public double Dir { get; set; }
		public double Transaltion { get; set; }

		public double CostFunctionValue => MinutiaeManager.Alpha * (Transaltion / MinutiaeManager.TranslationMax)
										   + MinutiaeManager.Beta * (Rot / MinutiaeManager.RotationMax)
										   + MinutiaeManager.Gamma * (Rot / MinutiaeManager.ScalingMax);

		public Common.Tuple<Common.Tuple<Minutiae>> Tuple;
	}
}
