using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tnt.reactive;

public class StateFlow<T> : Flow<T>
{
	public T value { get => base.value; protected set => base.value = value; }

	public StateFlow(T value)
	{
		base.value = value;
	}
}
