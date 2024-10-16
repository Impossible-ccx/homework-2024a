#include<stdio.h>
#include<stdlib.h>
//��ʵ��deque
typedef struct Node {
	int value;
	struct Node* nextNode;
	struct Node* preNode;
}Node;
typedef struct Queue {
	Node emptyNode;
}Queue;
Queue* createQueue() {
	Queue* newQueue = NULL;
	while (newQueue == NULL) {
		newQueue = (Queue*)malloc(sizeof(Queue));
	}
	newQueue->emptyNode.nextNode = &(newQueue->emptyNode);
	newQueue->emptyNode.preNode = &(newQueue->emptyNode);
	return newQueue;
}
int IsEmpty(Queue* target) {
	return target->emptyNode.preNode == &(target->emptyNode);
}
void append(Queue* target, int value) {
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
int pop(Queue* target) {
	int val;
	if (IsEmpty(target)) {
		return -1;
	}
	else {
		val = target->emptyNode.preNode->value;
		target->emptyNode.preNode = target->emptyNode.preNode->preNode;
		free(target->emptyNode.preNode->nextNode);
		target->emptyNode.preNode->nextNode = &(target->emptyNode);
		return val;
	}
}
int top(Queue* target) {
	return target->emptyNode.preNode->value;
}
//int main() {
//	Queue* MyQueue = createQueue();
//	for (int i = 0; i < 5; i++) {
//		append(MyQueue, i);
//	}
//	for (int i = 0; i < 5; i++)
//	{
//		printf("%d", top(MyQueue));
//		pop(MyQueue);
//	}
//	printf("%d",IsEmpty(MyQueue));
//	return 0;
//}