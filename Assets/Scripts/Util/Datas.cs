
namespace Datas
{
    [System.Serializable]
    public class Pair<T1,T2> 
    {
        public T1 key;
        public T2 value;

        public Pair(T1 _key, T2 _value) 
        { 
            key = _key;
            value = _value;
        }
    }
}
