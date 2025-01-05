#include "pch.h"
#include "CppCliClass.h"
#include "../CAPI/CLIAPI.h"
#include "../EXPAI/EXPAI.h"
#include "../EXPAI/GobangAI.h"
#include "../EXPAI/Inner.h"
#include<vector>
//#pragma comment(lib, "./EXPAI.lib")
#pragma managed
using namespace EXPAI;
namespace CppCliClass
{
	extern int Gmaxy;
	extern int Gmaxx;
	extern PlayerType OnlinePlayer;
	extern PlayerType OfflinePlayer;
	WpfApp::Node AIPlayer::Draw(array<WpfApp::DrawType, 2>^ map, WpfApp::PlayerSide u, int maxx, int maxy)
	{
		CDrawMap cmap;
		for (int i = 0; i < maxx; i++)
		{
			cmap.map.push_back(*(new std::vector<CDrawType>()));
			for (int j = 0; j < maxy; j++)
			{
				switch (map[i, j])
				{
				case WpfApp::DrawType::Player1:
					cmap.map[i].push_back(DPlayer1);
					break;
				case WpfApp::DrawType::Player2:
					cmap.map[i].push_back(DPlayer2);
					break;
				case WpfApp::DrawType::Empty:
					cmap.map[i].push_back(Empty);
					break;
				case WpfApp::DrawType::Wall:
					cmap.map[i].push_back(Wall);
					break;
				}
			}
		}
		auto g = CPlayerSide::Player1;
		switch (u)
		{
		case WpfApp::PlayerSide::Player1:
			g = CPlayerSide::Player1;
			break;
		case WpfApp::PlayerSide::Player2:
			g = CPlayerSide::Player2;
			break;
		}
		CNode cn = EXPAI::Draw(cmap, g);
		WpfApp::Node result;
		result.X = cn.x;
		result.Y = cn.y;
		return result;
	}
	void AIPlayer::OnFailToDraw(array<WpfApp::DrawType, 2>^ map, WpfApp::Node preDraw)
	{
		;
	}
}
