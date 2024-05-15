namespace StoneShard_Mono.UIComponents
{
    public class Space : Component
    {
        public Space(int width, int height) 
        {
            _width = width;
            _height = height;
        }

        internal int _width;

        internal int _height;

        public override int Height => _height;

        public override int Width => _width;
    }
}
