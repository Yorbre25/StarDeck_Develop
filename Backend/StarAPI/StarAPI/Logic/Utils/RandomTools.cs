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
    }