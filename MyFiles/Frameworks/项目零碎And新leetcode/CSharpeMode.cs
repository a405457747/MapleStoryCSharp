using System;
using System.Collections.Generic;
using UnityEngine;

namespace CallPalCatGames.CSharpeMode
{
    public struct Fish
    {
        public int Age { get; }
        public string Name { get; }

        public Fish(int age, string name) : this()
        {
            Age = age;
            Name = name;
        }
    }

    internal interface IJump
    {
        void Jump();
    }

    internal enum Direction
    {
        NULL,
        UP,
        LEFT,
        RIGHT,
        DOWN
    }

    public abstract class CatBase
    {
    }

    internal class Dog
    {
        internal int Age { get; set; }
        internal string Name { get; set; }
    }

    internal class Person
    {
        internal Person(int age, string name)
        {
            Age = age;
            Name = name;
        }

        internal int Age { get; }

        internal string Name { get; }

        internal Dog Dog { get; private set; }

        public Dog Dog2 { get; set; }
    }

    //private protected private_protected internal protected_internal public
    //struct IFoo Foo new() T2 class System.Enum System.Delegate
    internal class CSharpeMode : MonoBehaviour
    {
        private const int CLASS_COUNT = 3;
        private List<Person> _persons;
        private Dictionary<int, string> bookDic;
        internal Action EatCallback;

        internal static event Action OnEat;

        private void Start()
        {
            _persons = new List<Person>
            {
                new Person(name: "bill", age: 9),
                new Person(name: "bill2", age: 9),
                new Person(name: "bill3", age: 9),
                new Person(name: "bill4", age: 9)
            };

            bookDic = new Dictionary<int, string>
            {
                {1, "book1"},
                {2, "book2"}
            };

            _persons = new List<Person>
            {
                new Person(name: "bill", age: 9)
                {
                    Dog2 = new Dog {Name = "billdog", Age = 44}
                },
                new Person(2, "Tom")
                {
                    Dog = {Name = "Tomdog", Age = 99}
                },
                new Person(5, "je"),
                new Person(1, "bke")
            };

            /*_persons = new List<Person>()
            {
                new Person(name: "Bill", age: 9)
                {
                    Dog2 = new Dog {Name = "Billdog", Age = 55}
                },
                new Person(2, "Tom")
                {
                    Dog = {Name = "Tomdog", Age = 99}
                },
                new Person(1, "Cindy") { },
                new Person(5, "Jerry"),
            };*/

            var nameArray = new[] {"Tom", "Jerry"};
            print($"nameArray length:{nameArray.Length}");

            var fish = new Fish(3, "");
            print($"fish age:{fish.Age}");

            OnEat += MyEatRice;
            OnEat?.Invoke();

            EatCallback += MyEatRice;
            EatCallback?.Invoke();
        }

        private void MyEatRice()
        {
            Debug.Log(nameof(MyEatRice));
        }
    }
}