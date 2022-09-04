using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.Work();
        }
    }

    class Zoo
    {
        private static Random _random = new Random();
        private List<Aviary> _aviaries = new List<Aviary>();
        private int _minSizeAviary = 1;
        private int _maxSizeAviary = 8;
        private int _aviariesCount = 8;
        private const int CommandExit = 0;

        public Zoo()
        {
            AddAviaries();
        }

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                ShowInfo();
                int index = GetIndex();

                if (index == CommandExit)
                {
                    isWork = false;
                }
                else
                {
                    _aviaries[index - 1].ShowInfo();
                }

                Console.ReadKey(true);
            }
        }

        private void AddAviaries()
        {
            for (int i = 0; i < _aviariesCount; i++)
            {
                int size = _random.Next(_minSizeAviary, _maxSizeAviary + 1);
                _aviaries.Add(new Aviary(size));
            }
        }

        private int GetIndex()
        {
            int index = 0;
            bool success = false;
            bool isCorrect = false;

            while (isCorrect == false)
            {
                Console.WriteLine("Вольер под каким номером вы хотите посмотреть?");
                string userInput = Console.ReadLine();
                success = int.TryParse(userInput, out index);

                if (success && index >= 0 && index <= _aviaries.Count)
                {
                    isCorrect = true;
                }
                else
                {
                    Console.WriteLine("Такого вольера нет");
                }
            }

            return index;
        }

        private void ShowInfo()
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать в зоопарк!" +
                "\nК какому вольеру вы хотите подойти:");

            for (int i = 0; i < _aviaries.Count; i++)
            {
                Console.WriteLine($"Вольер №{i + 1}");
            }

            Console.WriteLine($"\nВведите {CommandExit} чтобы покинуть зоопарк \n");
        }
    }

    class Aviary
    {
        private static Random _random = new Random();
        private List<Animal> _animals = new List<Animal>();

        public Aviary(int size)
        {
            AddAnimals(size);
        }

        public void ShowInfo()
        {
            int maleCount = 0;
            int femaleCounr = 0;
            int firstIndex = 0;

            foreach (Animal animal in _animals)
            {
                if (animal.IsMale)
                {
                    maleCount++;
                }
                else
                {
                    femaleCounr++;
                }
            }

            Console.WriteLine($"В вольере содержатся {_animals[firstIndex].ClassName}. " +
                $"\nОни издают звуки: {_animals[firstIndex].Sound} " +
                $"\nВсего животных: {_animals.Count}" +
                $"\nСамцов: {maleCount}" +
                $"\nСамок: {femaleCounr}");
        }

        private void AddAnimals(int size)
        {
            List<Animal> animals = new List<Animal>();

            for (int i = 0; i < size; i++)
            {
                _animals.Add(CreateAnimal());
            }

            SetGenders();
            _animals.AddRange(animals);
        }

        private Animal CreateAnimal()
        {
            Animal[] animalsTypes = { new Lion(), new Buffalo(), new Parrot(), new Bear(), new Wolf() };

            if (_animals.Count == 0)
            {
                return animalsTypes[_random.Next(animalsTypes.Length)];
            }
            else
            {
                int firstIndex = 0;

                foreach (Animal animal in animalsTypes)
                {
                    if (animal.ClassName == _animals[firstIndex].ClassName)
                    {
                        return animal;
                    }
                }
            }

            return null;
        }

        private void SetGenders()
        {
            foreach (Animal animal in _animals)
            {
                animal.SetGender();
            }
        }
    }

    class Animal
    {
        private static Random _random = new Random();

        public string ClassName { get; private set; }
        public string Sound { get; private set; }
        public bool IsMale { get; private set; }

        public Animal(string сlassName, string sound)
        {
            ClassName = сlassName;
            Sound = sound;
            IsMale = true;
        }

        public void SetGender()
        {
            int gendersNumber = 2;
            IsMale = Convert.ToBoolean(_random.Next(gendersNumber));
        }
    }

    class Lion : Animal
    {
        public Lion() : base("Львы", "Рычат") { }
    }

    class Buffalo : Animal
    {
        public Buffalo() : base("Буйволы", "Мычат и бормочат") { }
    }

    class Parrot : Animal
    {
        public Parrot() : base("Попугаи", "Поют на все голоса, повторяют слова за посетителями зоопарка") { }
    }

    class Bear : Animal
    {
        public Bear() : base("Медведи", "Ревут") { }
    }

    class Wolf : Animal
    {
        public Wolf() : base("Волки", "Воют") { }
    }
}
