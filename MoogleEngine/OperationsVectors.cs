class OperationsVectors
{

    public static float[] Multiplication(float[] vector)
    {
        int D = 0;
        float[] multiplicacion = new float[MoogleEngine.Moogle.textSplitter.GetLength(0)];
        for (int i = 0; i < MoogleEngine.Moogle.textSplitter.GetLength(0) && i <= StaticMatrix.key; i++)
        {
            for (int j = 0; j < MoogleEngine.Moogle.textSplitter.GetLength(1); j++)
            {
                multiplicacion[i] += MoogleEngine.Moogle.textSplitter[i, j] * vector[j];
            }
             
            if (multiplicacion[i]!=0)
            D++;
        }
        return multiplicacion;
    }
}

