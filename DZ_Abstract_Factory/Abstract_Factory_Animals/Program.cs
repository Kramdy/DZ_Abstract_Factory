using System;

namespace AbstractFactoryAnimals
{
    internal abstract class Herbivore
    {
        public int Weight { get; protected set; }
        public bool Life { get; set; }
        public abstract string Name { get; }
        protected Herbivore(int weight) { Weight = weight; Life = true; }
        public void EatGrass() { if (Life) { Weight += 10; Console.WriteLine(Name + " ест траву. Вес: " + Weight); } }
    }
    internal abstract class Carnivore
    {
        public int Power { get; protected set; }
        public abstract string Name { get; }
        protected Carnivore(int power) { Power = power; }
        public void Eat(Herbivore herbivore)
        {
            if (!herbivore.Life) { Console.WriteLine(Name + ": добыча уже не жива."); return; }
            if (Power > herbivore.Weight) { Power += 10; herbivore.Life = false; Console.WriteLine(Name + " съел " + herbivore.Name + ". Сила: " + Power); }
            else if (Power < herbivore.Weight) { Power -= 10; Console.WriteLine(Name + " не справился с " + herbivore.Name + ". Сила: " + Power); }
            else Console.WriteLine(Name + " и " + herbivore.Name + " равны по силе и весу.");
        }
    }

    internal class Wildebeest : Herbivore { public Wildebeest() : base(70) { } public override string Name { get { return "Антилопа Гну"; } } }
    internal class Bison : Herbivore { public Bison() : base(120) { } public override string Name { get { return "Бизон"; } } }
    internal class Elk : Herbivore { public Elk() : base(100) { } public override string Name { get { return "Лось"; } } }
    internal class Lion : Carnivore { public Lion() : base(90) { } public override string Name { get { return "Лев"; } } }
    internal class Wolf : Carnivore { public Wolf() : base(80) { } public override string Name { get { return "Волк"; } } }
    internal class Tiger : Carnivore { public Tiger() : base(130) { } public override string Name { get { return "Тигр"; } } }

    internal abstract class Continent
    {
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();
        public abstract string Name { get; }
    }
    internal class Africa : Continent
    {
        public override string Name { get { return "Африка"; } }
        public override Herbivore CreateHerbivore() { return new Wildebeest(); }
        public override Carnivore CreateCarnivore() { return new Lion(); }
    }
    internal class NorthAmerica : Continent
    {
        public override string Name { get { return "Северная Америка"; } }
        public override Herbivore CreateHerbivore() { return new Bison(); }
        public override Carnivore CreateCarnivore() { return new Wolf(); }
    }
    internal class Eurasia : Continent
    {
        public override string Name { get { return "Евразия"; } }
        public override Herbivore CreateHerbivore() { return new Elk(); }
        public override Carnivore CreateCarnivore() { return new Tiger(); }
    }

    internal class AnimalWorld
    {
        private Herbivore herbivore;
        private Carnivore carnivore;
        public AnimalWorld(Continent continent)
        {
            herbivore = continent.CreateHerbivore();
            carnivore = continent.CreateCarnivore();
            Console.WriteLine("\nКонтинент: " + continent.Name + ". Травоядное: " + herbivore.Name + ", хищник: " + carnivore.Name);
        }
        public void MealsHerbivores() { herbivore.EatGrass(); }
        public void NutritionCarnivores() { carnivore.Eat(herbivore); }
    }

    internal class Program
    {
        private static void Main()
        {
            // Задание. Постройте программу на паттерне «Абстрактная фабрика»:
            // Continent — абстрактная фабрика, Africa и North America — конкретные
            // фабрики. Herbivore и Carnivore — абстрактные продукты. Гну/лев,
            // бизон/волк — конкретные продукты. AnimalWorld управляет кормлением.
            // Дополните Евразией: лось и тигр.
            Continent[] continents = { new Africa(), new NorthAmerica(), new Eurasia() };
            foreach (Continent continent in continents)
            {
                AnimalWorld world = new AnimalWorld(continent);
                world.MealsHerbivores();
                world.NutritionCarnivores();
            }
            Console.ReadKey();
        }
    }
}
