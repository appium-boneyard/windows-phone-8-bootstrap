using System.Collections.Generic;

namespace AppiumWP8DesktopServer.Model
{
    public class SessionList
    {
        private List<Session> _sessions { get; set; }

        public int Count { get { return _sessions.Count; } }

        public SessionList()
        {
            _sessions = new List<Session>();
        }

        public Session CreateNewSession()
        {
            var session = new Session();
            _sessions.Add(session);
            return session;
        }

        public void EndSession(string sessionId)
        {
            Session session = GetSessionById(sessionId);
            if (null != session)
            {
                _sessions.Remove(session);
            }
        }

        public Session GetSessionById(string sessionId)
        {
            return _sessions.Find(x => x.ID == sessionId);
        }

        public Session[] ToArray()
        {
            return _sessions.ToArray();
        }
    }
}
