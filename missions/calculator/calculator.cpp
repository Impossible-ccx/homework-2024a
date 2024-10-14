#include<stdio.h>
#include<memory.h>
#include"../stack/stack.cpp"
int main() {
	Stack* stackNum = createStack();
	char str[100];
	
	memset(str, 0, sizeof(str));

	scanf("%s", &str);
	char opera;
	int S1, S2;
	for (int i = 0; str[i] != 0; ++i) {
		if (str[i] == '+' || str[i] == '-' || str[i] == '*') {
			opera = str[i];
			S1 = pop(stackNum);
			S2 = pop(stackNum);
			if (opera == '+') {
				append(stackNum, S1 + S2);
			}
			if (opera == '-') {
				append(stackNum, S1 - S2);
			}
			if (opera == '*') {
				append(stackNum, S1 * S2);
			}
		}
		if (str[i] >= '0' && str[i] <= '9') {
			append(stackNum, str[i] - '0');
		}
	}

	printf("%d", top(stackNum));

	return 0;
}