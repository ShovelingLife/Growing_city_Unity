using System;

public class Large_number
{
	static string   formatString = "N2";
	static double   maxFractional = 0.99;
	static string[] arr_latin;
	static string[] arr_ones;
	static string[] arr_tens;
	static string[] arr_hundreds;
    static string	m_current_language = Csv_loader_manager.instance.current_language_prop;

    static public string ToString(double _raw_number)
	{
		Scientific_notation scientific_notation = Scientific_notation.FromDouble(_raw_number);
		ushort adjusted_exponent			   = (ushort)((scientific_notation.exponent / 3) - 1);
		string prefix						   = "";
		double adjustedSignificand			   = scientific_notation.significand * Math.Pow(10, scientific_notation.exponent % 3);
		double integralPart					   = Math.Truncate(adjustedSignificand);

		Language_translation_arr_init();

		if (_raw_number < 1000000.0)
			return _raw_number.ToString(formatString);

		if (adjusted_exponent < 10)
			prefix = arr_latin[adjusted_exponent - 1];

		else
		{
			ushort hundredsPlace = (ushort)(adjusted_exponent / 100);
			ushort tensPlace	 = (ushort)((adjusted_exponent / 10) % 10);
			ushort onesPlace	 = (ushort)(adjusted_exponent % 10);
			string onesString	 = (onesPlace > 0) ? arr_ones[onesPlace - 1] : "";
			string modifier		 = "";

			if (onesPlace == 7 || 
				onesPlace == 9)
			{
				if (tensPlace > 0)
				{
					if		(tensPlace == 2 || 
							 tensPlace == 8)
							 modifier = "m";

					else if (tensPlace != 9)
							 modifier = "n";
				}
				else if (hundredsPlace > 0)
				{
					if		(hundredsPlace == 8)
							 modifier = "m";

					else if (hundredsPlace != 9)
							 modifier = "n";
				}
			}
			if (onesPlace == 3 || 
				onesPlace == 6)
			{
				if (tensPlace > 0)
				{
					if (tensPlace == 2 || tensPlace == 3 || 
						tensPlace == 4 || tensPlace == 5 || tensPlace == 8)
						modifier = (onesPlace == 6 && tensPlace == 8) ? "x" : "s";
				}
				else if (hundredsPlace > 0)
				{
					if (hundredsPlace == 1 || hundredsPlace == 3 || 
						hundredsPlace == 4 || hundredsPlace == 5 || hundredsPlace == 8)
						modifier = (onesPlace == 6 && (tensPlace == 1 || tensPlace == 8)) ? "x" : "s";
				}
			}
			string tensString	  = (tensPlace > 0) ? arr_tens[tensPlace - 1] : "";
			string hundredsString = (hundredsPlace > 0) ? arr_hundreds[hundredsPlace - 1] : "";
			prefix				  = string.Format("{0}{1}{2}{3}", onesString, modifier, tensString, hundredsString);
		}
		return Language_translation(adjustedSignificand, integralPart, prefix);
	}
	public struct Scientific_notation
	{
		public ushort exponent;
		public double significand;

		static public Scientific_notation FromDouble(double _raw_number)
		{
			ushort exponent = (ushort)Math.Log10(_raw_number);

			return new Scientific_notation
			{
				exponent = exponent,
				significand = _raw_number * Math.Pow(0.1, exponent)
			};
		}
	}

	static void Language_translation_arr_init()
    {

		if		(m_current_language == "english")
		{
				 arr_latin	 = new string[] { "mi", "bi", "tri", "quadri", "quinti", "sexti", "septi", "octi", "noni" };
				 arr_ones	 = new string[] { "un", "duo", "tre", "quattuor", "quinqua", "se", "septe", "octo", "nove" };
				 arr_tens	 = new string[] { "deci", "viginti", "triginta", "quadraginta", "quinquaginta", "sexaginta", "septuaginta", "octoginta", "nonaginta" };
				 arr_hundreds = new string[] { "centi", "ducenti", "trecenti", "quadringenti", "quingenti", "sescenti", "septingenti", "octingenti", "nongenti" };
		}
		else if (m_current_language == "spanish")
		{
				 arr_latin	 = new string[] { "mi", "bi", "tri", "quatri", "quinti", "sexti", "septi", "octi", "noni" };
				 arr_ones	 = new string[] { "diez", "once", "doce", "trece", "catorce", "quince", "dieciseis", "dieciciete", "diecinueve" };
				 arr_tens	 = new string[] { "veinte", "veintiuno", "veintidos", "veintitres", "veinticuatro", "veinticinco", "veintiseis", "veintisiete", "veintiocho" };
				 arr_hundreds = new string[] { "veintinueve", "treinta", "treintauno", "treintados", "treintatres", "treintacuatro", "treintacinco", "treintaseis", "treintasiete" };
		}
		else if (m_current_language == "korean")
		{
				 arr_latin	 = new string[] { "억", "십억", "백억", "천억", "조", "십조", "백조", "천조", "경" };
				 arr_ones	 = new string[] { "십경", "백경", "천경", "해", "십해", "백해", "천해", "자", "십자" };
				 arr_tens	 = new string[] { "백자", "천자", "양", "십양", "백양", "천양", "구", "십구", "백구" };
				 arr_hundreds = new string[] { "천구", "간", "십간", "백간", "천간", "정", "십정", "백정", "천정" };
		}
	}

	static string Language_translation(double adjustedSignificand, double integralPart, string prefix)
    {
        if		(m_current_language == "english")
				 return string.Format("{0} {1}llion", (((adjustedSignificand - integralPart) > maxFractional) ? integralPart + maxFractional : adjustedSignificand).ToString(formatString), prefix.TrimEnd('a'));

        else if	(m_current_language == "spanish")
				 return string.Format("{0} {1}llon", (((adjustedSignificand - integralPart) > maxFractional) ? integralPart + maxFractional : adjustedSignificand).ToString(formatString), prefix.TrimEnd('a'));

		else if (m_current_language == "korean")
				 return string.Format("{0} {1}", (((adjustedSignificand - integralPart) > maxFractional) ? integralPart + maxFractional : adjustedSignificand).ToString(formatString), prefix.TrimEnd('a'));


		else return null;
	}
}