# Solaris

## Componența Echipei

- Fritz Raluca-Mihaela (343)
- Ion Alexandra (342)
- Lascu Matei (343)
- Manole Patricia-Theodora (341)
- Mihai Radu-Ioan (332)
- Pop Maria (332)
- Postolache Andreea Miruna (342)
- Stanescu Maria-Raluca (332)
- Velniceru Ioana-Alexandra (332) 
- Vultur Sofia (343)

## Descriere

Solaris reprezintă o simulare a sistemului solar. Aceasta conține, pe lângă cele 8 planete și Astrul Solar, asteroizi, o rachetă și reziduuri spațiale.

Ideea simulării este de a elimina, prin deplasarea rachetei (cu ajutorul tastelor WASD și indicând direcția de deplasare cu ajutorul mouse-ului) în vecinatatea apropiată, a asteroizilor ce prezintă un real pericol pentru locuitorii Pământului.

## Interfață grafică

- Ecran de început, ce conține numele aplicației, o imagine sugestivă și două butoane: start și quit;

- În cadrul aplicației, există o informație în partea stânga sus a ecranului, prin care utilizatorul este informat cu privire la numărul de asteroizi distruși, cât și numărul total al acestora generați la începutul rulării simulării.

## Funcționalități

### Fizici

- În cadrul mișcării planetelor, se iau în considerare formulele care descriu mișcarea corpurilor cerești în relație cu Soarele. Acestea includ viteza de orbitare a planetelor și Legea Gravitației Universale formulată de Newton.

- Racheta se deplasează după fizici reale, în următorul mod: utilizatorul arată direcția de înaintare cu ajutorul poziției mouse-ului pe ecran, în timp ce deplasarea efectivă se realizează cu tastele WASD. În plus, pentru o deplasare mai rapidă, se poate ține apăsată tasta SHIFT pentru a activa boost-ul.

### Grafică

- Asteorizii au fost realizați pornind de la o sferă, pe care s-a aplicat un shader custom pentru a realiza o formă cât mai asemănătoare cu cea a unui asteroid real. Aceștia apar în cadrul aplicației în număr și poziție variabile. 

- Pe baza elementelor realizate anterior, forma a fost păstrată și pentru crearea reziduurilor.

- Implementările au avut la bază codul prezentat în cadrul laboratoarelor.

### Boids

- Pentru a evidenția o problemă majoră, cea a deșeurilor din spațiu, s-a realizat un grup de reziduuri (Boids), ce navighează prin atmosferă și își poate schimba direcția la fiecare 10 secunde.

### Inteligență Artificială - Implementări încercate

- Ideea inițială de implementare conținea un algoritm A* care deplasa racheta către cel mai apropiat asteroid, până când țoți erau eliminați, moment în care racheta trecea în deplasare manuală pentru admirarea Sistemului Solar. Din cauza distanțelor mari dintre obiectele din scenă, algoritmul trebuia să genereze un număr prea mare de drumuri pentru a putea fi stocate și utilizate. Acest fapt a dus la alegerea implementării Boids.



