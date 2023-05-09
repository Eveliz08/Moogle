using System;
using System.Text;
using System.Text.RegularExpressions;
class StaticMatrix
{

    public static void ToDoMatrix(string[] content) //recorre el array con la información
    {
        for (int i = 0; i < content.Length; i++)
        {
            content[i] = Cleaner(content[i]);
            Refill_Vocabulary(content[i], i);
        }
    }

    public static string Cleaner(string a) //Solo para los documetos
    {
        a = a.ToLower().Replace("\n", " "); //Se quitan mayúsculas y saltos de línea
        a = Regex.Replace(a, "\\p{P}", string.Empty); //Se quitan signos de puntación

        Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
        Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
        Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
        Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
        Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);

        a = replace_a_Accents.Replace(a, "a");
        a = replace_e_Accents.Replace(a, "e");
        a = replace_i_Accents.Replace(a, "i");
        a = replace_o_Accents.Replace(a, "o");
        a = replace_u_Accents.Replace(a, "u");

        return a;
    }


    public static void Refill_Vocabulary(string a, int i) //Solo para los documentos
    {
        string[] a_array = a.Split();  //En este punto ya está el codigo limpio de signos, mayusculas y tildes. Se divide

        for (int k = 0; k < a_array.Length; k++)
        {
            if (!Vocabulary.ContainsKey(a_array[k]))
            {
                Vocabulary.Add(a_array[k], key);
                key++;
            }
        }
        Refill_Matrix(a_array, i);
    }

    public static int Key_Vocabulary(string a)
    {
        return Vocabulary[a];
    }


    private static void Refill_Matrix(string[] matriz, int i)
    {
      MoogleEngine.Moogle length = new MoogleEngine.Moogle();
        for (int k = 0; k < matriz.Length; k++)
        MoogleEngine.Moogle.textSplitter[i, Key_Vocabulary(matriz[k])] += (float)1 / length.getLengthDoc()[i];

    }

    public static int key = 0;
    public static Dictionary<string, int> Vocabulary = new Dictionary<string, int>();
}

