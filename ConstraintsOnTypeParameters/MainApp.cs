using System;
using static System.Console;

namespace ConstraintsOnTypeParameters
{
    class StructArray<T> where T : struct
    {                           // T는 값형식이여야함
        public T[] Array{ get; set; } 
        public StructArray(int size) // 여기서 T(TypeParameter)는 int를 사용했네~
        {
            Array = new T[size];
        }
    }

    class RefArray<T> where T : class
    {                           // T는 참조형식이여야함
        public T[] Array { get; set; }
        public RefArray(int size)
        {
            Array = new T[size];
        }
    
    }

    class Base { }
    class Derived : Base { }
    class BaseArray<U> where U : Base
    {
        public U[] Array { get; set; }
        public BaseArray(int size)
        {
            Array = new U[size];
        }

        public void CopyArray<T>(T[] Source) where T : U
        {
            Source.CopyTo(Array, 0);
        }
    }
    
    class MainApp
    {
        public static T CreateInstance<T>() where T : new()
        {
            return new T();
        }

        static void Main(string[] args)
        {              
            // T[] Array = new T[3]
            StructArray<int> a = new StructArray<int>(3);
            a.Array[0] = 0;
            a.Array[1] = 1;
            a.Array[2] = 2;
            foreach (var aa in a.Array)
            {
                Write($"{aa} ");
            }

            WriteLine();

            RefArray<StructArray<double>> b = new RefArray<StructArray<double>>(3);
            b.Array[0] = new StructArray<double>(1);
            b.Array[1] = new StructArray<double>(10);
            b.Array[2] = new StructArray<double>(1000);
            for (int i = 0; i < b.Array.Length; i++)
            {
                Write($"b.Array[{i}]: ");
                foreach (double x in b.Array[i].Array)
                    Write(x + " ");
                WriteLine();
            }
            WriteLine();
            BaseArray<Base> c = new BaseArray<Base>(3);
            c.Array[0] = new Base();
            c.Array[1] = new Derived();
            c.Array[2] = CreateInstance<Base>();
            foreach (var cc in c.Array)
            {
                Write($"{cc} ");
            }
            WriteLine();

            BaseArray<Derived> d = new BaseArray<Derived>(3);
            d.Array[0] = new Derived(); // Base형식은 못 옴 // Derived:Base
            d.Array[1] = CreateInstance<Derived>();
            d.Array[2] = CreateInstance<Derived>();

            BaseArray<Derived> e = new BaseArray<Derived>(3);
            e.CopyArray<Derived>(d.Array);
        }
    }
}