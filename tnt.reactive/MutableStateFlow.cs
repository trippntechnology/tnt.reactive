using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tnt.reactive;

public class MutableStateFlow<T> : StateFlow<T>
{

	public T value { get => base.value; set => base.value = value; }

	public MutableStateFlow(T initialValue) : base(initialValue) { }
}
