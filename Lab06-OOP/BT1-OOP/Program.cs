using System;
using System.Collections.Generic;
using System.Text;

public abstract class ABicycle
{
    public string id;
    public string color;
    public double price;

    public ABicycle()
    {
        id = "Default";
        color = "Default";
        price = 0.0;
    }

    public ABicycle(string id, string color, double price)
    {
        this.id = id;
        this.color = color;
        this.price = price;
    }

    public abstract void Print();

    public void MakeDeal(string searchId)
    {
        if (this.id == searchId)
        {
            this.price = this.price - this.price * 15/100;
            Console.WriteLine("Áp dụng 15% khuyến mãi cho xe đạp mã: " + id);
        }
    }

    public double GetPrice()
    {
        return price;
    }
}

public class UsualBicycle : ABicycle
{
    private bool utility;

    public UsualBicycle() : base()
    {
        utility = false;
    }

    public UsualBicycle(string id, string color, double price, bool utility)
        : base(id, color, price)
    {
        this.utility = utility;
    }

    public override void Print()
    {
        Console.WriteLine("Usual Bicycle [ID: " + id + ", Color: " + color +
            ", Price: " + price + ", Utility: " + (utility ? "Yes" : "No") + "]");
    }
}

public class SpeedBicycle : ABicycle
{
    private int speedrate;

    public SpeedBicycle() : base()
    {
        speedrate = 0;
    }

    public SpeedBicycle(string id, string color, double price, int speedrate)
        : base(id, color, price)
    {
        this.speedrate = speedrate;
    }

    public override void Print()
    {
        Console.WriteLine("Speed Bicycle [ID: " + id + ", Color: " + color +
            ", Price: " + price + ", Speed Rate: " + speedrate + "]");
    }
}

public class ClimpBicycle : ABicycle
{
    private int climprate;

    public ClimpBicycle() : base()
    {
        climprate = 0;
    }

    public ClimpBicycle(string id, string color, double price, int climprate)
        : base(id, color, price)
    {
        this.climprate = climprate;
    }

    public override void Print()
    {
        Console.WriteLine("Climp Bicycle [ID: " + id + ", Color: " + color +
            ", Price: " + price + ", Climp Rate: " + climprate + "]");
    }
}

public class Store
{
    private List<ABicycle> bicycles;
    private Random random;
    private string[] colors = { "Red", "Blue", "Green", "Yellow", "Black" };

    public Store()
    {
        bicycles = new List<ABicycle>();
        random = new Random();
        GenerateRandomBicycles();
    }

    private void GenerateRandomBicycles()
    {
        for (int i = 0; i < 10; i++)
        {
            string id = "BikeNum" + (i + 1);
            string color = colors[random.Next(colors.Length)];
            double price = random.Next(100, 1000);
            int type = random.Next(3);

            switch (type)
            {
                case 0:
                    bicycles.Add(new UsualBicycle(id, color, price, random.Next(2) == 1));
                    break;
                case 1:
                    bicycles.Add(new SpeedBicycle(id, color, price, random.Next(1, 6)));
                    break;
                case 2:
                    bicycles.Add(new ClimpBicycle(id, color, price, random.Next(1, 6)));
                    break;
            }
        }
    }

    public void SearchByPriceRange(double from, double to)
    {
        Console.WriteLine("\nXe đạp giá trong khoảng " + from + " - " + to + ":");
        for (int i = 0; i < bicycles.Count; i++)
        {
            if (bicycles[i].GetPrice() >= from && bicycles[i].GetPrice() <= to)
            {
                bicycles[i].Print();
            }
        }
    }

    public void SortByPrice(bool ascending)
    {
        for (int i = 0; i < bicycles.Count - 1; i++)
        {
            for (int j = 0; j < bicycles.Count - i - 1; j++)
            {
                if (ascending)
                {
                    if (bicycles[j].GetPrice() > bicycles[j + 1].GetPrice())
                    {
                        ABicycle temp = bicycles[j];
                        bicycles[j] = bicycles[j + 1];
                        bicycles[j + 1] = temp;
                    }
                }
                else
                {
                    if (bicycles[j].GetPrice() < bicycles[j + 1].GetPrice())
                    {
                        ABicycle temp = bicycles[j];
                        bicycles[j] = bicycles[j + 1];
                        bicycles[j + 1] = temp;
                    }
                }
            }
        }
    }

    public void PrintAll()
    {
        for (int i = 0; i < bicycles.Count; i++)
        {
            bicycles[i].Print();
        }
    }

    public List<ABicycle> GetBicycles()
    {
        return bicycles;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;

        Store store = new Store();
        Console.WriteLine("Bảng xe đạp:");
        store.PrintAll();

        store.SearchByPriceRange(300, 600);

        store.SortByPrice(true);
        Console.WriteLine("\nSắp xếp theo giá tăng dần:");
        store.PrintAll();

        store.SortByPrice(false);
        Console.WriteLine("\nSắp xếp theo giá giảm dần:");
        store.PrintAll();

        Console.WriteLine("\n---Áp mã khuyến mãi---");
        for (int i = 0; i < store.GetBicycles().Count; i++)
        {
            store.GetBicycles()[i].MakeDeal("BikeNum2");
        }
        Console.WriteLine("-----------------------");

        // Print updated prices after discount
        Console.WriteLine("\nBảng giá xe sau khi áp mã khuyến mãi:");
        store.PrintAll();

        

        
    }
}