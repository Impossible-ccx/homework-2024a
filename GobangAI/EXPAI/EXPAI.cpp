// EXPAI.cpp : 定义静态库的函数。
//
#include "framework.h"
#include "../CAPI/CLIAPI.h"
#include <vector>
#include "GoBangAI.h"
#include "EXPAI.h"
#include"Inner.h"
using namespace GobangAI;
// TODO: 这是一个库函数示例
using namespace CAPI;
using namespace Inner;
namespace EXPAI
{

	bool IsInitialized = false;
	CNode Draw(CDrawMap map, CPlayerSide pla)
	{
		if (IsInitialized == false)
		{
			Gmaxx = map.map.size();
			Gmaxy = map.map[0].size();
			IniLinkVType();			
			OnlinePlayer = PlayerType::Player1;
			OfflinePlayer = PlayerType::Player2;
		}
		GobangMap gobangmap;
		for (int i = 0; i < Gmaxx; i++)
		{
			for (int j = 0; j < Gmaxy; j++)
			{
				if (map.map[i][j] == CDrawType::Empty || map.map[i][j] == CDrawType::Wall)
				{
					gobangmap.Map[i][j] = '0';
				}
				else
				{
					if (map.map[i][j] == CDrawType::DPlayer1 && pla == CPlayerSide::Player1
						|| map.map[i][j] == CDrawType::DPlayer2 && pla == CPlayerSide::Player2)
					{
						gobangmap.Map[i][j] = PlayerType::Player1;
					}
					else
					{
						gobangmap.Map[i][j] = PlayerType::Player2;
					}
				}
			}
		}
		OnlinePlayer = PlayerType::Player2;
		OfflinePlayer = PlayerType::Player1;
		GNode res = GDraw(gobangmap);
		CNode node;
		node.x = res.x;
		node.y = res.y;
		return node;
	}
}
