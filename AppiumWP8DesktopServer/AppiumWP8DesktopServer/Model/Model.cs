namespace AppiumWP8DesktopServer.Model
{
    public class Model
    {
        public Model()
        {
            Sessions = new SessionList();
        }

        public SessionList Sessions { get; private set; }


    }
}
