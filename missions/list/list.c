#include<stdio.h>
#include<stdlib.h>
typedef struct Node {
	int val;
	struct Node* next;
}Node;
typedef struct List {
	struct Node* head;
}List;
void Add(List* target, int val) {
	Node* newNode = (Node*)malloc(sizeof(Node));
	newNode->val = val;
	if (target->head == NULL) {
		target->head = newNode;
		newNode->next = NULL;
	}
	else {
		newNode->next = target->head;
		target->head = newNode;
	}

}
void Delete(List* target) {
	if (target->head == NULL) {
		return;
	}
	else if (target->head->next == NULL) {
		free(target->head);
		target->head = NULL;
	}
	else {
		Node* track = target->head;
		target->head = track->next;
		free(track);
	}
}
int Peek(List* target) {
	if (target->head == NULL) {
		return 0;
	}
	else
	{
		return target->head->val;
	}
}
void AddAt(List* target, int val, int index) {
	Node* track = target->head;
	Node* newNode = (Node*)malloc(sizeof(Node));
	newNode->val = val;
	newNode->next = NULL;
	if (track == NULL) {
		target->head = newNode;
		return;
	}
	else {
		if (index == 0) {
			target->head = newNode;
			newNode->next = track;
			return;
		}
		else {
			for (int i = 0; i < index - 1; i++) {
				if (track->next == NULL) {
					track->next = newNode;
					return;
				}
				else {
					track = track->next;
				}
			}
			newNode->next = track->next;
			track->next = newNode;
			return;
		}
	}
}
int ReadAt(List* target, int index) {
	Node* track = target->head;
	if (track == NULL) {
		return 0;
	}
	else {
		for (int i = 0; i < index; i++) {
			if (track->next == NULL) {
				return 0;
			}
			else {
				track = track->next;
			}
		}
		return track->val;
	}
}
int main() {
	List* aList = (List*)malloc(sizeof(List));
	aList->head = NULL;
	for (int i = 0; i < 10; i++) {
		Add(aList, i * i - i + 4);
	}
	for (int i = 0; i < 10; i++) {
		printf("%d\n", Peek(aList));
		Delete(aList);
	}
	return 0;
}