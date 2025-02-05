// Temat projektu: Obliczanie histogramu kanałów kolorów obrazu RGB w języku C.
// Krótki opis algorytmu: Program analizuje obraz zapisany w formacie BGR i oblicza histogramy dla każdego kanału kolorów (R, G, B).
// Data wykonania projektu: Styczeń 2025, Semestr Zimowy / Rok akademicki 2024/2025
// Autor: Alicja Jędryka
// Aktualna wersja programu: 1.0
// Historia zmian: 1.0 - Pierwsza wersja implementacji obliczania histogramu RGB.

#include "pch.h"
#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>

// Deklaracja funkcji eksportowanej z biblioteki DLL
// Nazwa: calculateHistogram
// Krótki opis: Funkcja oblicza histogramy dla kanałów R, G i B na podstawie danych obrazu w formacie BGR.
// Parametry wejściowe:
//   - const unsigned char* imageData: Wskaźnik do danych obrazu w formacie BGR (każdy piksel to 3 bajty: B, G, R).
//   - int pixelCount: Liczba pikseli w obrazie (>= 0).
//   - int* histogram: Wskaźnik do tablicy histogramu kanału R (256 elementów).
//   - int* histogram2: Wskaźnik do tablicy histogramu kanału G (256 elementów).
//   - int* histogram3: Wskaźnik do tablicy histogramu kanału B (256 elementów).
// Parametry wyjściowe:
//   - histogram, histogram2, histogram3: Wypełnione histogramy intensywności dla kanałów R, G i B.
// Wartość zwracana:
//   - 0: Sukces.
//   - -1: Błąd wejściowy (np. null wskaźnik).

__declspec(dllexport) int calculateHistogram(const unsigned char* imageData, int pixelCount, int* histogram, int* histogram2, int* histogram3) {
    if (!imageData || !histogram || !histogram2 || !histogram3)
        return -1; // Sprawdzenie, czy wskaźniki wejściowe są poprawne

    // Pętla przetwarzająca każdy piksel
    for (int i = 0; i < pixelCount; i++) {
        unsigned char blue = imageData[i * 3];     // Pobranie wartości kanału B
        unsigned char green = imageData[i * 3 + 1]; // Pobranie wartości kanału G
        unsigned char red = imageData[i * 3 + 2];   // Pobranie wartości kanału R

        // Inkrementacja liczników histogramu dla odpowiednich poziomów intensywności
        histogram[red]++;    // Zwiększenie liczby dla kanału R
        histogram2[green]++; // Zwiększenie liczby dla kanału G
        histogram3[blue]++;  // Zwiększenie liczby dla kanału B
    }

    return 0; // Sukces
}
