using System;

class MyClass
{
    // Fields
    private string name;
    private int age;

    // Constructor
    public MyClass(string name, int age)
    {
        this.name = name;
        this.age = age;
    }

    // Method to display information
    public void DisplayInfo()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Age: " + age);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating an instance of MyClass with constructor arguments
        MyClass obj = new MyClass("John", 30);

        // Calling the DisplayInfo method to display the information
        obj.DisplayInfo();
    }
}
