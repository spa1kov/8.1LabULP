
using System;

abstract class Length
{
    public abstract Length Add(Length other);
    public abstract Length Subtract(Length other);
    public abstract Length Multiply(int scalar);
    public abstract Length Divide(int divisor);
    public abstract bool LessThan(Length other);
    public abstract bool GreaterThan(Length other);
    public abstract override string ToString();
}

class EngMer : Length
{
    public int Pounds { get; set; }
    public int Inches { get; set; }

    public EngMer(int pounds, int inches)
    {
        Pounds = pounds;
        Inches = inches;
    }

    public override Length Add(Length other)
    {
        EngMer eng2 = (EngMer)other;
        int totalPounds = Pounds + eng2.Pounds;
        int totalInches = Inches + eng2.Inches;
        if (totalInches >= 12)
        {
            totalPounds++;
            totalInches -= 12;
        }
        return new EngMer(totalPounds, totalInches);
    }

    public override Length Subtract(Length other)
    {
        EngMer eng2 = (EngMer)other;
        int totalPounds = Pounds - eng2.Pounds;
        int totalInches = Inches - eng2.Inches;
        if (totalInches < 0)
        {
            totalPounds--;
            totalInches += 12;
        }
        return new EngMer(totalPounds, totalInches);
    }

    public override Length Multiply(int scalar)
    {
        int totalPounds = Pounds * scalar;
        int totalInches = Inches * scalar;
        totalPounds += totalInches / 12;
        totalInches %= 12;
        return new EngMer(totalPounds, totalInches);
    }

    public override Length Divide(int divisor)
    {
        int totalPounds = Pounds / divisor;
        int totalInches = Inches / divisor;
        if (totalInches >= 12)
        {
            totalPounds++;
            totalInches -= 12;
        }
        return new EngMer(totalPounds, totalInches);
    }

    public override bool LessThan(Length other)
    {
        EngMer eng2 = (EngMer)other;
        int totalInches1 = Pounds * 12 + Inches;
        int totalInches2 = eng2.Pounds * 12 + eng2.Inches;
        return totalInches1 < totalInches2;
    }

    public override bool GreaterThan(Length other)
    {
        EngMer eng2 = (EngMer)other;
        int totalInches1 = Pounds * 12 + Inches;
        int totalInches2 = eng2.Pounds * 12 + eng2.Inches;
        return totalInches1 > totalInches2;
    }

    public override string ToString()
    {
        return $"{Pounds} фунтов {Inches} дюймов";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите первую длину (фунты дюймы):");
        string[] input1 = Console.ReadLine().Split(' ');
        int pounds1 = int.Parse(input1[0]);
        int inches1 = int.Parse(input1[1]);

        Console.WriteLine("Введите вторую длину (фунты дюймы):");
        string[] input2 = Console.ReadLine().Split(' ');
        int pounds2 = int.Parse(input2[0]);
        int inches2 = int.Parse(input2[1]);

        Length length1 = new EngMer(pounds1, inches1);
        Length length2 = new EngMer(pounds2, inches2);

        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Сложение");
        Console.WriteLine("2. Вычитание");
        Console.WriteLine("3. Умножение");
        Console.WriteLine("4. Деление");
        Console.WriteLine("5. Сравнение");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("Результат сложения: " + (length1.Add(length2)));
                break;
            case 2:
                Console.WriteLine("Результат вычитания: " + (length1.Subtract(length2)));
                break;
            case 3:
                Console.WriteLine("Введите множитель:");
                int multiplier = int.Parse(Console.ReadLine());
                Console.WriteLine("Результат умножения: " + (length1.Multiply(multiplier)));
                break;
            case 4:
                Console.WriteLine("Введите делитель:");
                int divisor = int.Parse(Console.ReadLine());
                Console.WriteLine("Результат деления: " + (length1.Divide(divisor)));
                break;
            case 5:
                if (length1.LessThan(length2))
                    Console.WriteLine("Первая длина меньше второй");
                else if (length1.GreaterThan(length2))
                    Console.WriteLine("Первая длина больше второй");
                else
                    Console.WriteLine("Длины равны");
                break;
            default:
                Console.WriteLine("Неверный выбор операции.");
                break;
        }
    }
}