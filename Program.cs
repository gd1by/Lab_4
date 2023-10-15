using System;

class Matrix
{

    private int rows;
    private int columns;
    private double[,] elements;

    // Конструктор для создания матрицы
    public Matrix(int rows, int columns, double[] values)
    {
        this.rows = rows;
        this.columns = columns;
        this.elements = new double[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                elements[i, j] = values[i * columns + j];
            }
        }
    }

    // Метод для вывода матрицы
    public void PrintMatrix()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write(elements[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    // Метод для проверки, является ли матрица диагональной
    public bool IsDiagonalMatrix()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (i != j && elements[i, j] != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Метод для проверки, является ли матрица единичной
    public bool IsIdentityMatrix()
    {
        if (rows != columns) return false;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (i == j && elements[i, j] != 1)
                {
                    return false;
                }
                if (i != j && elements[i, j] != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Метод для проверки, является ли матрица нулевой
    public bool IsZeroMatrix()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (elements[i, j] != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Перегрузка оператора сложения
    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.rows != b.rows || a.columns != b.columns)
            throw new InvalidOperationException("Матрицы имеют разные размеры");

        double[] resultValues = new double[a.rows * a.columns];
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.columns; j++)
            {
                resultValues[i * a.columns + j] = a.elements[i, j] + b.elements[i, j];
            }
        }

        return new Matrix(a.rows, a.columns, resultValues);
    }

    // Перегрузка оператора вычитания
    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.rows != b.rows || a.columns != b.columns)
            throw new InvalidOperationException("Матрицы имеют разные размеры");

        double[] resultValues = new double[a.rows * a.columns];
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.columns; j++)
            {
                resultValues[i * a.columns + j] = a.elements[i, j] - b.elements[i, j];
            }
        }

        return new Matrix(a.rows, a.columns, resultValues);
    }

    // Перегрузка оператора умножения
    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.columns != b.rows)
            throw new InvalidOperationException("Неправильные размеры для умножения матриц");

        double[] resultValues = new double[a.rows * b.columns];

        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < b.columns; j++)
            {
                for (int k = 0; k < a.columns; k++)
                {
                    resultValues[i * b.columns + j] += a.elements[i, k] * b.elements[k, j];
                }
            }
        }

        return new Matrix(a.rows, b.columns, resultValues);
    }

    // Перегрузка оператора умножения на число
    public static Matrix operator *(Matrix a, double scalar)
    {
        double[] resultValues = new double[a.rows * a.columns];
        for (int i = 0; i < a.rows; i++)
        {
            for (int j = 0; j < a.columns; j++)
            {
                resultValues[i * a.columns + j] = a.elements[i, j] * scalar;
            }
        }

        return new Matrix(a.rows, a.columns, resultValues);
    }
    // Добавьте этот метод в класс Matrix
    public Matrix MultiplyByScalar(double scalar)
    {
        double[] resultValues = new double[rows * columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                resultValues[i * columns + j] = elements[i, j] * scalar;
            }
        }

        return new Matrix(rows, columns, resultValues);
    }

}

class Program
{
    static void Main()
    {
        double[] valuesA = { 1, 2, 2, 0, 3, 1, 1, 0, 0 };
        double[] valuesB = { 0, 0, 1, 0, 0, 1, 0, 0, -1 };

        Matrix A = new Matrix(3, 3, valuesA);
        Matrix B = new Matrix(3, 3, valuesB);

        // Выполняем операции, как указано в задаче
        Matrix D = (B * A).MultiplyByScalar(3) + (B - A);
        Console.WriteLine("Матрица D:");
        D.PrintMatrix();

        if (D.IsIdentityMatrix())
        {
            Console.WriteLine("Матрица D является единичной.");
        }
        else if (D.IsDiagonalMatrix())
        {
            Console.WriteLine("Матрица D является диагональной.");
        }
        else if (D.IsZeroMatrix())
        {
            Console.WriteLine("Матрица D является нулевой.");
        }
        else
        {
            Console.WriteLine("Матрица D не соответствует ни одному из указанных условий.");
        }
    }
}
