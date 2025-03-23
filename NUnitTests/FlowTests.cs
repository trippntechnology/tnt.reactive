using System.Diagnostics.CodeAnalysis;
using TNT.Reactive;

namespace NUnitTests;

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

    Flow<List<string>?> mapResultFlow = flow.map(value =>
    {
      return value?.Split(',').ToList() ?? new List<string>();
    });

    mapResultFlow.collect(value =>
    {
      result = value;
    });

    flow.emit("one,two,three");

    Assert.That(result, Is.Not.Null);
    Assert.That(result, Is.EqualTo(new List<string>() { "one", "two", "three" }));
  }

  [Test]
  public void StateFlowTest()
  {
    StateFlow<string> stateFlow = new StateFlow<string>("initial value");
    var stateFlowValues = new List<string?>();
    var flowValues = new List<string?>();

    Flow<string?> flow = stateFlow.map(value =>
    {
      stateFlowValues.Add(value);
      return $"-{value}-";
    });

    Assert.That(stateFlow.value, Is.EqualTo("initial value"));

    flow.collect(value =>
    {
      flowValues.Add(value);
    });

    stateFlow.emit("new value");
    Assert.That(stateFlow.value, Is.EqualTo("new value"));
    Assert.That(stateFlowValues, Is.EqualTo(new List<string>() { "initial value", "new value" }));
    Assert.That(flowValues, Is.EqualTo(new List<string>() { "-new value-" }));
  }

  [Test]
  public void MutableStateFlowTest()
  {
    MutableStateFlow<string?> mutableStateFlow = new MutableStateFlow<string?>(null);
    var msfValues = new List<string?>();
    var flowValues = new List<string?>();

    Flow<string?> flow = mutableStateFlow.map(value =>
    {
      msfValues.Add(value);
      return value ?? "";
    });

    flow.collect(value =>
    {
      flowValues.Add(value);
    });

    mutableStateFlow.value = "one";
    Assert.That(mutableStateFlow.value, Is.EqualTo("one"));
    mutableStateFlow.value = "two";
    Assert.That(mutableStateFlow.value, Is.EqualTo("two"));
    mutableStateFlow.value = "three";
    Assert.That(mutableStateFlow.value, Is.EqualTo("three"));

    Assert.That(msfValues, Is.EqualTo(new List<string?>() { null, "one", "two", "three" }));
    Assert.That(flowValues, Is.EqualTo(new List<string>() { "one", "two", "three" }));
  }

  [Test]
  public void AsStateFlowTest()
  {
    Flow<string> flow = new Flow<string>();

    StateFlow<string?> stateFlow = flow.map(value =>
    {
      return value;
    }).asStateFlow();

    flow.emit("one");
    Assert.That(stateFlow.value, Is.EqualTo("one"));
    flow.emit("two");
    Assert.That(stateFlow.value, Is.EqualTo("two"));
    flow.emit("three");
    Assert.That(stateFlow.value, Is.EqualTo("three"));
  }

  [Test]
  public void CombineTest()
  {
    Flow<string> flow1 = new Flow<string>();
    Flow<string> flow2 = new Flow<string>();
    var flowValues = new List<string?>();

    var flow3 = flow1.combine(flow2, (f1, f2) =>
    {
      return $"{f1} {f2}";
    });

    flow3.collect(value => flowValues.Add(value));

    flow1.emit("first");
    flow2.emit("second");
    flow1.emit("third");

    Assert.That(flowValues, Is.EqualTo(new List<string>() { "first ", "first second", "third second" }));
  }
}