## T1. PR1. Seguretat i vulnerabilitat


#### 1. a. Escull 3 vulnerabilitats d’aquesta llista i descriu-les. Escriu l’impacte que tenen a la seguretat i quins danys pot arribar a fer un atac en aquesta vulnerabilitat. Enumera diferents mesures i tècniques per poder evitar-les.
1. Pèrdua de Control d'Accés (A01:2021)
Això passa quan els controls d'accés no estan ben posats i algú que no deuria acaba entrant a dades o funcions restringides. Un atacant podria veure, canviar o esborrar informació sensible, o fins i tot usar funcions d'administrador sense permís. Això ho fa súper perillós per a la seguretat. Per a evitar-ho, cal usar el "mínim privilegi", que significa donar a cada usuari sol l'accés que necessita. També es poden posar llistes de control d'accés (ACL) ben definides i fer proves de tant en tant per a trobar i arreglar fallades.

2. Fallades Criptogràfiques (A02:2021)
Aquí el problema és que el xifratge, que serveix per a protegir dades sensibles, està mal fet o usa mètodes febles. Si un atacant el trenca, pot veure informació privada com a contrasenyes o dades personals, i això porta a violacions de privacitat o pèrdues de diners. La solució és xifrar les dades sempre, ja sigui quan es mouen o quan estan guardats, usant algorismes forts. Res d'usar mètodes vells o trencats. També cal generar, guardar i canviar les claus de xifratge amb cura.

3. Injecció (A03:2021)
Això ocorre quan dades dolentes es fiquen en un sistema, com en una consulta a una base de dades, i el programa els pren com a comandos vàlids. Un atacant podria accedir a dades que no deuria, canviar informació o fins a prendre control del sistema. És un risc gran. Per a prevenir-ho, cal revisar i netejar tot el que entra des de l'usuari. Usar consultes parametritzades ajuda al fet que les bases de dades no es confonguin amb dades rares. També es poden usar eines d'anàlisi estàtica i dinàmica per a buscar aquests errors en el codi.

#### 2. Copia cada una de les sentències SQL resultant que has realitzat a cada nivell i comenta que has aconseguit. Enumera i raona diferents formes que pot evitar un atac per SQL injection en projectes fets amb Razor Pages i Entity Framework. 

''' sql
SELECT username FROM users WHERE username ='jane';--' AND password ='d41d8cd98f00b204e9800998ecf8427e';
'''
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

#### 3. a. Definició del control d’accés: enumera els rols  i quin accés a dades tenen cada rol. 
La meva definició del control d'accés
Client: Registre, inici de sessió, compra d'obres, escriptura de ressenyes, edició de dades personals (nom, DNI, adreça, telèfon).
Artista: Registre, inici de sessió, gestió d'obres (pujar, editar, esborrar), edició de dades personals i bancàries, consulta de ressenyes.
Account Manager: Verificació de nous artistes, accés a les seves dades (excepte bancaris).
Administrador: Gestió total d'usuaris, accés i edició de totes les dades, monitoratge de seguretat.
Cada rol té permisos limitats a les seves funcions.

#### b. Definició de la política de contrasenyes: normes de creació, d’ús i canvi de contrasenyes. Raona si són necessàries diferents polítiques segons el perfil d’usuari.
Definició de la política de contrasenyes
Creació: Mínim 12 caràcters, amb majúscula, minúscula, número i caràcter especial. No coincideix amb nom ni DNI.
Ús i canvi: Renovació cada 6 mesos, sense repetir les últimes 5 contrasenyes. Artistes i managers usen 2FA.
Per perfil: Clients amb contrasenyes fortes. Artistes i managers amb renovació i 2FA per dades més sensibles.

#### c. Avaluació de la informació: determina quin valor tenen les dades que treballa l'aplicació. Determina com tractar les dades més sensibles. Quines dades encriptaries?
Avaluació de la informació
Crítics: Contrasenyas (hash amb bcrypt), dades bancàries i DNI/adreça (encriptats amb AES-256). Alta sensibilitat.
Sensibles: Nom, telèfon, historial de compres (protegits amb permisos).
Públics: Ressenyes i dades d'obres (sense encriptar, protegits contra modificacions).
Dades sensibles s'encripten i transmeten amb HTTPS.

#### 4.En el control d’accessos, existeixen mètodes d’autenticació basats en tokens. Defineix l’autenticació basada en tokens. Quins tipus hi ha? Com funciona mitjançant la web? Cerca llibreries .Net que ens poden ajudar a implementar autenticació amb tokens.

Autenticación basada en tokens
La autenticación basada en tokens es un sistema donde primero te verificas con tus credenciales y el servidor te da un token . Luego usas este token para entrar en las cosas sin tener que poner a tu usuario y contraseña todo el tiempo. Esto hace que las aplicaciones web sean más seguras y vayan más rápido.

Tipo de autenticación basada en tokens
- JWT ( JSON Web Token) : Es como un token chiquito que lleva todo dentro, lo usan mucho en APIs y páginas web.
- OAuth 2.0 y OpenID Connect : Son formas estándar de autenticarte y dar permisos, sobre todo para cosas como inicio de sesión único.
- SAML : Éste lo usan más en empresas grandes para conectar sistemas distintos.

Funcionamiento en aplicaciones web
El usuario mete sus credenciales en la página. El servidor las chequea y te da un token firmado. Este token lo mandas en las cabeceras HTTP cuando pides algo más. El servidor le mira y dice si te deja pasar o no. Los tokens duran poco y puedes renovarlos con otro token especial.

Librerías. NET para implementar autenticación con tokens
- Microsoft.AspNetCore.Authentication.JwtBearer: Esto te ayuda a meter JWT en ASP.NET Core.
- IdentityServer4: Es para usar OAuth y OpenID Connect en .NET, bastante útil.
- Duende IdentityServer: Viene después de IdentityServer4, para manejar autenticación y permisos.
- Microsoft.IdentityModel.Tokens: Sirve para hacer y revisar tokens en .NET.

#### 5.Crea un projecte de consola amb un menú amb tres opcions:
#### Registre: l’usuari ha d’introduir username i una password. De la combinació dels dos camps guarda en memòria directament l'encriptació. Utilitza l’encriptació de hash SHA256. Mostra per pantalla el resultat.
#### Verificació de dades: usuari ha de tornar a introduir les dades el programa mostra per pantalla si les dades són correctes.
#### Encriptació i desencriptació amb RSA. L’usuari entrarà un text per consola. A continuació mostra el text encriptat i en la següent línia el text desencriptat. L’algoritme de RSA necessita una clau pública per encriptar i una clau privada per desencriptar. No cal guardar-les en memòria persistent. Per realitzar aquest exercici utilitza la llibreria System.Security.Cryptography.




#### 6. Bibliografia 
- [ https://owasp.org ]
- [ https://www.nist.gov ]
- [ https://docs.sonarqube.org ]
- [ https://www.entrust.com/es/resources/learn/what-is-token-based-authentication?utm_source](https://www.entrust.com/es/resources/learn/what-is-token-based-authentication?)
- [ https://www.aluracursos.com/blog/tipos-de-autenticacion? ]
- [ https://learn.microsoft.com/en-us/entra/msal/dotnet/? ]
- [ https://es.wikipedia.org/wiki/JSON_Web_Token? ]
