## T1. PR1. Seguretat i vulnerabilitat


#### 1. a. Escull 3 vulnerabilitats d’aquesta llista i descriu-les. Escriu l’impacte que tenen a la seguretat i quins danys pot arribar a fer un atac en aquesta vulnerabilitat. Enumera diferents mesures i tècniques per poder evitar-les.
1. Pèrdua de Control d'Accés (A01:2021)
Això passa quan els controls d'accés no estan ben posats i algú que no deuria acaba entrant a dades o funcions restringides. Un atacant podria veure, canviar o esborrar informació sensible, o fins i tot usar funcions d'administrador sense permís. Això ho fa súper perillós per a la seguretat. Per a evitar-ho, cal usar el "mínim privilegi", que significa donar a cada usuari sol l'accés que necessita. També es poden posar llistes de control d'accés (ACL) ben definides i fer proves de tant en tant per a trobar i arreglar fallades.

2. Fallades Criptogràfiques (A02:2021)
Aquí el problema és que el xifratge, que serveix per a protegir dades sensibles, està mal fet o usa mètodes febles. Si un atacant el trenca, pot veure informació privada com a contrasenyes o dades personals, i això porta a violacions de privacitat o pèrdues de diners. La solució és xifrar les dades sempre, ja sigui quan es mouen o quan estan guardats, usant algorismes forts. Res d'usar mètodes vells o trencats. També cal generar, guardar i canviar les claus de xifratge amb cura.

3. Injecció (A03:2021)
Això ocorre quan dades dolentes es fiquen en un sistema, com en una consulta a una base de dades, i el programa els pren com a comandos vàlids. Un atacant podria accedir a dades que no deuria, canviar informació o fins a prendre control del sistema. És un risc gran. Per a prevenir-ho, cal revisar i netejar tot el que entra des de l'usuari. Usar consultes parametritzades ajuda al fet que les bases de dades no es confonguin amb dades rares. També es poden usar eines d'anàlisi estàtica i dinàmica per a buscar aquests errors en el codi.
