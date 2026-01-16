namespace Helicopter.Analytics
{
  internal class Parameter
  {
    public string Name { get; }
    public string Value { get; }

    public Parameter(string name, string value)
    {
      Name = name;
      Value = value;
    }
  }
}
