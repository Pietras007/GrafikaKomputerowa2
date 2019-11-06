Projekt Grafika 2

1. Informacje wstępne:
Do poprawnego i szybkiego działania aplikacji wymagany jest wydajny procesor wielordzeniowy.
Pamięć RAM jest praktycznie nieistotna.
Z racji iż aplikacja jest wielowątkowa, idealne warunki wykorzystania sprzętu następują w momencie, gdy ilość trójkątów na ekranie jest równa wielokrotności ilości wątków procesora.

2. Obsługa:

Tryb pracy: 
"Wyłącz siatkę" - wyłącza renderowanie siatki opcja zalecana w przypadku ilości trójkątów powyżej 50x50 (DrawLine jest bardzo, bardzo powolne)
"Przesuwanie"(checkbox) - pozwala uruchomić możliwość przesuwania krawędzi, ta opcja jest niezależna od reszty programu, krawędzie można przesuwać podczas działania algorytmu.

"Światło daleko", "Wędrujace światło" - ustawia tryb działania światła
Wędrujace światło jest zsynchronizowane z generowaniem kolejnych klatek, dla szybszych algorytmów będzie się poruszało szybciej, tak samo dotyczy się to szybszych i wolniejszych komputerów

Wypełnienie:
"Tekstura", "Jednolity kolor tła" - ustawia tryb wypełniania obrazka
"Wybierz kolor" - pozwala na wybranie koloru tła (dla opcji "Jednolity kolor tła")

Rodzaj malowania:
"Dokładne" - opcja 1 
"Interpolowane" - opcja 2
"Hybrydowe" - opcja 3
Działanie algorytmów zgodnie ze specyfikacją.

Wektor N:
"Stały [0,0,1]", "Z tekstury" - odpowiada za wybór wektora normalnego, albo stały, albo pobrany z NormalMapy

Współczynniki:
"Losowe"(checkbox) - ustawia losowe wartości kd, ks i m dla każdego trójkąta
kd, ks (suwaki) - współczynniki opisujące wpływ danej składowej na wynik (0 - 1), nie sumują się do 1
m (suwak) - współczynnik opisujący jak bardzo dany trókat jest zwierciadlany (1-100)

Dodatkowe ustawienia:
"Kolor światła" - pozwala wybrac kolor światła
Ilość trójkątów - po kliknięciu na "OK" ustawia ilosć trójkątów do pomalowania 

3. Inna NormalMapa, albo Tekstura:
Należy w katalogu z plikiem Grafika.exe podmienić plik
"picture.jpg" - Tekstura
"normal.jpg" - NormalMapa
Zalecane minimalne wymiary 725 x 580px 