namespace Helicopter.Analytics
{
  internal static class Api
  {
    public static void LogEvent(string eventName, bool timed = false) { }
    public static void LogEvent(string eventName, System.Collections.Generic.IEnumerable<Parameter> parameters, bool timed = false) { }
    public static void EndTimedEvent(string eventName) { }
  }
}
