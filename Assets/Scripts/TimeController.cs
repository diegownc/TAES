using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] int min, seg;
    [SerializeField] Text textTime;

    private float timeRest;
    private bool run;

    private void Awake()
    {
        timeRest = (min * 60) + seg;
        run = true;
    }

    void Update()
    {
        if (run)
        {
            timeRest -= Time.deltaTime;
            
            if(timeRest < 0)
            {
                run = false;
                //Escena Final o TerminarEjecución y comprobar quien ha ganado
                //Hacer static goal1 y goal2 para comprobar aquí eso.

                //Ideas repararBugs:
                //Adaptar canvas a todas las pantallas.
                //Para que no se haga bug los player, poner 1 seg de delay, parar el tiempo si marcan, posicionar los players y jugar.
                //O ver como poder bloquear todos los botones para que cuando se marque no se toque nada y empiezen normal.

                //Bug del salto, pasa cuando se acerca a la portería y marca y se vuelve a acercar, colisiona el triger de a bajo con el triger de la portería.
            }

            int timeMin = Mathf.FloorToInt(timeRest / 60);
            int timeSeg = Mathf.FloorToInt(timeRest % 60);
            textTime.text = string.Format("{00:00}:{01:00}", timeMin, timeSeg);
        }
        

    }
}
