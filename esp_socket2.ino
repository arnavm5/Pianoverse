/*
 * WebSocketServer.ino
 *
 *  Created on: 22.05.2015
 *
 */

#include <Arduino.h>

#include <WiFi.h>
#include <WiFiMulti.h>
#include <WiFiClientSecure.h>

#include <WebSocketsServer.h>
#include <ESP32Servo.h>

WiFiMulti WiFiMulti;
WebSocketsServer webSocket = WebSocketsServer(81);
int intensity = 1;

int curr_intensity = 0;

int direction;
// Servo 1 = thumb
// Servo 2 = index
// Servo 3 = middle
// Servo 4 = ring
// Servo 5 = pinky
Servo r1, r2, r3, r4, r5;
int r1_pin = 13;
int r2_pin = 12;
int r3_pin = 14;
int r4_pin = 23;
int r5_pin = 22;
int pos = 170;

int relay1 = 27;
int relay2 = 18;
String global = "";
int curr_inten = 0;

#define USE_SERIAL Serial

void hexdump(const void *mem, uint32_t len, uint8_t cols = 16) {
	const uint8_t* src = (const uint8_t*) mem;
	USE_SERIAL.printf("\n[HEXDUMP] Address: 0x%08X len: 0x%X (%d)", (ptrdiff_t)src, len, len);
	for(uint32_t i = 0; i < len; i++) {
		if(i % cols == 0) {
			USE_SERIAL.printf("\n[0x%08X] 0x%08X: ", (ptrdiff_t)src, i);
		}
		USE_SERIAL.printf("%02X ", *src);
		src++;
	}
	USE_SERIAL.printf("\n");
}

void webSocketEvent(uint8_t num, WStype_t type, uint8_t * payload, size_t length) {

    switch(type) {
        case WStype_DISCONNECTED:
            USE_SERIAL.printf("[%u] Disconnected!\n", num);
            break;
        case WStype_CONNECTED:
            {
                IPAddress ip = webSocket.remoteIP(num);
                USE_SERIAL.printf("[%u] Connected from %d.%d.%d.%d url: %s\n", num, ip[0], ip[1], ip[2], ip[3], payload);

				// send message to client
				webSocket.sendTXT(num, "Connected");
            }
            break;
        case WStype_TEXT:
            USE_SERIAL.printf("[%u] get Text: %s\n", num, payload);
            global = (char *)payload;

            // send message to client
            // webSocket.sendTXT(num, "message here");

            // send data to all connected clients
            // webSocket.broadcastTXT("message here");
            break;
        case WStype_BIN:
            USE_SERIAL.printf("[%u] get binary length: %u\n", num, length);
            hexdump(payload, length);

            // send message to client
            // webSocket.sendBIN(num, payload, length);
            break;
		case WStype_ERROR:			
		case WStype_FRAGMENT_TEXT_START:
		case WStype_FRAGMENT_BIN_START:
		case WStype_FRAGMENT:
		case WStype_FRAGMENT_FIN:
			break;
    }

}

void setup() {
  r1.attach(r1_pin, 700, 2500);
  r2.attach(r2_pin, 700, 2500);
  r3.attach(r3_pin, 700, 2500);
  r4.attach(r4_pin, 700, 2500);
  r5.attach(r5_pin, 700, 2500);


  pinMode(relay1, OUTPUT);
  pinMode(relay2, OUTPUT);

  USE_SERIAL.begin(115200);

  //Serial.setDebugOutput(true);
  USE_SERIAL.setDebugOutput(true);

  USE_SERIAL.println();
  USE_SERIAL.println();
  USE_SERIAL.println();

  for(uint8_t t = 4; t > 0; t--) {
      USE_SERIAL.printf("[SETUP] BOOT WAIT %d...\n", t);
      USE_SERIAL.flush();
      delay(1000);
  }

  WiFiMulti.addAP("quackster", "quackquack");

  while(WiFiMulti.run() != WL_CONNECTED) {
      delay(100);
  }

  webSocket.begin();
  webSocket.onEvent(webSocketEvent);
}

void pull_finger_up(Servo &servo1, Servo &servo2, Servo &servo3, Servo &servo4) {
  if (curr_inten < 2){
    activate_relay(relay1);
    curr_inten += intensity;
  }
  servo1.write(pos);
  servo2.write(pos);
  servo3.write(pos);
  servo4.write(pos);
  delay(10);
}

void reset(Servo &servo1, Servo &servo2, Servo &servo3, Servo &servo4, Servo &servo5) {
  activate_relay(relay2);
  curr_inten -= intensity;
  servo1.write(0);
  servo2.write(0);
  servo3.write(0);
  servo4.write(0);
  servo5.write(0);
  delay(10);
}

void let_go_finger(Servo &servo) {
  servo.write(0);
  delay(10);
}

void play_finger(int current){
  if (current == 1){
    let_go_finger(r1);
    pull_finger_up(r2, r3, r4, r5);
    // delay(5000);
    // reset(r1, r2, r3, r4, r5);
  }
  if (current == 2){
    let_go_finger(r2);
    pull_finger_up(r1, r3, r4, r5);
    // delay(5000);
    // reset(r1, r2, r3, r4, r5);
  }
  if (current == 3){
    let_go_finger(r3);
    pull_finger_up(r1, r2, r4, r5);
    // delay(5000);
    // reset(r1, r2, r3, r4, r5);
  }
  if (current == 4){
    let_go_finger(r4);
    pull_finger_up(r1, r2, r3, r5);
    // delay(5000);
    // reset(r1, r2, r3, r4, r5);
  }
  if (current == 5){
    let_go_finger(r5);
    pull_finger_up(r1, r2, r3, r4);
    // delay(5000);
    // reset(r1, r2, r3, r4, r5);
  }
}

void activate_relay(int relay) {
  for (int i = 0; i < intensity; i++) {
    digitalWrite(relay, HIGH);
    delay(100);
    digitalWrite(relay, LOW);
    delay(100);
  }
}

void loop() {
  webSocket.loop();
  // Serial.println(String(global) == "props_148key24_collider");
  // Serial.println(String(global));
  if (curr_inten > 2){
    reset(r1, r2, r3, r4, r5);
  }

  if (String(global) == "props_148key24_collider"){
    //index
    play_finger(2);
    global = "";
  }
  if (String(global) == "props_148key26_collider"){
    //middle
    play_finger(3);
    global = "";
  }
  if (String(global) == "props_148key28_collider"){
    //ring
    play_finger(4);
    global = "";
  }
  if (String(global) == "props_148key30_collider"){
    //pinky
    play_finger(5);
    global = "";
  }
  if (String(global) == "stop1"){
    reset(r1, r2, r3, r4, r5);
    global = "";
  }

  // for (int i = 0; i < 5; i++){
  //   let_go_finger(servos[i]);
  // }
}