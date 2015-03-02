// This contains various utility classes
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tuple<T1, T2> {
	public T1 first { get; set; }
	public T2 second { get; set; }
	internal Tuple(T1 _first, T2 _second) {
		first = _first;
		second = _second;
	}
}

[System.Serializable]
public static class Tuple {
	public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second) {
		var tuple = new Tuple<T1, T2>(first, second);
		return tuple;
	}
}