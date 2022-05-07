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

public class PrimerMando extends AppCompatActivity {
    private String ipAddress;
    private int puerto = 8050;
    Button btn;
    Button btnRight;
    Button btnLeft;
    Button btnJump;
    private DatagramSocket socketUDP;
    private DatagramPacket peticionRight;
    private DatagramPacket peticionRightF;
    private DatagramPacket peticionLeft;
    private DatagramPacket peticionLeftF;
    private DatagramPacket peticionJump;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_primer_mando);
        //Paso de parámetros entre activitys
        recibirParametros();

        try{
            socketUDP = new DatagramSocket();
            InetAddress hostServidor = InetAddress.getByName(ipAddress);
            String accionRight = "Right";
            String accionRightF = "RightF";
            String accionLeft = "Left";
            String accionLeftF = "LeftF";
            String accionJump = "Jump";

            byte[] mensajeRight = accionRight.getBytes();
            byte[] mensajeRightF = accionRightF.getBytes();
            byte[] mensajeLeft = accionLeft.getBytes();
            byte[] mensajeLeftF = accionLeftF.getBytes();
            byte[] mensajeJump  = accionJump.getBytes();

            peticionRight = new DatagramPacket(mensajeRight, accionRight.length(), hostServidor,  puerto);
            peticionRightF = new DatagramPacket(mensajeRightF, accionRightF.length(), hostServidor,  puerto);
            peticionLeft = new DatagramPacket(mensajeLeft, accionLeft.length(), hostServidor,  puerto);
            peticionLeftF = new DatagramPacket(mensajeLeftF, accionLeftF.length(), hostServidor,  puerto);
            peticionJump = new DatagramPacket(mensajeJump, accionJump.length(), hostServidor,  puerto);
        }catch(Exception e){
            if(socketUDP != null && socketUDP.isClosed()){
                socketUDP.close();
            }
        }
        btnRight = (Button)findViewById(R.id.ButtonRIght);
        btnRight.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                if (motionEvent.getAction() == MotionEvent.ACTION_DOWN){
                    sendTest sendcode = new sendTest();
                    sendcode.execute(peticionRight);
                    btnRight.setBackgroundColor(getResources().getColor(R.color.teal_200));
                    return true;
                }

                if(motionEvent.getAction() == MotionEvent.ACTION_UP){
                    sendTest sendcode = new sendTest();
                    sendcode.execute(peticionRightF);
                    btnRight.setBackgroundColor(getResources().getColor(R.color.teal_700));
                }
                return true;
            }
        });

        btnLeft = (Button)findViewById(R.id.ButtonLeft);
        btnLeft.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                if (motionEvent.getAction() == MotionEvent.ACTION_DOWN){
                    sendTest sendcode = new sendTest();
                    sendcode.execute(peticionLeft);
                    btnLeft.setBackgroundColor(getResources().getColor(R.color.teal_200));
                    return true;
                }

                if(motionEvent.getAction() == MotionEvent.ACTION_UP){
                    sendTest sendcode = new sendTest();
                    sendcode.execute(peticionLeftF);
                    btnLeft.setBackgroundColor(getResources().getColor(R.color.teal_700));
                }
                return true;
            }
        });


        btnJump = (Button)findViewById(R.id.ButtonJump);
        btnJump.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                if (motionEvent.getAction() == MotionEvent.ACTION_DOWN) {
                    sendTest sendcode = new sendTest();
                    sendcode.execute(peticionJump);
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

    /*
    class send extends AsyncTask<Void, Void, Void> {
        private DataOutputStream pw;
        private DatagramSocket socketUDP;
        public String accion;
        private View view;

        protected Void doInBackground(Void...params) {
            try {
                socketUDP = new DatagramSocket();
                String msg2 = "F" + accion;

                byte[] mensaje = accion.getBytes();
                byte[] mensaje2 = msg2.getBytes();

                InetAddress hostServidor = InetAddress.getByName(ipAddress);

                // Construimos un datagrama para enviar el mensaje al servidor
                DatagramPacket peticion = new DatagramPacket(mensaje, accion.length(), hostServidor,  puerto);
                DatagramPacket peticionFin = new DatagramPacket(mensaje2, msg2.length(), hostServidor,  puerto);

                // Enviamos el datagrama
                socketUDP.send(peticion);

                switch (accion){
                    case "Right":
                        while(view.isPressed()){}
                        break;
                    case "Left":
                        while(btnLeft.isPressed()){}
                        break;
                }

                // Enviamos el datagrama
                socketUDP.send(peticionFin);

            } catch (UnknownHostException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }finally {
                if(!socketUDP.isClosed())
                    socketUDP.close();
            }

            return null;
        }
    }

    */
    /*
    class sendMessage extends AsyncTask<Void, Void, Void> {
        DataOutputStream pw;
        DatagramSocket socketUDP;
        public String accion;

        protected Void doInBackground(Void...params) {
            try {
                socketUDP = new DatagramSocket();
                String msg2 = "F" + accion;

                byte[] mensaje = accion.getBytes();
                byte[] mensaje2 = msg2.getBytes();

                InetAddress hostServidor = InetAddress.getByName(ipAddress);

                // Construimos un datagrama para enviar el mensaje al servidor
                DatagramPacket peticion = new DatagramPacket(mensaje, accion.length(), hostServidor,  puerto);
                DatagramPacket peticionFin = new DatagramPacket(mensaje2, msg2.length(), hostServidor,  puerto);

                // Enviamos el datagrama
                socketUDP.send(peticion);
                socketUDP.send(peticionFin);
            } catch (UnknownHostException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }finally {
                if(!socketUDP.isClosed())
                    socketUDP.close();
            }

            return null;
        }
    }
    */
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
            siguiente.putExtra("pantalla", "MandoUno");
            startActivity(siguiente);
        }

        return super.onOptionsItemSelected(item);
    }
}