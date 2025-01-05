#include"CppCliClass.h"
int main()
{
	array<WpfApp::DrawType, 2>^ map = gcnew array<WpfApp::DrawType, 2>(12, 12);
	for (int i = 0; i < 12 * 12; i++)
	{

		map[i / 12, i % 12] = DrawType::Empty;
	}
	WpfApp::PlayerSide u = WpfApp::PlayerSide::Player1;
	int maxx = 12;
	int maxy = 12;
	auto p = gcnew CppCliClass::AIPlayer();
	p->Draw(map, u, 12, 12);
	return 0;
}