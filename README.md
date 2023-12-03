# Schrödingers Haus

<img src="pictures/logo.png" alt="Logo" style="width: 300px; height: auto;">

Ein Virtual Reality Escape Room mit Website Multiplayer-Funktion. Der VR Spieler soll ein spannendes Spielerlebnis haben, bei welchem den Zuschauern die Möglichkeit angeboten wird, diesen auf ihren Smartphones zu unterstützen

## Team
 
Abazovic Emina, Aschenberger Maria (Scrum Master), Böhm Sophie, Stieg Seppi

## Projektantrag

## 1. Ausgangslage 

### 1.1. Ist-Situation 

Der VR Escape Room ist eine Diplomarbeit aus dem Jahr 2023, die von uns verbessert und fortgesetzt wird. 
Die Webseite kann unkompliziert von jedem Gerät aus mittels QR-Code aufgerufen werden. 
Es soll ca. eine halbe Stunde Spielspaß bieten.

#### Wie funktioniert unser Spiel?

Der Hauptspieler startet das Spiel in der Quest App. Wenn er die VR-Brille aufsetzt, 
erscheint eine Lobby-ID. Diese wird von den Mitspielern benötigt, um sich für den Chat anmelden zu können. 
Wenn die Lobby-ID richtig war, muss man einen Usernamen eingeben. Ist dieser noch nicht vergeben wird man 
automatisch auf die Hauptseite weitergeleitet. Auf dieser werden verschiedene Geschichten angezeigt, 
die mehrere Rätsel für die Mitspieler enthalten. Diese Aufgaben müssen gelöst werden, um dem Hauptspieler das 
Fertigstellen eines Raumes zu ermöglichen. Die Ergebnisse eines Rätsels müssen über das Chatfenster eingegeben werden, 
um dem Hauptspieler angezeigt zu werden und diesem zu Helfen. Es gibt vier Räume (Kinderzimmer, Bibliothek, Labor, Keller), 
die erfolgreich abgeschlossen werden müssen um das Spielende zu erreichen.

#### Räume

#### Kinderzimmer 
<img src="pictures/childrensRoom.jpg" alt="Childrens Room">

1. Wurfobjekt muss unter dem Bett gefunden werden und in den Basketballkorb geworfen werden, sodass ein Papierflieger aus dem Korb fällt
2. Im Papierkorb liegt eine DVD die man in den alten Röhrenfersehen schieben muss 
3. Es erscheint eine Werbung von einem Teddy mit einen Reißverschluss hinten, aus dem man einen Schlüssel nehmen kann 
4. Der Schlüssel öffnet eine Schatztruhe in der eine Mathe Aufgabe enthalten ist 
5. Die Lösung kann als Code für eine Lade verwendet werden 
6. In der Lade befindet sich ein Hammer, mit dem man das Fenster durchbrechen kann (Ausgang: Durch das Fenster)

#### Bibliothek
<img src="pictures/library.jpg" alt="Childrens Room">

1. Notiz muss auf dem Tisch entdeckt werden, auf der steht was bereits am Atelier zu sehen ist und 1 Sache die noch gemalt werden muss
2. Fehlende Sache wird auf das Papier gemalt und dies fällt dann herunter und deckt eine Statue auf 
3. Statue schaut in die Richtung einer Säule und in der Nähe dieser Säule findet man ein bestimmtes Buch 
4. Nach Entnahme des Buches öffnet sich die Türe

#### Labor 
<img src="pictures/laboratory.jpg" alt="Childrens Room">

1. Ein Alarm ertönt ("Raum ist kontaminiert") und man müss die Notfallanweisungen durchlesen 
2. Lüftung im Boden muss abgedeckt werden, dass der Alarm aufhört 
3. Es ertönt eine Durchsage mit den Zutanten für ein Gegenmittel
4. Die beschrifteten Gläsern müssen zusammengesucht, gemischt und getrunken werden, dass der Alarm aus geht 
5. Es müssen Zahlen am Tisch gefunden werden die als Code für die Tür verwendet werden können


Kerker 
<img src="pictures/dungeon.jpg" alt="Childrens Room">

1. Eine Schriftrolle muss im Bücherregal gefunden werden, in der eine Anleitung für ein Schwert enthalten ist 
2. Ein Hammer muss im Fass gefunden werden und eine Metallstange in einer Truhe 
3. Ein Feuerholz muss in den Ofen gelegt werden, sodass eine Stange beginnt zu glühen 
4. Auf dem Amboss muss aus dem glühenden Stab und dem Hammer ein Schwert gemacht werden 
5. Mit dem Schwert können dann die Ketten an der Tür zerschlagen werden 

### 1.2 Verbesserungspotential 

#### Probleme 

* Benutzbarkeit 
* Layout 
* Modelle

#### Verbesserungen 

* Datenmodell
* Chat 
* Spielstand

## 2. Zielsetzung 

### 2.1 Soll-Zustand

Unser Ziel ist es, ein Escape Room VR-Spiel zu entwickeln, welches die HTL Leonding vorzeigen kann, wenn sie die Oculus Quest 2 oder Oculus Rift bei Events ausstellt. 

## 3. Risikoanalyse 

### 3.1 Chancen 
* Neue Technologien kennenlernen 
* Zweigübergreifende Zusammenarbeit 

### 3.2 Projektrisiken 

* Komplikationen, da wir noch nie mit VR gearbeitet haben 
* hoher Zeitaufwand sich in das Projekt einzufinden 

## 4. Projektablauf 

### 4.1 Umgebung
* Visual Studio 
* IntelliJ
* Unity
* Cinema 4D

### 4.2 Meilensteine 

#### Wintersemester
* Anwendung Testen und weitere Fehler suchen 
* Aufgabenzuweisung 
* Fehler beheben 
* neues Web-Design 

#### Sommersemester
* alle Fehler beheben 
* neue Features hinzufügen 

## User Stories

### Vr-Spieler:

#### neues Spiel:
* Der User startet das Spiel auf der VR Brille und erhält eine neue LobbyID 
* Der User startet bei neuer LobbyID im Kinderzimmer 
* Der User erhält Hinweise und Aufgaben von seinen Mitspielern durch einen eingeblendeten Chat 
* Der User löst Rätsel des aktuellen Raumes um in den nächsten fortzuschreiten 
* Der User kann während des Spiels den Spielstand jederzeit speichern

#### altes Spiel:
* Der User kann ein gespeichertes Spiel weiterspielen
* Der User startet das Spiel und steigt mit der bereits vorhandenen LobbyID ein 
* Der User startet im zuletzt besuchten Raum 

### Mitspieler:
* Weitere User steigen über die LobbyID oder den QR Code mit einem Username in den Chatroom ein 
* Die User erhalten bei falscher LobbyId oder bereits vergebenem Username eine Fehlermeldung 
* Die User sehen nach Einstieg eine Liste von allen Mitspielern in der selben Lobby 
* Die User können mit ihren Mitspielern und dem VR-Spieler über den Chat kommunizieren 
* Die User können durch eine vorgegebene Geschichte, die zu den jeweiligen Räumen gehört, durchblättern
* Die User müssen im laufe der Geschichte verschiedene Rätsel und Mini-Games lösen um dem VR-Spieler zu helfen
