namespace Commons.Singleton
{
    /// <summary>
    /// @author Mayank Grover 
    /// </summary>
    public class Singleton<T> where T: Singleton<T>, new()
    {
        private static T instance;

        public static T Instance { get {
                if (instance == null) {
                    instance = new T();
                    instance.OnInitialized();
                }
                return instance;
            }
        }

        protected virtual void OnInitialized() { }
    }
}
