# Zmija / Snake
RP3 project
## Upute za kloniranje repozitorija
* u repozitoriju nema svih datoteka koje VS generira prilikom pokretanja programa nego samo one bitne za *build*
* nakon kloniranja i otvaranja 'Zmijica.sln' trebao bi funkcionirati build projekta
* kod push i pull naredbi se automatski preskaću datoteke opisane u '.gitignore' 
# Specification

## Original text 
Igra bi se trebala sastojati od više razina različite težine. Cilj svake razine je npr. dostići određenu duljinu zmije ili neki broj bodova. 

Razine se mogu razlikovati po brzini kretanja zmije, zatim možete dodati neke dodatne prepreke s kojima se zmija ne smije sudariti, možete mijenjati i oblik prostora unutar kojeg se zmija kreće. 

Također, na plohi kretanja se mogu nalaziti predmeti za koje, ako ih zmija pojede, igrač može dobiti dodatne bodove, izgubiti bodove, može prijeći na drugu razinu, može se promijeniti
smjer kretanja zmije ili joj se duljina smanji ili poveća na neki drugi način od uobičajenog. Za neki tip "hrane" postavite da je jasno kako utječe (nekom 2-kombinacijom tipaka neka je moguće dobiti informativni prozor), dok se za drugu "hranu" ponašanje određuje slučajnim odabirom. 

Ubacite na neku razinu mogućnost da se u ovisnosti o nekim događajima pojave mjesta na kojima zmija smije preskočiti samu sebe. Da bi igra tekla dinamičnije, dozvolite brže kretanje zmije na način da pritiskanjem strelica i numeričke tipke pomaknete odjednom zmiju za određen broj polja u zadanom smjeru. S tipkom SHIFT i strelicom smjera, zmija bi trebala ići do samog ruba plohe. Uz neku drugu kombinaciju, pomakne se koliko je najviše moguće u tom smjeru, a da se ne sudari sa samom sobom. 

U postavkama igre igrač može sam namjestiti tipke koje mu odgovaraju za te operacije. 

Na nekoj od viših razina možete ubaciti i drugu zmiju kojom upravlja računalo i koja je protivnik igračevoj zmiji. Cilj je ne sudariti se sa zmijom kojom upravlja računalo. Zmije se naizmjenično pomiču. 

Također neka je moguće dobiti i određen broj pauza u igri. Možete odrediti i broj života koje zmija može potrošiti u igri.

## Basic Look
![basic_look](assets/Snake_basic_look.jpg)
* graphics for the prototype will be mostly rectangles filled with different colors
* purple represents the walls
* dark green is the snake's head
* lightgreen is the rest of the snake
* red, orange and gray are the foods - more detail in [Foods](#foods)
## Controls
## Win conditions
## Loose conditions
## Foods
## Menu
### Settings window
### Info window

# Implementation
## General notes
### Game loop
Class hierarchy `Form > GameForm > Game` is made to ensure code sustainability. `Game` focuses on methods:
* `Setup` : called on Form.Load event
* `Draw` : called on Application.Idle event
* `[InputEventHandler]` : called on input event

`GameForm` is also an **abstract** class because of mentioned abstract methods implemented in the `Game` class objects.
## Model
### Field
### `SnakeProto`
### `Snake`
## View
### `IDrawable`
