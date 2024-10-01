#include <stdio.h>
#include<conio.h>
#include<windows.h>
#define my 4
#define mx 4
char map[20][20] = {
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##0Y000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
        {"##00000000000000000"},
};
int x = 3, y = 2;
char ch = '\0';
void gotoxy(int xpos, int ypos)
{
    COORD scrn;
    HANDLE hOuput = GetStdHandle(STD_OUTPUT_HANDLE);
    scrn.X = xpos; scrn.Y = ypos;
    SetConsoleCursorPosition(hOuput, scrn);
}
void init(int ix, int iy) {
    x = ix;
    y = iy;
    for (int i = 0; i < 20; i++) {
        for (int j = 0; j < 20; j++) {
            printf("%c", map[i][j]);
        }
        printf("\n");
    }
}
void fps() {
    for (int i = 0; i < 20; i++) {
        for (int j = 0; j < 20; j++) {
            gotoxy(j+1,i);
            printf("\b%c", map[i][j]);
        }
        printf("\n");
    }    
    //printf("%d   %d", x, y);
}
void tps() {
    ch = '\0';
    Sleep(150);
    if (kbhit()) ch = getch();
    //if (true) ch = 'w';
    else Sleep(40);
    if (ch == 'w') {
        if (y - 1 < 0 || map[y-1][x] == '#') {
            ;
        }
        else {
            map[y][x] = '0';
            y -= 1;
            map[y][x] = 'Y';
        }
    }
    if (ch == 's') {
        if (map[y + 1][x] == 0 || map[y+1][x] == '#') {
            ;
        }
        else {
            map[y][x] = '0';
            y += 1;
            map[y][x] = 'Y';
        }
    }
    if (ch == 'a') {
        if (x - 1 < 0 || map[y][x-1] == '#') {
            ;
        }
        else {
            map[y][x] = '0';
            x -= 1;
            map[y][x] = 'Y';
        }
    }
    if (ch == 'd') {
        if (map[y][x + 1] == 0 || map[y][x+1] == '#') {
            ;
        }
        else {
            map[y][x] = '0';
            x += 1;
            map[y][x] = 'Y';
        }
    }
}
int main() {
    init(x,y);
    while (1) {
        tps();
        fps();
    }
    return 0;
}