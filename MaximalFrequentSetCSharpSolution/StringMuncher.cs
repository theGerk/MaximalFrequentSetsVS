﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchers
{
	public static class StringMuncher
	{
		public enum Formating
		{
			Raw,
			Binary,
			Octal,
			Decimal,
			Hectal,
			Text
		}

		public static List<bool> MunchToBool(this string str, Formating formating = Formating.Text)
		{
			switch (formating)
			{
				case Formating.Raw:
					throw new NotImplementedException();
					//doesn't work this way
					//System.Collections.BitArray bitArr = new System.Collections.BitArray(Convert.FromBase64String(str));
					//bool[] boolArr = new bool[bitArr.Length];
					//bitArr.CopyTo(boolArr, 0);
					//return boolArr.ToList();

				case Formating.Binary:
				case Formating.Octal:
				case Formating.Decimal:
				case Formating.Hectal:
					string[] substrs = str.Split(',', ' ', '\t', '\n');
					List<bool> output = new List<bool>(substrs.Length);
					foreach (var item in substrs)
					{
						if(item != "")
							output.Add(Convert.ToInt32(item, 16) != 0);
					}
					return output;

				case Formating.Text:
					throw new NotImplementedException("Munchers.StringMuncher.MunchToBool\nformating = Text not implemented.");

				default:
					throw new Exception("You gave a bad formating value!");
			}
		}
	}
}
