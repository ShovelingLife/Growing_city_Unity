using System;

public class Large_number
{
	static string formatString = "N2";
	static double maxFractional = 0.99;
	static string[] latin;
	static string[] ones;
	static string[] tens;
	static string[] hundreds;

	static public string ToString(double rawNumber)
	{
		ScientificNotation scientificNotation = ScientificNotation.FromDouble(rawNumber);
		ushort adjustedExponent = (ushort)((scientificNotation.exponent / 3) - 1);
		string prefix = "";
		double adjustedSignificand = scientificNotation.significand * Math.Pow(10, scientificNotation.exponent % 3);
		double integralPart = Math.Truncate(adjustedSignificand);

		Language_translation_arr_init();

		if (rawNumber < 1000000.0)
		{
			return rawNumber.ToString(formatString);
		}
		if (adjustedExponent < 10)
		{
			prefix = latin[adjustedExponent - 1];
		}
		else
		{
			ushort hundredsPlace = (ushort)(adjustedExponent / 100);
			ushort tensPlace = (ushort)((adjustedExponent / 10) % 10);
			ushort onesPlace = (ushort)(adjustedExponent % 10);
			string onesString = (onesPlace > 0) ? ones[onesPlace - 1] : "";
			string modifier = "";
			if ((onesPlace == 7) || (onesPlace == 9))
			{
				if (tensPlace > 0)
				{
					if ((tensPlace == 2) || (tensPlace == 8))
					{
						modifier = "m";
					}
					else if (tensPlace != 9)
					{
						modifier = "n";
					}
				}
				else if (hundredsPlace > 0)
				{
					if (hundredsPlace == 8)
					{
						modifier = "m";
					}
					else if (hundredsPlace != 9)
					{
						modifier = "n";
					}
				}
			}
			if ((onesPlace == 3) || (onesPlace == 6))
			{
				if (tensPlace > 0)
				{
					if ((tensPlace == 2) || (tensPlace == 3) || (tensPlace == 4) || (tensPlace == 5) || (tensPlace == 8))
					{
						modifier = ((onesPlace == 6) && (tensPlace == 8)) ? "x" : "s";
					}
				}
				else if (hundredsPlace > 0)
				{
					if ((hundredsPlace == 1) || (hundredsPlace == 3) || (hundredsPlace == 4) || (hundredsPlace == 5) || (hundredsPlace == 8))
					{
						modifier = ((onesPlace == 6) && ((tensPlace == 1) || (tensPlace == 8))) ? "x" : "s";
					}
				}
			}
			string tensString = (tensPlace > 0) ? tens[tensPlace - 1] : "";
			string hundredsString = (hundredsPlace > 0) ? hundreds[hundredsPlace - 1] : "";
			prefix = string.Format("{0}{1}{2}{3}", onesString, modifier, tensString, hundredsString);
		}

		return Language_translation(adjustedSignificand, integralPart, prefix);
	}
	public struct ScientificNotation
	{
		public ushort exponent;
		public double significand;

		static public ScientificNotation FromDouble(double rawNumber)
		{
			ushort exponent = (ushort)Math.Log10(rawNumber);
			return new ScientificNotation
			{
				exponent = exponent,
				significand = rawNumber * Math.Pow(0.1, exponent)
			};
		}
	}

	static void Language_translation_arr_init()
    {
		if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
		{
			latin = new string[] { "mi", "bi", "tri", "quadri", "quinti", "sexti", "septi", "octi", "noni" };
			ones = new string[] { "un", "duo", "tre", "quattuor", "quinqua", "se", "septe", "octo", "nove" };
			tens = new string[] { "deci", "viginti", "triginta", "quadraginta", "quinquaginta", "sexaginta", "septuaginta", "octoginta", "nonaginta" };
			hundreds = new string[] { "centi", "ducenti", "trecenti", "quadringenti", "quingenti", "sescenti", "septingenti", "octingenti", "nongenti" };
		}
		if (Lean.Localization.LeanLocalization.CurrentLanguage == "Spanish")
		{
			latin = new string[] { "mi", "bi", "tri", "quatri", "quinti", "sexti", "septi", "octi", "noni" };
			ones = new string[] { "diez", "once", "doce", "trece", "catorce", "quince", "dieciseis", "dieciciete", "diecinueve" };
			tens = new string[] { "veinte", "veintiuno", "veintidos", "veintitres", "veinticuatro", "veinticinco", "veintiseis", "veintisiete", "veintiocho" };
			hundreds = new string[] { "veintinueve", "treinta", "treintauno", "treintados", "treintatres", "treintacuatro", "treintacinco", "treintaseis", "treintasiete" };
		}
		if (Lean.Localization.LeanLocalization.CurrentLanguage == "Korean")
		{
			latin = new string[] { "억", "십억", "백억", "천억", "조", "십조", "백조", "천조", "경" };
			ones = new string[] { "십경", "백경", "천경", "해", "십해", "백해", "천해", "자", "십자" };
			tens = new string[] { "백자", "천자", "양", "십양", "백양", "천양", "구", "십구", "백구" };
			hundreds = new string[] { "천구", "간", "십간", "백간", "천간", "정", "십정", "백정", "천정" };
		}
	}

	static string Language_translation(double adjustedSignificand, double integralPart, string prefix)
    {
		if (Lean.Localization.LeanLocalization.CurrentLanguage == "Spanish")
			return string.Format("{0} {1}llon", (((adjustedSignificand - integralPart) > maxFractional) ? integralPart + maxFractional : adjustedSignificand).ToString(formatString), prefix.TrimEnd('a'));

		if (Lean.Localization.LeanLocalization.CurrentLanguage == "Korean")
			return string.Format("{0} {1}", (((adjustedSignificand - integralPart) > maxFractional) ? integralPart + maxFractional : adjustedSignificand).ToString(formatString), prefix.TrimEnd('a'));

		if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
			return string.Format("{0} {1}llion", (((adjustedSignificand - integralPart) > maxFractional) ? integralPart + maxFractional : adjustedSignificand).ToString(formatString), prefix.TrimEnd('a'));

		else return null;
	}
}