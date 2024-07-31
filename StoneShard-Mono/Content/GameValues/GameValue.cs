namespace StoneShard_Mono.Content.GameValues
{
    public class GameValue<T>
    {
        public T Value 
        { 
            get
            {
                PreGetValue(ref _value);
                return _value;
            }
            set
            {
                PreSetValue(ref _value);
                _value = value;
                PostSetValue(ref _value);
            }
        }

        private T _value;

        public delegate void GameDelegate(ref T value);

        public event GameDelegate PreGetValue;

        public event GameDelegate PreSetValue;
        public event GameDelegate PostSetValue;
    }
}
