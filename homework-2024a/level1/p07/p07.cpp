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
            if (x - 1 < 0 || map[x-1][y] == '#') {
                ;
            }
            else {
                map[x][y] = '0';
                map[x-1][y] = 'Y';
                x -= 1;
            }
		}
		if (ch == 's') {
            if (map[x+1][y] == 0 || map[x+1][y] == '#') {
                ;
            }
            else {
                map[x][y] = '0';
                map[x+1][y] = 'Y';
                x += 1;
            }
        }
        Sleep(150);
    }
    else {
        Sleep(40);
        }
    }
    if (ch == 'd') {
    init(5,5);
            map[x][y] = '0';
            x += 1;
            map[x][y] = 'Y';
=========
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
>>>>>>>>> Temporary merge branch 2
        }
    }
}
int main() {
<<<<<<<<< Temporary merge branch 1
=========
    init(x,y);
>>>>>>>>> Temporary merge branch 2
    while (1) {
        tps();
        fps();
    }
    return 0;
}