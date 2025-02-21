---
description: Die User Storys
---

# User Story

## User Roles

### Benutzer

Normaler verwender der Applikation.

## Station suchen

ID: 1

Als [Benutzer](user-story.md#benutzer) möchte ich 

eine Station eingeben und nach dieser suchen, um

Stationen zu sehen und diese dann für die Suche von Verbindungen zu verwenden.

### AK

| AK | Beschreibung | Done |
| :--- | :--- | :--- |
| AK-1 | Durch die eingabe kann eine Station eingegeben werden und nach dieser Station gesucht werden. | ✔ |

## Verbindung suchen

ID: 2

Als [Benutzer](user-story.md#benutzer) möchte ich 

mindestens die nächsten Vier Verbindungen zwischen Start und Zielstation angezeigt sehen, um

die Verbindungen zu sehen.

### AK

| AK | Beschreibung | Done |
| :--- | :--- | :--- |
| AK-1 | Der Benutzer kann eine Start und Zielstation durch die Eingabe in ein Textfenster festlegen. | ✔ |
| AK-2 | Der Benutzer kann durch das betätigen eines "Search-Buttons" eine Anzeige aufrufen welche die Verbindungen zwischen der Start und Zielstation anzeigt | ✔ |
| AK-3 | Die Anzeige mit den Verbindungen zwischen den beiden Stationen zeigt mindestens die Nächsten 4 Verbindungen an | ✔ |

### Priorität

Diese User-Story hat die Priorität 1

## Abfahrtstafel

ID: 3

Als [Benutzer](user-story.md#benutzer) möchte ich 

eine Abfahrtstafel welche anzeigt welche verbindungen von meiner Station ausgehen,  um

die Verbindungen zu sehen.

### AK

| AK | Beschreibung | Done |
| :--- | :--- | :--- |
| AK-1 | Der Benutzer kann eine Station durch die Eingabe in ein Textfenster festlegen. | ✔ |
| AK-2 | Der Benutzer kann durch das betätigen eines "Search-Buttons" eine Anzeige aufrufen welche die Abfahrtsverbindungen anzeigt welche von dieser Station weg gehen. | ✔ |

## Geplante Abfahrt

ID: 12

Als [Benutzer](user-story.md#benutzer) möchte ich 

Beim suchen einer Verbindung eine Uhrzeit und ein Datum mitgeben welche dann anstelle des Aktuellen Zeitpunkts verwendet wird , um

Verbindungen aus der Zukunft anzuzeigen.

### AK

| AK | Beschreibung | Done |
| :--- | :--- | :--- |
| AK-1 | Bei der Eingabe der Start und Zielstation soll als Optionale eingabe eine Uhrzeit und Tag eingegeben werden können | ✔ |
| AK-2 | Standartgemäss wird die Uhrzeit und Tag eingabe auf den Aktuellen Zeitpunkt gesetzt. | ✔ |
| AK-3 | Die Uhrzeit und der Tag wird für das [suchen der Verbindungen](user-story.md#verbindung-suchen) verwendet, es werden verbindungen zu dieser Uhrzeit angezeigt. | ✔ |

## Abfahrtsinformationen per Mail teilen

ID: 5

Als [Benutzer](user-story.md#benutzer) möchte ich 

In der [Verbindungsansicht ](user-story.md#verbindung-suchen)eine möglichkeit diese Verbindungen Per E-Mail zu teilen , um

anderen Personen diese Informationen mitzuteilen.

### AK

| AK | Beschreibung | Done |
| :--- | :--- | :--- |
| AK-1 | Beim Klicken auf einen Knopf in der Detail ansicht wird ein Mail-Fenster geöffnet welches die Informationen der Verbindung beeinhaltet. | ✔ |

## Auto-Completion

ID: 6

Als [Benutzer](user-story.md#benutzer) möchte ich 

Beim [suchen von Verbindungen](user-story.md#verbindung-suchen) wärend der eingabe einer Station eine liste von Vorschlägen haben welche mit meiner bisherigen eingabe übereinstimmen , um

schneller Verbindungen zu suchen.

### AK

| AK | Beschreibung | Done |
| :--- | :--- | :--- |
| AK-1 | die Benutzer-Texteingaben haben eine Autocompletion durch die Möglichen Stationsnahmen. | - |

## Karte

ID: 7

Als [Benutzer](user-story.md#benutzer) möchte ich 

Den Standort einer Station auf einer Karte sehen , um

den Standort der Station nachzuvollziehen.

### AK

| AK | Beschreibung | Done |
| :--- | :--- | :--- |
| AK-1 | Eine Station kann auf der Karte angezeigt werden | ✔ |
| AK-2 | Die Umliegenden Stationen werden auf der Karte mitangezeigt | - |
| AK-3 | Auf der Karte kann der Aktuelle standort angezeigt werden ohne ausgewählte Station | - |

## Stationen in der Nähe

ID: 8

Als [Benutzer](user-story.md#benutzer) möchte ich 

Station in meiner Nähe anzeigen um

meine reise besser zu planen.

### AK

| AK | Beschreibung | Done |
| :--- | :--- | :--- |
| AK-1 | Stationen in der Nähe werden angezeigt. | ✔ |
| AK-2 | Stationen in der Nähe können auf einer Karte Angezeigt werden | ✔ |

## Favorit-Station

ID: 77

Als [Benutzer](user-story.md#benutzer) möchte ich 

Station als Favorit markieren können, um

diese Station bei der Suche mit höherer Priorität zu finden.

### AK

| AK | Beschreibung | Done |
| :--- | :--- | :--- |
| AK-1 | Eine Station kann als Favorit markiert werden | ✔ |
| AK-2 | Favorit-Stationen werden in der [Autocompletion ](user-story.md#auto-completion)mit höherer priorität angezeigt. | ✔ |
| AK-3 | Favorit-Stationen können bereits vor der eingabe von Text ausgewählt werden | - |

## Prioritätenliste

### Prio 1

* [Verbindungen suchen](user-story.md#verbindung-suchen)
* [Abfahrtstafel](user-story.md#abfahrtstafel)
* [Station suchen](user-story.md#station-suchen)

### Prio 2

* [Geplante Abfahrt](user-story.md#geplante-abfahrt)
* [Auto-Completion](user-story.md#auto-completion)

### Prio 3

* [Mail](user-story.md#abfahrtsinformationen-per-mail-teilen)
* [Drucken](user-story.md#verbindungen-ausdrucken)
* [Karte](user-story.md#karte)
* [Favorit](user-story.md#favorit-station)







