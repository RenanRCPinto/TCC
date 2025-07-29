#include "HX711.h"

HX711 balancaEsq;
HX711 balancaDir;

void setup() {
  Serial.begin(115200);

  balancaEsq.begin(25, 26); // DT, SCK (ajuste conforme ligação)
  balancaDir.begin(33, 32);
}

void loop() {
  long sE = balancaEsq.read();
  long sD = balancaDir.read();

  Serial.print(sE);
  Serial.print(",");
  Serial.println(sD);

  delay(50);
}
