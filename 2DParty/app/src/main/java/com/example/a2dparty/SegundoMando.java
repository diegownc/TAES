package com.example.a2dparty;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Button;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

public class SegundoMando extends AppCompatActivity {
    private String ipAddress;
    private int puerto = 8050;
    Button btn;
    Button btnPush;
    private DatagramSocket socketUDP;
    private DatagramPacket peticionPush;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_segundo_mando);

        //Paso de parámetros entre activitys
        recibirParametros();

        try{
            socketUDP = new DatagramSocket();
            InetAddress hostServidor = InetAddress.getByName(ipAddress);
            String accionPush = "Push";

            byte[] mensajePush = accionPush.getBytes();

            peticionPush = new DatagramPacket(mensajePush, accionPush .length(), hostServidor,  puerto);
        }catch(Exception e){
            if(socketUDP != null && socketUDP.isClosed()){
                socketUDP.close();
            }
        }
        btnPush = (Button)findViewById(R.id.ButtonPush);
        btnPush.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                if (motionEvent.getAction() == MotionEvent.ACTION_DOWN){
                    sendTest sendcode = new sendTest();
                    sendcode.execute(peticionPush);
                    btnPush.setBackgroundColor(getResources().getColor(R.color.teal_200));
                    return true;
                }

                if(motionEvent.getAction() == MotionEvent.ACTION_UP){
                    btnPush.setBackgroundColor(getResources().getColor(R.color.teal_700));
                }
                return true;
            }
        });

    }

    public void setIpAddress(String ip){
        if(ip.isEmpty()){
            ipAddress = "127.0.0.1";
        }else{
            ipAddress = ip;
        }
    }

    public void setPuerto(int puerto){
        if(puerto == -1){
            this.puerto = 8050;
        }else{
            this.puerto = puerto;
        }
    }

    public void recibirParametros(){
        try {
            setIpAddress(getIntent().getStringExtra("ip"));
        }catch(Exception e){//EN CASO DE QUE NO RECIBAMOS NINGÚN PARAMETRO
            setIpAddress("");
        }

        try {
            setPuerto(getIntent().getIntExtra("puerto", -1));
        }catch(Exception e){//EN CASO DE QUE NO RECIBAMOS NINGÚN PARAMETRO
            setPuerto(-1);
        }
    }


    class sendTest extends AsyncTask<DatagramPacket, Void, Void> {

        protected Void doInBackground(DatagramPacket...params) {
            try {
                // Enviamos el datagrama
                socketUDP.send(params[0]);

            } catch (Exception e) {
                socketUDP.close();
            }
            return null;
        }
    }


    //Metodo para mostrar y ocultar menu
    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.overflow, menu);
        return true;
    }

    //Metodo para asignar las funciones correspondientes a las opciones
    public boolean onOptionsItemSelected(MenuItem item){
        int id = item.getItemId();
        if(id == R.id.item1){
            Intent siguiente = new Intent(this, Ajustes.class);
            siguiente.putExtra("ip", ipAddress);
            siguiente.putExtra("puerto", puerto);
            siguiente.putExtra("pantalla", "MandoDos");
            startActivity(siguiente);
        }

        return super.onOptionsItemSelected(item);
    }
}