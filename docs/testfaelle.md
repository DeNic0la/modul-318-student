---
description: Die Testfälle für die Applikation
---

# Testfälle

## Testplan

### Abfahrtstafel anzeigen

Requirements: Programm gestartet, Aktive Internetverbindung.

| Schritt | Aktivität | Erwartetes Resultat | Abweichendes Resultat | Erfüllt |
| :--- | :--- | :--- | :--- | :--- |
| 1 | "Luzern" in das Startstationsfeld eingeben | Wärend dem Eingeben von Text taucht eine Liste von Vorschlägen auf, von welcher Luzern durch klicken oder durch die Pfeiltasten ausgewählt werden kann.  |  |  |
| 2 | "Luzern" aus der ListBox durch Pfeiltasten auswählen oder ausschreiben und anschliessen die Tab-Taste drücken | Anstelle des "Stationen in der Nähe anzeigen"-Button wird ein "Abfahrtstafel Anzeigen"-Button angezeigt |  |  |
| 3 | Der "Abfahrtstafel Anzeigen"-Button wird gedrückt | Der Coursor beginnt sich zu drehen bis im unteren Bereicht des GUI's eine Liste der Abfahrtstafeleinträge angezeigt wird. |  |  |

### Verbindung suchen

Requirements: Programm gestartet, Aktive Internetverbindung

| Schritt | Aktivität | Erwartetes Resultat | Abweichendes Resultat | Erfüllt |
| :--- | :--- | :--- | :--- | :--- |
| 1 | "Luzern" in das Startstationsfeld eingeben | Wärend dem Eingeben von Text taucht eine Liste von Vorschlägen auf, von welcher Luzern durch klicken oder durch die Pfeiltasten ausgewählt werden kann. |  |  |
| 2 | "Luzern" aus der ListBox durch Pfeiltasten auswählen oder ausschreiben und anschliessen die Tab-Taste drücken | Der Coursor Verschiebt sich in das Feld "Zielstation" |  |  |
| 3 | Im Feld "Zielstation" wird "Bern" eingegeben | Wärend dem tippen tauchen Vorschläge auf |  |  |
| 4 | Mit den Pfeiltasten "Bern" aus der ListBox auswählen und die "Enter"-Taste drücken | Nach kurzem laden tauchen die Verbindungen von Luzern nach Bern auf |  |  |



### Station suchen

Requirements: Programm gestartet, Aktive Internetverbindung

| Schritt | Aktivität | Erwartetes Resultat | Abweichendes Resultat | Erfüllt |
| :--- | :--- | :--- | :--- | :--- |
| 1 | "Luz" in das Startstationsfeld eingeben | Luz steht im Startstationsfeld |  |  |
| 2 | Auf den "Station suchen" Knopf klicken | Nach kurzem laden tauchen im unteren Teil des GUI's meherere Buttons auf. Unteranderem der Button Luzern |  |  |
| 3 | Der Button "Luzern" wird gedrückt | Im Startstationsfeld steht Luzern |  |  |

### Verbindung suchen zu bestimmter Zeit

Requirements: Programm gestartet, Aktive Internetverbindung

| Schritt | Aktivität | Erwartetes Resultat | Abweichendes Resultat | Erfüllt |
| :--- | :--- | :--- | :--- | :--- |
| 1 | "Luzern" in das Startstationsfeld eingeben | Wärend dem Eingeben von Text taucht eine Liste von Vorschlägen auf, von welcher Luzern durch klicken oder durch die Pfeiltasten ausgewählt werden kann. |  |  |
| 2 | "Luzern" aus der ListBox durch Pfeiltasten auswählen oder ausschreiben und anschliessen die Tab-Taste drücken | Der Coursor Verschiebt sich in das Feld "Zielstation" |  |  |
| 3 | Im Feld "Zielstation" wird "Bern" eingegeben | Wärend dem tippen tauchen Vorschläge auf |  |  |
| 4 | Mit den Pfeiltasten "Bern" aus der ListBox auswählen und die "Enter"-Taste drücken | Nach kurzem laden tauchen die Verbindungen von Luzern nach Bern auf, der Cursor verschiebt sich in das Zeitpunkt-Feld |  |  |
| 5 | Es wird der Text "1230" eingegeben. Ohne Lehrzeichen, Ohne Doppelpunk. | Im Feld Zeitpunkt steht der Text "12:30" |  |  |
| 6 | Im "Datum auswählen"-Feld wird das Datum von morgen eingetragen | Im Datum-Feld steht das Datum von Morgen |  |  |
| 7 | Der Knopf "Verbindung suchen" wird gedrückt | Es werden mindestens 4 Verbindungen angezeigt, alle verbindungen sind nach dem Morgigen tag um 12:30 |  |  |

### Verbindung per Mail versenden

Requirements: Programm gestartet, Aktive Internetverbindung, Es werden bereits [Verbindungen ](testfaelle.md#verbindung-suchen)angezeigt.

| Schritt | Aktivität | Erwartetes Resultat | Abweichendes Resultat | Erfüllt |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Aus den angezeigten Verbindungen wird eine Reihe angeklickt. \(Es wird ein Feld mit Text angeklickt.\) | Die angeklickte Reihe ist blau markiert |  |  |
| 2 | Der Knopf mit dem Brief-Icon wird gedrückt | Es öffnet sich möglicherweise nach einem auswahldialog das Standart-Mail-Programm, in Diesem sind die Informationen der Verbindung bereits eingetragen |  |  |

### Auto-Completion

Requirements: Programm gestartet, Aktive Internetverbindung.

| Schritt | Aktivität | Erwartetes Resultat | Abweichendes Resultat | Erfüllt |
| :--- | :--- | :--- | :--- | :--- |
| 1 | In die Startstation-Textbox wird "Luz" eingegeben | Es öffnet sich eine ListBox welche "Luzern" aufgelistet hat |  |  |
| 2 | In der ListBox wird "Luzern" durch die Pfeiltasten ausgewählt und mit "Enter" bestätigt | in der Startstation-Textbox steht nun Luzern |  |  |

### Stationen in der Nähe

Requirements: Programm gestartet, Aktive Internetverbindung, 

| Schritt | Aktivität | Erwartetes Resultat | Abweichendes Resultat | Erfüllt |
| :--- | :--- | :--- | :--- | :--- |
| 1 | In die Startstation-Textbox wird "Luz" eingegeben | Es öffnet sich eine ListBox welche "Luzern" aufgelistet hat |  |  |
| 2 | In der ListBox wird "Luzern" durch die Pfeiltasten ausgewählt und mit "Enter" bestätigt | in der Startstation-Textbox steht nun Luzern |  |  |

