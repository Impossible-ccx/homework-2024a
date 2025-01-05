#pragma once
namespace GobangAI
{
#define MaxSize 12
	struct GNode
	{
		int x;
		int y;
		int dv;
	};

	class GobangMap
	{
	public:
		GobangMap();
		char Map[12][MaxSize];
		char Peek(GNode target);
		void Draw(GNode target, char input);
	};
	void IniLinkVType();
	GNode GDraw(GobangMap goMap);
	enum PlayerType
	{
		Player1 = 'x',
		Player2 = 'v'
	};
}


