#define USE_LOGS
using UnityEngine;
using System;
using System.Diagnostics;

public class Trace {

	[Conditional("USE_LOGS")]
	public static void Msg (string msg) {
		UnityEngine.Debug.Log (msg);
	}
}
