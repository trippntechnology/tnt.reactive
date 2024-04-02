using System.Diagnostics.CodeAnalysis;
using tnt.reactive;

namespace tests;

[ExcludeFromCodeCoverage]
public class FlowTests
{
  [Test]
  public void CollectTest()
  {
    Flow<string> flow = new Flow<string>();
    string? result = null;

    flow.collect(value =>
    {
      result = value;
    });

    flow.emit("expected result");

    Assert.That(result, Is.Not.Null);
    Assert.That(result, Is.EqualTo("expected result"));
  }

  [Test]
  public void MapTest()
  {
    Flow<string?> flow = new Flow<string?>();
    List<string>? result = null;

    Flow<List<string>> mapResultFlow = flow.map(value =>
    {
      return value?.Split(',').ToList() ?? new List<string>();
    });

    mapResultFlow.collect(value =>
    {
      result = value;
    });

    flow.emit("one,two,three");

    Assert.That(result, Is.Not.Null);
    CollectionAssert.AreEqual(result, new List<string>() { "one", "two", "three" });

  }
}