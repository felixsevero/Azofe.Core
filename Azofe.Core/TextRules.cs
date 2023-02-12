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
				throw new ArgumentException($"The text must contain only Latin characters. The character 'U+{codePoint:X4}', at position {index} of the text, is invalid.");
			bool newLine = codePoint == '\r' || codePoint == '\n';
			if(newLine && !multiline)
				throw new ArgumentException($"The text must be on a single line. There is a line break at position {index} of the text.");
			if(!newLine && char.IsControl(text, index))
				throw new ArgumentException($"The text is invalid. It contains the control character 'U+{codePoint:X4}' at position {index} of the text.");
		}
	}

}
