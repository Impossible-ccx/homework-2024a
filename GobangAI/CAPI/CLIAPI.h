#pragma once
#pragma unmanaged
#include<vector>
namespace CAPI
{
	struct CNode
	{
	public:
		int x;
		int y;
	};
	enum CPlayerSide
	{
		Player1,
		Player2,
	};
	enum CDrawType
	{
		DPlayer1,
		DPlayer2,
		Empty,
		Wall
	};
	struct CDrawMap
	{
	public:
		std::vector<std::vector<CDrawType>> map;
	};
}
