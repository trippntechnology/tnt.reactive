﻿namespace tnt.reactive;

public static class FlowHelpers
{
  public static Flow<R> combine<F1, F2, R>(Flow<F1> flow1, Flow<F2> flow2, Func<F1, F2, R> func)
  {
    F1? flowValue1 = default(F1);
    F2? flowValue2 = default(F2);
    Flow<R> flow = new Flow<R>();

    flow1.collect(value1 =>
    {
      flowValue1 = value1;
      flow.emit(func(value1, flowValue2));
    });
    flow2.collect(value2 =>
    {
      flowValue2 = value2;
      flow.emit(func(flowValue1, value2));
    });

    return flow;
  }
}
