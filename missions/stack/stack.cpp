#include<stdio.h>
#include<stdlib.h>
//双向循环链表全杀
typedef struct Node {
	int value;
	struct Node* nextNode;
	struct Node* preNode;
}Node;
typedef struct Queue {
	Node emptyNode;
}Stack;
Stack* createStack() {
	Stack* newStack = NULL;
	while (newStack == NULL) {
		newStack = (Queue*)malloc(sizeof(Queue));
	}
	newStack->emptyNode.nextNode = &(newStack->emptyNode);
	newStack->emptyNode.preNode = &(newStack->emptyNode);
	return newStack;
}
int IsEmpty(Stack* target) {
	return target->emptyNode.preNode == &(target->emptyNode);
}
void append(Stack* target, int value) {
	Node* newNode = NULL;
	while (newNode == NULL) {
		newNode = (Node*)malloc(sizeof(Node));
	}
	newNode->value = value;
	newNode->nextNode = target->emptyNode.nextNode;
	newNode->preNode = &(target->emptyNode);
	target->emptyNode.nextNode->preNode = newNode;
	target->emptyNode.nextNode = newNode;
}
void pop(Stack* target) {
	if (IsEmpty(target)) {
		;
	}
	else {
		target->emptyNode.nextNode = target->emptyNode.nextNode->nextNode;
		free(target->emptyNode.nextNode->preNode);
		target->emptyNode.nextNode->preNode = &(target->emptyNode);
	}
}
int top(Stack* target) {
	return target->emptyNode.nextNode->value;
}
int main() {
	Stack* MyStack = createStack();
	for (int i = 0; i < 5; i++) {
		append(MyStack, i);
	}
	for (int i = 0; i < 5; i++)
	{
		printf("%d", top(MyStack));
		pop(MyStack);
	}
	printf("%d", IsEmpty(MyStack));
	return 0;
}