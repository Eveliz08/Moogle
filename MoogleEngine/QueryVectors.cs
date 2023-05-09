using System.Diagnostics;
using System.Text.Json;
using System.Text;
using System.Text.RegularExpressions;

class QueryVectors
{
    
    public static (float[], float[], string[]) QueryVector(string query) //Solo para el query
    {

       MoogleEngine.Moogle length = new MoogleEngine.Moogle();
        float[] vector = new float[length.getLengthDataBase()];
        float[] vectorSugerencias = new float[length.getLengthDataBase()];

        query = StaticMatrix.Cleaner(query);
        double valor_palabra = 1;

        string[] queryPecked = query.Split();


        for (int i = 0; i < queryPecked.Length; i++)
        {
            if (StaticMatrix.Vocabulary.ContainsKey(queryPecked[i]))
            {
               valor_palabra = SearchRequests(queryPecked[i]);
               queryPecked[i] = Regex.Replace(queryPecked[i], "\\p{P}", string.Empty); 
            
               vector[StaticMatrix.Key_Vocabulary(queryPecked[i])] = (float)(valor_palabra * Math.Log10((length.getTotalDoc() + 1) / TdocQuery(queryPecked[i])));
               vectorSugerencias[StaticMatrix.Key_Vocabulary(queryPecked[i])] = vector[StaticMatrix.Key_Vocabulary(queryPecked[i])] ;

               vectorSugerencias = AddSynonyms(queryPecked[i], vectorSugerencias);
            }
        }
        return(vector, vectorSugerencias, queryPecked);
    }


    private static double SearchRequests(string query)
    {
        if (query.IndexOf("!") == 0) { return 0; }
         if (query.IndexOf("^") == 0) { return 100; }
        int i = 0;
        while (query.IndexOf("*") == i) { i++; }
        return Math.Pow(2, i);
    }

    private static int TdocQuery (string a)
    {
        MoogleEngine.Moogle moogle = new MoogleEngine.Moogle();
        int D = 0;
       for(int k = 0; k < moogle.getTotalDoc(); k++)
       {
          if(moogle.getContent()[k].Contains(a))
          D++;
       } 
       return D; 
    }


    public static float[] AddSynonyms(string aPecked, float[] vectorSugerencias) //no defino query vector
    { 
        MoogleEngine.Moogle length = new MoogleEngine.Moogle();
        if(MoogleEngine.Moogle.Synonyms.ContainsKey(aPecked))
        {
            var sinonimos = MoogleEngine.Moogle.Synonyms[aPecked];
            for(int i = 0; i < sinonimos.Length; i++)
            {
                if(StaticMatrix.Vocabulary.ContainsKey(sinonimos[i]))
                   vectorSugerencias[StaticMatrix.Key_Vocabulary(sinonimos[i])] = 1/2 * (float)Math.Log10((double)(length.getTotalDoc() + 1)/TdocQuery(sinonimos[i]));
            }
        } 
        return vectorSugerencias;
    }
}