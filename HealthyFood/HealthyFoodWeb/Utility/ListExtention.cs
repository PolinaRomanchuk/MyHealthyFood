using System.Runtime.CompilerServices;

namespace HealthyFoodWeb.Utility
{
    public static class ListExtention
    {
        private static Random random = new Random();

        public static T Random<T>(this List<T> list)
        {
            var randomIndex = random.Next(list.Count);
            return list[randomIndex];
        }

        public static T Random<T>(this IEnumerable<T> list)
        {
            return list.ToList().Random();
        }
    }
}
