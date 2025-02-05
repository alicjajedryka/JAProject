.CODE
calculateHistogramAsm PROC
    push rbp                   ; Zachowanie wska�nika bazowego
    mov rbp, rsp               ; Ustawienie nowego wska�nika bazowego
    sub rsp, 32                ; Wyr�wnanie stosu

    push r11                   ; Zachowanie rejestr�w og�lnego przeznaczenia
    push r12
    push r13

    mov rbx, rdx               ; Przechowanie liczby pikseli w RBX
    mov r11, r8                ; Wska�nik do histogramu R
    mov r12, r9                ; Wska�nik do histogramu G
    mov r13, qword ptr [rbp + 48] ; Wska�nik do histogramu B (argument na stosie)

    ; Inicjalizacja histogram�w
    xor rax, rax               ; Zera dla histogram�w
    mov [r11], rax
    mov [r12], rax
    mov [r13], rax

ProcessRemainingPixels:
    cmp rbx, 0                 ; Sprawdzenie, czy pozosta�y piksele
    je DonePixels              ; Je�li nie, zako�cz

    ; Wczytanie warto�ci kana��w R, G, B dla aktualnego piksela
    movzx rax, byte ptr [rcx]  ; Wczytanie warto�ci kana�u B
    inc dword ptr [r13 + rax * 4] ; Inkrementacja histogramu B

    movzx rax, byte ptr [rcx + 1] ; Wczytanie warto�ci kana�u G
    inc dword ptr [r12 + rax * 4] ; Inkrementacja histogramu G

    movzx rax, byte ptr [rcx + 2] ; Wczytanie warto�ci kana�u R
    inc dword ptr [r11 + rax * 4] ; Inkrementacja histogramu R

    add rcx, 3                 ; Przesuni�cie wska�nika do nast�pnego piksela
    dec rbx                    ; Zmniejszenie liczby pozosta�ych pikseli
    jmp ProcessRemainingPixels ; Powr�t do przetwarzania pikseli

DonePixels:
    xor rax, rax               ; Ustawienie kodu sukcesu na 0

    pop r13                    ; Przywr�cenie rejestr�w og�lnego przeznaczenia
    pop r12
    pop r11
    add rsp, 32                ; Przywr�cenie stosu
    pop rbp                    ; Przywr�cenie wska�nika bazowego
    ret                        ; Powr�t z procedury
calculateHistogramAsm ENDP
END