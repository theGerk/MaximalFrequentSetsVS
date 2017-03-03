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
					throw new NotImplementedException("Munchers.StringMuncher.MunchToBool\nformating = Raw not implemented.");
				case Formating.Binary:
				case Formating.Octal:
				case Formating.Decimal:
				case Formating.Hectal:
					
				case Formating.Text:
					throw new NotImplementedException("Munchers.StringMuncher.MunchToBool\nformating = Text not implemented.");
				default:
					throw new Exception("You gave a bad formating value!");
			}
		}
	}
}