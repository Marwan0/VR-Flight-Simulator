 using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class udpSend : MonoBehaviour {
	public float pitch,roll,yaw,power, brF;
	public bool br;
	private int clientPort = 49000;
	UdpClient udpServer;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//Rotational Motion
		GameObject g = GameObject.FindGameObjectWithTag ("yolk");
		yolkTrans yoke = g.GetComponent<yolkTrans> ();

		pitch = yoke.pitch;
		if (yoke.roll < 1 && yoke.roll > -1) {
			roll = -yoke.roll;
		};
		//yaw = -999;
		yaw = yoke.yaw;

		Socket sock = new Socket (AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		IPAddress serverAddr = IPAddress.Parse ("127.0.0.1");
		IPEndPoint dataServer = new IPEndPoint (serverAddr, clientPort);
			
		byte[] udpArray = new byte[41];
		byte[] data1 = Encoding.ASCII.GetBytes ("DATA");
		byte[] data2 = { 8, 0, 0, 0 };
		byte[] data3 = System.BitConverter.GetBytes(pitch);
		byte[] data4 = System.BitConverter.GetBytes(roll);
		byte[] data5 = System.BitConverter.GetBytes(yaw);
		//byte[] data5 = {0,0,0,0};

		for(int i=0; i<4; i++){
			udpArray[i] = data1[i];
		}

		udpArray[4] = 0;

		for(int i=5; i<9; i++){
			udpArray[i] = data2[i-5];
		}

		for(int i=9; i<13; i++){
			udpArray[i] = data3[i-9];

		}

		for(int i=13; i<17; i++){
			udpArray[i] = data4[i-13];


		}

		for(int i=17; i<21; i++){
			udpArray[i] = data5[i-17];
			//udpArray[i] = -999f;
		}

		for(int i=21; i<udpArray.Length; i++){
			udpArray [i] = 0;
		}

		//Throttle
		GameObject t = GameObject.FindGameObjectWithTag ("throttle");
		throttle throt = t.GetComponent<throttle> ();
		power = throt.power;

		byte[] throtArray = new byte[41];
		byte[] elsa1 = Encoding.ASCII.GetBytes ("DATA");
		byte[] elsa2 = { 25, 0, 0, 0 };
		byte[] elsa3 = System.BitConverter.GetBytes(power);

		for(int i=0; i<4; i++){
			throtArray[i] = elsa1[i];
		}

		throtArray[4] = 0;

		for(int i=5; i<9; i++){
			throtArray[i] = elsa2[i-5];
		}

		for(int i=9; i<13; i++){
			throtArray[i] = elsa3[i-9];

		}

		for(int i=13; i<throtArray.Length; i++){
			throtArray[i] = 0;

		}
			

		//Break
//		bool temp = OVRInput.GetDown(OVRInput.RawButton.X);
//		int count = 0;
//
//		if (temp)
//			br = !br;
//		if (br)
//			brF = 0.5f;
//		else
//			brF = 0f;
//
//
//		byte[] breakArray = new byte[41];
//		byte[] jojo1 = Encoding.ASCII.GetBytes ("DATA");
//		byte[] jojo2 = { 14, 0, 0, 0 };
//		byte[] jojo3 = System.BitConverter.GetBytes(brF);
//		byte[] jojo4 = System.BitConverter.GetBytes(1.0f);
//
//		for(int i=0; i<4; i++){
//			breakArray[i] = jojo1[i];
//		}
//
//		breakArray[4] = 0;
//
//		for(int i=5; i<9; i++){
//			breakArray[i] = jojo2[i-5];
//		}
//		for(int i=9; i<13; i++){
//			breakArray[i] = jojo4[i-9];
//		}
//
//
//		for(int i=13; i<17; i++){
//			breakArray[i] = jojo3[i-13];
//
//		}
//
//		for(int i=17; i<breakArray.Length; i++){
//			breakArray[i] = 0;
//
//		}

		sock.SendTo(udpArray,dataServer);
		//sock.SendTo(breakArray,dataServer);
		sock.SendTo(throtArray,dataServer);

	}
}


