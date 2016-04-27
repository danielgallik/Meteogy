# Meteogy
Projekt na Extremne programovanie

Popis:
Aplikácia Meteogy bude slúžiť na zobrazovanie meteorologických vlastností istého územia. V databáze budú údaje z obmedzeného počtu senzorov,
ktoré nemusia byť rovnomerne rozložené. Aplikácia bude načítavať dáta z databázy a na základe potrieb užívateľa ich bude prepočítavať
do 2D mapy, ktorú výsledne zobrazí. Na výpočet neznámych oblastí využijeme poznatky zo štatistiky, kde bude potrebné naprogramovať niekoľko vzorcov.
Senzory budú získavať tieto údaje:
	Vlhkosť (pôdy/vzduchu),
	Teplota (denná/nočná),
	Rýchlosť vetra,
	Zrážky,
	Kvalita pôdy,
	Priame svetlo,
	Hlučnosť,
	Množstvo smogu,
	Poloha senzorov

	
Činnosti - Časová náročnosť

	1. Vytvorenie GUI
	2. Vytvorenie databázy
	3. Vytvorenie testov
	4. Načítavanie dát z databázy
	5. Prepočet GPS polôh senzorov do 2D mapy
	7. Výpočet ostatných bodov v mape
	8. Grafické zobrazenie


Iteracia 1 - 22 hodin


	1.	Vytvorit webove rozhranie – 2 hod
	2.	Dopyty na databazu – 6 hod
	3.	Zobrazenie 2D mapy – 4 hod
	4.	Vizualizacia udajov na 2d mape(podla GPS) – 10 hod


Iteracia 2 - 32 hodin


	5.	Odhadnutie hodnot aj pre nemonitorovane oblasti – 20 hod
	6.	Volba parametrov zobrazovania(Vlhkosť (pôdy/vzduchu), Teplota (denná/nočná), Rýchlosť vetra, Zrazky, Kvalita pody, Priame svetlo, Hlučnosť, Množstvo smogu) – 8 hod
	7.	Voľba podla časoveho rozmedzia(datum od do) – 4 hod


Iteracia 3 - 14 hodin


	8.	Zvolenie dolezitych faktorov – 4 hod
	9.	Validacia vstupov z formularov – 9 hod
	10.	Zobrazenie legendy k mape – 1 hod


Spolu - 68 hodin


Špecifikácia:

	1. Vyberte si jazyk, operačný systém, cieľové prostredie.
	C#, všetk, web
	
	2. Pre dané podmienky si vyberte vývojové prostredie.
	Visual Studio, Xamarin
	
	3. Pre dané vývojové prostredie si nájdite unit tester.
	Visual Studio Nunit
	
	4. Dohodnite si úložisko zdrojových kódov (git).
	github
	
	5. Dohodnite si pravidlá pre integráciu.

Členovia týmu:
Daniel Gallik, Jakub Piteľ, Peter Hlatky, Katarína Fabianová
