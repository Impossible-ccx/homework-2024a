#include<stdio.h>
#include<conio.h>
#include<stdlib.h>
#include<windows.h>
typedef struct good {
	int ID;
	int Num;
	char Name[100];
} Good;
void mainIni();
char goodList() {
	system("cls");
	printf("1.退出\n");
	printf("2.入库\n");
	printf("3.出库\n");
	FILE* fp = fopen("./store.dat", "r");
	Good aGood;
	int ifRead = 0;
	printf("ID\tName\t\tNum\n");
	while (1) {
		ifRead = fread(&aGood, sizeof(Good), 1, fp);
		if (ifRead == 1) {
			printf("%d\t%s\t\t%d\n", aGood.ID, aGood.Name, aGood.Num);
		}
		else {
			break;
		}
	}
	char ch = 0;
	while (1) {
		Sleep(100);
		ch = getch();
		if (ch == '1') {
			fclose(fp);
			return '0';
		}
		if (ch == '2') {
			fclose(fp);
			return '2';
		}
		if (ch == '3') {
			fclose(fp);
			return '3';
		}
	}
}
char goodInput() {
	system("cls");
	FILE* fp = fopen("./store.dat", "r+");
	Good aGood;	
	int ifExist = 0;
	int tID;
	int tNum;
	int ifRead = 0;
	printf("ID\tName\t\tNum\n");
	while (1) {
		ifRead = fread(&aGood, sizeof(Good), 1, fp);
		if (ifRead == 1) {
			printf("%d\t%s\t\t%d\n", aGood.ID, aGood.Name, aGood.Num);
		}
		else {
			break;
		}
	}
	rewind(fp);
	printf("输入ID：");
	scanf("%d", &tID);
	while (!feof(fp)) {
		fread(&aGood, sizeof(Good), 1, fp);
		if (aGood.ID == tID) {
			ifExist = 1;
			break;
		}
	}
	if (ifExist) {
		fseek(fp, -sizeof(Good), SEEK_CUR);
		printf("已存在的货物，输入数量：");
		scanf("%d", &tNum);
		aGood.Num += tNum;
		fwrite(&aGood, sizeof(Good), 1, fp);
	}
	else {
		aGood.ID = tID;
		printf("新货物，输入名称：");
		scanf("%s", &aGood.Name);
		printf("输入数量：");
		scanf("%d", &aGood.Num);
		fwrite(&aGood, sizeof(Good), 1, fp);
	}
	fclose(fp);
	return '1';
}
char goodOutput() {
	system("cls");
	FILE* fp = fopen("./store.dat", "r+");
	FILE* rp = fopen("./td.dat", "w");
	Good aGood;
	int ifRead = 0;
	printf("ID\tName\t\tNum\n");
	while (1) {
		ifRead = fread(&aGood, sizeof(Good), 1, fp);
		if (ifRead == 1) {
			printf("%d\t%s\t\t%d\n", aGood.ID, aGood.Name, aGood.Num);
		}
		else {
			break;
		}
	}
	int targetId, targetNum;
	printf("输入出库品ID：");
	scanf("%d", &targetId);
	rewind(fp);
	while (1) {
		ifRead = fread(&aGood, sizeof(Good), 1, fp);
		if (ifRead == 1) {
			if (targetId == aGood.ID) {
				fseek(fp, -sizeof(Good), SEEK_CUR);
				break;
			}
			fwrite(&aGood, sizeof(aGood), 1, rp);
		}
		else {
			printf("无此产品！\n");
			Sleep(1000);
			return '1';
		}
	}

	printf("输入出库数量：");
	scanf("%d", &targetNum);
	if (targetNum > aGood.Num) {
		printf("数量不足！\n");
		Sleep(1000);
		return '1';
	}
	if (targetNum < aGood.Num) {
		aGood.Num -= targetNum;
		fwrite(&aGood, sizeof(Good), 1, fp);
		fclose(fp);

	}
	if (targetNum == aGood.Num) {
		printf("全部取出，是否删除存货记录？1.删除 2.保留\n");
		char ch = 0;
		while (1) {
			Sleep(100);
			ch = getch();
			if (ch == '1') {
				fread(&aGood, sizeof(Good), 1, fp);
				while (fread(&aGood, sizeof(Good), 1, fp)) {
					fwrite(&aGood, sizeof(aGood), 1, rp);
				}
				fclose(fp);
				fclose(rp);
				remove("./store.dat");
				rename("./td.dat", "./store.dat");
				break;
			}
			if (ch == '2') {
				aGood.Num = 0;
				fwrite(&aGood, sizeof(Good), 1, fp);
				fclose(fp);
				fflush(rp);
				fclose(rp);
				remove("./td.dat");
				break;
			}
		}
	}
	fflush(rp);
	fclose(rp);
	remove("./td.dat");
	return '1';
}
void mainIni() {
	aFlag:
	system("cls");
	printf("1.显示货物列表\n");
	printf("2.入库\n");
	printf("3.出库\n");
	printf("4.退出\n");
	printf("5.重置数据库\n");
	char ch = 0;
	while (1) {
		Sleep(100);
		ch = getch();
		while (1) {
			if (ch == '0') {
				goto aFlag;
			}
			if (ch == '1') {
				ch = goodList();
				continue;
			}
			if (ch == '2') {
				Sleep(1000);
				ch = goodInput();
				continue;
			}
			if (ch == '3') {
				Sleep(1000);
				ch = goodOutput();
				continue;
			}
			if (ch == '5') {
				ch = '0';
				FILE* fp = fopen("./store.dat", "w");
				fclose(fp);
				continue;
			}
			if (ch == '4') {
				return;
			}
			break;
		}

	}
}
int main() {
	mainIni();
	return 0;
}