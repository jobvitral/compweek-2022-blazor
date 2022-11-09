using System.Text;

namespace CompWeek.Domain.Commons;

public class StringHelper
{
    public static string GetUrlName(string? name)
    {
        if (string.IsNullOrEmpty(name))
            return Guid.NewGuid().ToString();

        // remove especial characters
        var urlName = GetWithoutSpecialCharacters(name.ToLower());
        
        // replace white spaces
        urlName = urlName.Replace("^\\s+", "-");
        urlName = urlName.Replace("\\s+$", "-");
        urlName = urlName.Replace("\\s+", "-");

        return urlName;
    }
    
    public static string GetWithoutSpecialCharacters(string str)
    {
        string[] accentsLetters = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
        string[] commonLetters = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
 
        for (int i = 0; i < accentsLetters.Length; i++)
        {
            str = str.Replace(accentsLetters[i], commonLetters[i]);
        }
        
        string[] specialCharacters = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };
 
        for (int i = 0; i < specialCharacters.Length; i++)
        {
            str = str.Replace(specialCharacters[i], "");
        }

        return str;
    }
    
    static public string EncodeToBase64(string texto)
    {
        try
        {
            byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
            string resultado = System.Convert.ToBase64String(textoAsBytes);
            return resultado;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
