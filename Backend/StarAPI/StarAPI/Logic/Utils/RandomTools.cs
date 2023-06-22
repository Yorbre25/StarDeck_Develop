using System;
using System.Security.Cryptography;
using System.Text;

namespace StarAPI.Logic.Utils;

    public class RandomTools
    {

        public T GetRandomElement<T>(List<T> list)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, list.Count);
            return list[randomIndex];
        }

        public T GetRandomElement<T>(T[] array)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, array.Length);
            return array[randomIndex];
        }
    }