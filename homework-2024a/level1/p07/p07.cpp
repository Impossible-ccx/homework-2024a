#include <stdio.h>
#include<conio.h>
#include<windows.h>
#define my 4
#define mx 4
char map[20][20] = {
};
void fps() {
    for (int i = 0; i < 20; i++) {
        for (int j = 0; j < 20; j++) {
        }
        printf("\n");
    }
    //printf("%d   %d", x, y);
}
void tps() {
    if (ch == 'w') {
            ;
        }
        else {
            y -= 1;
        }
    }
    if (ch == 's') {
            ;
        }
        else {
            y += 1;
        }
    }
    if (ch == 'a') {
            ;
        }
        else {
            x -= 1;
        }
    }
    if (ch == 'd') {
            ;
        }
        else {
            x += 1;
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