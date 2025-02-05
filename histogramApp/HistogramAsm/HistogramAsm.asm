.CODE
calculateHistogramAsm PROC
    push rbp                   ; Zachowanie wskaŸnika bazowego
    mov rbp, rsp               ; Ustawienie nowego wskaŸnika bazowego
    sub rsp, 32                ; Wyrównanie stosu

    push r11                   ; Zachowanie rejestrów ogólnego przeznaczenia
    push r12
    push r13

    mov rbx, rdx               ; Przechowanie liczby pikseli w RBX
    mov r11, r8                ; WskaŸnik do histogramu R
    mov r12, r9                ; WskaŸnik do histogramu G
    mov r13, qword ptr [rbp + 48] ; WskaŸnik do histogramu B (argument na stosie)

    ; Inicjalizacja histogramów
    xor rax, rax               ; Zera dla histogramów
    mov [r11], rax
    mov [r12], rax
    mov [r13], rax

ProcessRemainingPixels:
    cmp rbx, 0                 ; Sprawdzenie, czy pozosta³y piksele
    je DonePixels              ; Jeœli nie, zakoñcz

    ; Wczytanie wartoœci kana³ów R, G, B dla aktualnego piksela
    movzx rax, byte ptr [rcx]  ; Wczytanie wartoœci kana³u B
    inc dword ptr [r13 + rax * 4] ; Inkrementacja histogramu B

    movzx rax, byte ptr [rcx + 1] ; Wczytanie wartoœci kana³u G
    inc dword ptr [r12 + rax * 4] ; Inkrementacja histogramu G

    movzx rax, byte ptr [rcx + 2] ; Wczytanie wartoœci kana³u R
    inc dword ptr [r11 + rax * 4] ; Inkrementacja histogramu R

    add rcx, 3                 ; Przesuniêcie wskaŸnika do nastêpnego piksela
    dec rbx                    ; Zmniejszenie liczby pozosta³ych pikseli
    jmp ProcessRemainingPixels ; Powrót do przetwarzania pikseli

DonePixels:
    xor rax, rax               ; Ustawienie kodu sukcesu na 0

    pop r13                    ; Przywrócenie rejestrów ogólnego przeznaczenia
    pop r12
    pop r11
    add rsp, 32                ; Przywrócenie stosu
    pop rbp                    ; Przywrócenie wskaŸnika bazowego
    ret                        ; Powrót z procedury
calculateHistogramAsm ENDP
END