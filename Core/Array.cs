namespace Engine.Core
{
    public static class Array<T> where T : class
    {
        public static void Add(ref T[] aArray, T tElement)
        {
            T[] aNew = new T[aArray.Length + 1];
            aArray.CopyTo(aNew, 0);
            aNew[aNew.Length -1] = tElement;
            aArray = new T[aNew.Length];
            aNew.CopyTo(aArray, 0);
        }
        public static void Remove(ref T[] aArray, T tElement)
        {
            for(int i = 0; i < aArray.Length; i++)
            {
                if(EqualityComparer<T>.Default.Equals(tElement, aArray[i]))
                {
                    T[] aNew = new T[aArray.Length - 1];
                    Array.Copy(aArray, 0, aNew, 0, i - 1);
                    Array.Copy(aArray, i + 1, aNew, i, aNew.Length - i);
                    aArray = new T[aNew.Length];
                    aNew.CopyTo(aArray, 0);
                }
            }
        }
    }
}
