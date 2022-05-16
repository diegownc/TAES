using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Diagnostics;
using System.Linq;
public class CerrarTerminal : MonoBehaviour
{
    // Start is called before the first frame update
    public void pou()
    {
        foreach (var process in Process.GetProcessesByName("python"))
        {
            process.Kill();
            UnityEngine.Debug.Log("Proceso cerrado");
        }
        foreach (var process in Process.GetProcessesByName("cmd"))
        {
            process.Kill();
            UnityEngine.Debug.Log("Proceso cerrado");
        }
    }
}
