namespace CompWeek.Domain.Commons;

public class DocumentHelper
{
    public static string GetDocumentType(string? document)
    {
        if(!string.IsNullOrEmpty(document))
            if(IsDocumentValid(document))
                if (IsCpf(document))
                    return "CPF";
                else
                    if(IsCnpj(document))
                        return "CNPJ";

        return "Nenhum";
    }

    public static bool IsDocumentValid(string document)
    {
        return (IsCpf(document) || IsCnpj(document));
    }

    public static bool IsCpf(string document)
    {
        int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        document = document.Trim().Replace(".", "").Replace("-", "");
        
        if (document.Length != 11)
            return false;

        for (int j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == document)
                return false;

        string hasCpf = document.Substring(0, 9);
        int sumResult = 0;

        for (int i = 0; i < 9; i++)
            sumResult += int.Parse(hasCpf[i].ToString()) * multiplier1[i];

        int restResult = sumResult % 11;
        if (restResult < 2)
            restResult = 0;
        else
            restResult = 11 - restResult;

        string digitResult = restResult.ToString();
        hasCpf = hasCpf + digitResult;
        sumResult = 0;
        for (int i = 0; i < 10; i++)
            sumResult += int.Parse(hasCpf[i].ToString()) * multiplier2[i];

        restResult = sumResult % 11;
        if (restResult < 2)
            restResult = 0;
        else
            restResult = 11 - restResult;

        digitResult = digitResult + restResult.ToString();

        return document.EndsWith(digitResult);
    }

    public static bool IsCnpj(string document)
    {
        int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        document = document.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
        
        if (document.Length != 14)
            return false;

        string hasCnpj = document.Substring(0, 12);
        int sumResult = 0;

        for (int i = 0; i < 12; i++)
            sumResult += int.Parse(hasCnpj[i].ToString()) * multiplier1[i];

        int restResult = (sumResult % 11);
        if (restResult < 2)
            restResult = 0;
        else
            restResult = 11 - restResult;

        string digitResult = restResult.ToString();
        hasCnpj = hasCnpj + digitResult;
        sumResult = 0;
        for (int i = 0; i < 13; i++)
            sumResult += int.Parse(hasCnpj[i].ToString()) * multiplier2[i];

        restResult = (sumResult % 11);
        if (restResult < 2)
            restResult = 0;
        else
            restResult = 11 - restResult;

        digitResult = digitResult + restResult.ToString();

        return document.EndsWith(digitResult);
    }
}
