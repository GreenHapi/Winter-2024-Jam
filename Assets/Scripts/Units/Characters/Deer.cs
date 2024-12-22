namespace WinterJam.Units.Characters
{
    public class Deer : Character
    {
        public static Deer Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}