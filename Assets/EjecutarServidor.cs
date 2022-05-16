using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System;
using System.Linq;
using System.Text;
using System.IO;

public class EjecutarServidor : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    public static void OnLoad()
    {
        var proc1 = new ProcessStartInfo();
        string anyCommand; 
        proc1.UseShellExecute = true;

        proc1.WorkingDirectory = @"C:\Windows\System32";
        proc1.FileName = @"C:\Windows\System32\cmd.exe";
        //proc1.WindowStyle = ProcessWindowStyle.Hidden;
        proc1.Arguments = "/k title ServidorJuego & cd " + $"{Application.dataPath} & python server.py";
        Process.Start(proc1);
    }
}
