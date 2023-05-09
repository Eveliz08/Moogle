namespace MoogleEngine;
using System.Text.Json;
using System;
public class Moogle
{
  public static void OpenDataBase()
  {
    files = Directory.GetFiles("C:\\Users\\Eveliz\\Desktop\\moogle-main\\Content");
    totalDoc =  files.Length; 

    content = new string[totalDoc];
    lengthDoc = new int[totalDoc];
     
    for (int i = 0; i < totalDoc; i++)
   {
      content[i] = File.ReadAllText(files[i]);
      lengthDoc[i] = content[i].Split(' ').Length;
      lengthDataBase += lengthDoc[i]; //Se introduce la cantidad de palabras de cada doc, el mayor será la dimensión de la matriz y el vector query
    }  

    textSplitter = new float[totalDoc, lengthDataBase];  
    Synonyms = JsonSerializer.Deserialize<Dictionary<string, string[]>>(File.ReadAllText("C:\\Users\\Eveliz\\Desktop\\moogle-main\\MoogleEngine\\Synonymous.json"))!;
  }

 
    private static string[] files;
    private static string[] content;
    public String[] getContent() {return content;}  
    private static int lengthDataBase;
    public int getLengthDataBase() {return lengthDataBase;}
    private static int[] lengthDoc; 
     public int[] getLengthDoc() {return lengthDoc;}
    private static int totalDoc;
    public int getTotalDoc() {return totalDoc;}
    public static float[,] textSplitter; 
   public static Dictionary<string, string[]> Synonyms;
   private static int executionsProgram = 0;




    public static  SearchResult Query(string query) 
    {
      if(executionsProgram == 0)
      {
        OpenDataBase();
        StaticMatrix.ToDoMatrix(content);// Modifique este método para responder a la búsqueda
      }
      executionsProgram++;

      

      var vectors = QueryVectors.QueryVector(query);

      float[] scoreQuery = OperationsVectors.Multiplication(vectors.Item1);
      float[] scoreSuggestion = OperationsVectors.Multiplication(vectors.Item2);
        

        float mediator = 0;
        int position = 0;
        for(int k = 0; k <scoreQuery.Length; k++)
        {
          if(mediator < scoreQuery[k])
          {
          mediator = scoreQuery[k];
          position = k;
          }
        }


        float mediator2 = 0;
        int position2 = 0;
         for(int k = 0; k < scoreSuggestion.Length && k!= position; k++)
        {
          if(mediator2 < scoreSuggestion[k])
          {
          mediator2 = scoreSuggestion[k];
          position2 = k;
          }
        }


        SearchItem[] items = new SearchItem[2] 
        {
            new SearchItem(files[position], Snippet(position, vectors.Item3), mediator),
            new SearchItem(files[position2], Snippet(position2, vectors.Item3),mediator2),
        };
         return new SearchResult(items, query);
    }

  private static string Snippet(int position, string[] queryPecked )
  {
    string snippet = File.ReadAllText(files[position]);

    int[] appearance = new int[queryPecked.Length];
    int min = int.MaxValue;
    int max = -1;
   

    for(int i = 0; i < queryPecked.Length; i++)
    {
      appearance[i] = content[position].IndexOf(queryPecked[i]);
      if(appearance[i] >= 0 && appearance[i] < min) {min = appearance[i];}
      if(appearance[i] > max) {max = appearance[i];}
    }
    

   
   min = snippet.LastIndexOf('.', min);
   if(min < 0) {min = 0;}
   else min ++;
   
   max = snippet.IndexOf('.', max);
   if(max < 0) {max = snippet.Length;}

   snippet = snippet.Substring(min, max);

   return snippet;
  }
}