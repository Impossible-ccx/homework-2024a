#include <stdio.h>
#include<conio.h>
#include<windows.h>
void gotoxy(int xpos, int ypos)
{
    COORD scrn;
    HANDLE hOuput = GetStdHandle(STD_OUTPUT_HANDLE);
    scrn.X = xpos; scrn.Y = ypos;
    SetConsoleCursorPosition(hOuput, scrn);
}
char map[20][20] = {
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
    {"##00000000000000000"}
};
int x, y;
char ch = '\0';
void init(int ix, int iy) {
    x = ix;
    y = iy;
    map[ix][iy] = 'Y';
}
void fps() {
    for (int i = 0; i < 20; i++) {
        for (int j = 0; j < 20; j++) {
            gotoxy(i + 1, j);
            printf("\b%c", map[j][i]);
        }
        gotoxy(i + 1, 19);
        printf("\b\n");
    }
    //printf("%d   %d", x, y);
}
void tps() {

    if (kbhit()) {
        ch = getch();
        if (ch == 'a') {
            if (y - 1 < 0 || map[x][y - 1] == '#') {
                ;
            }
            else {
                map[x][y] = '0';
                map[x][y - 1] = 'Y';
                y -= 1;
            }
        }
        if (ch == 'd') {
            if (map[x][y + 1] == 0 || map[x][y + 1] == '#') {
                ;
            }
            else {
                map[x][y] = '0';
                map[x][y + 1] = 'Y';
                y += 1;
            }
        }
        if (ch == 'w') {
            if (x - 1 < 0 || map[x - 1][y] == '#') {
                ;
            }
            else {
                map[x][y] = '0';
                map[x - 1][y] = 'Y';
                x -= 1;
            }
        }
        if (ch == 's') {
            if (map[x + 1][y] == 0 || map[x + 1][y] == '#') {
                ;
            }
            else {
                map[x][y] = '0';
                map[x + 1][y] = 'Y';
                x += 1;
            }
        }
        Sleep(150);
    }
    else {
        Sleep(40);
    }
}
int main() {
    init(5, 5);
    while (1) {
        tps();
        fps();
    }
    return 0;
}