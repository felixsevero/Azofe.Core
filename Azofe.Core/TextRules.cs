using System.Text.RegularExpressions;

namespace Azofe.Core;

public static class TextRules {

	public static string Normalize(string text, bool multiline) {
		const char noBreakSpace = '\u00A0', softHyphen = '\u00AD';

		Validate(text, multiline);
		text = text.Trim();
		text = new(text.Where(c => c != noBreakSpace && c != softHyphen).ToArray());
		text = Regex.Replace(text, "[ ]{2,}", " ", RegexOptions.Compiled);
		text = Regex.Replace(text, @"\r\n|\r", "\n", RegexOptions.Compiled);
		return text;
	}

	public static void Validate(string text, bool multiline) {
		const int maxCharacter = 255;

		ArgumentNullException.ThrowIfNull(text);
		for(int i = 0; i < text.Length; i += char.IsSurrogatePair(text, i) ? 2 : 1)
			ValidateCharacter(i, text, multiline);

		static void ValidateCharacter(int index, string text, bool multiline) {
			int codePoint = char.ConvertToUtf32(text, index);
			if(codePoint > maxCharacter)
				throw new ArgumentException($"O texto deve conter apenas caracteres latinos. O caractere 'U+{codePoint:X4}', na posição {index} do texto, é inválido.");
			bool newLine = codePoint == '\r' || codePoint == '\n';
			if(newLine && !multiline)
				throw new ArgumentException($"O texto deve estar em uma única linha. Há uma quebra de linha na posição {index} do texto.");
			if(!newLine && char.IsControl(text, index))
				throw new ArgumentException($"O texto é inválido, pois possui o caractere de controle 'U+{codePoint:X4}' na posição {index} do texto.");
		}
	}

}
