#include <stdio.h>
//��������棬�Ͽ��������𣿹����ÿ������룬���ٶ�������Сд��ĸ
char str[100];
void encrypt(int index) {
    for (int i = 0; str[i] != 0; i++) {
        str[i] += index;
        if (str[i] > 'x') {
            str[i] -= 26;
        }
    }
}
void decrypt(int index) {
    for (int i = 0; str[i] != 0; i++) {
        str[i] -= index;
        if (str[i] < 'a') {
            str[i] += 26;
        }
    }
}
int main() {
    scanf("%s", &str);
    encrypt(9);
    printf("%s\n", str);
    decrypt(9);
    printf("%s", str);
    return 0;
}