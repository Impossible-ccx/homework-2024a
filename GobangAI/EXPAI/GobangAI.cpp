#include<algorithm>
#include<memory>
#include<unordered_map>
#include <string>
#include<stdio.h>
#include<stdlib.h>
#include<memory.h>
#include<math.h>
#include <stdexcept>
#include<queue>
#include"Inner.h"
#include "GoBangAI.h"
using namespace std; 
using namespace Inner;
namespace GobangAI
{
#define Max_Depth 4
#define var auto
	bool CmpGreater(GNode& a, GNode& b)
	{
		return a.dv > b.dv;
	}
	bool CmpLess(GNode& a, GNode& b)
	{
		return a.dv < b.dv;
	}

	GobangMap::GobangMap()
	{
		for (int y = 0; y < Gmaxy; y++)
		{
			for (int x = 0; x < Gmaxx; x++)
			{
				Map[x][y] = '0';
			}
		}
	}
	char GobangMap::Peek(GNode target)
	{
		return Map[target.x][target.y];
	}
	void GobangMap::Draw(GNode target, char input)
	{
		if (target.x < Gmaxx && target.x >= 0 && target.y < Gmaxy && target.y >= 0)
		{
			Map[target.x][target.y] = input;
		}
	}
	struct Link
	{
		char str[9];
	};
	static pair<int, int> linkVValue[4][4][4][4][4][4][4][4][4];
	static unordered_map<string, pair<int, int>> LinkVType;
	int IniComputeLinkV(char* link)
	{
		int connectl = 0;
		int connectr = 0;
		int aLive = 2;
		if (link[3] == OnlinePlayer)
		{
			if (link[3] == OnlinePlayer) connectl++;
			if (link[2] == OnlinePlayer)
			{
				if (link[2] == OnlinePlayer) connectl++;
				if (link[1] == OnlinePlayer)
				{
					if (link[1] == OnlinePlayer) connectl++;
					if (link[0] == OnlinePlayer)
					{
						if (link[0] == OnlinePlayer) connectl++;
					}
					else if (link[1] == 'b' || link[1] == OfflinePlayer)
					{
						aLive--;
					}
				}
				else if (link[1] == 'b' || link[1] == OfflinePlayer)
				{
					aLive--;
				}
			}
			else if (link[2] == 'b' || link[2] == OfflinePlayer)
			{
				aLive--;
			}
		}
		else if (link[3] == 'b' || link[3] == OfflinePlayer)
		{
			aLive--;
		}
		if (link[5] == OnlinePlayer)
		{
			if (link[5] == OnlinePlayer) connectr++;
			if (link[6] == OnlinePlayer)
			{
				if (link[6] == OnlinePlayer) connectr++;
				if (link[7] == OnlinePlayer)
				{
					if (link[7] == OnlinePlayer) connectr++;
					if (link[8] == OnlinePlayer)
					{
						if (link[8] == OnlinePlayer) connectr++;
					}
					else if (link[7] == 'b' || link[7] == OfflinePlayer)
					{
						aLive--;
					}
				}
				else if (link[7] == 'b' || link[7] == OfflinePlayer)
				{
					aLive--;
				}
			}
			else if (link[6] == 'b' || link[6] == OfflinePlayer)
			{
				aLive--;
			}
		}
		else if (link[5] == 'b' || link[5] == OfflinePlayer)
		{
			aLive--;
		}
		int connect;
		if (link[4] == '0')
		{
			connect = max(connectl, connectr);
		}
		else if (link[4] == OfflinePlayer)
		{
			connect = max(connectl, connectr);
			aLive--;
		}
		else
		{
			connect = connectl + connectr + 1;
		}
		if (connect >= 5)
		{
			return (int)1e8;
		}
		if (connect == 4 && aLive >= 2)
		{
			return (int)1e7;
		}
		if (connect == 4 && aLive == 1)
		{
			return (int)1e4;
		}
		if (connect == 3 && aLive >= 2)
		{
			return (int)1e4;
		}
		if (connect == 3 && aLive == 1)
		{
			return 200;
		}
		if (connect == 2)
		{
			return 10;
		}
		if (connect == 1)
		{
			return 1;
		}
		return 0;
	}
	void IniLinkVType()
	{
		int status[9];
		int p = 0;
		char link[9];
		string str = "";
		memset(status, 0, 36);
		while (p >= 0)
		{
			if (p == 9)
			{
				str.append(link, 9);
				pair<int, int> ps;
				OnlinePlayer = Player1;
				OfflinePlayer = Player2;
				ps.first = IniComputeLinkV(link);
				OnlinePlayer = Player2;
				OfflinePlayer = Player1;
				ps.second = IniComputeLinkV(link);
				//LinkVType[str] = ps;
				linkVValue[status[0] - 1][status[1] - 1][status[2] - 1][status[3] - 1][status[4] - 1][status[5] - 1][status[6] - 1][status[7] - 1][status[8] - 1] = ps;
				str.clear();
				p--;
				continue;
			}
			if (status[p] == 0)
			{
				link[p] = '0';
			}
			else if (status[p] == 1)
			{
				link[p] = 'x';
			}
			else if (status[p] == 2)
			{
				link[p] = 'v';
			}
			else if (status[p] == 3)
			{
				link[p] = 'b';
			}
			else
			{
				status[p] = 0;
				p--;
				continue;
			}
			status[p]++;
			p++;
		}
	}
	int ComputeLinkV(Link& link)
	{
		int temp[9];
		for (int i = 0; i < 9; i++)
		{
			switch (link.str[i])
			{
			case '0':
				temp[i] = 0;
				break;
			case 'x':
				temp[i] = 1;
				break;
			case 'v':
				temp[i] = 2;
				break;
			case 'b':
				temp[i] = 3;
				break;
			}
		}
		//first for player1
		var temp1 = linkVValue[temp[0]][temp[1]][temp[2]][temp[3]][temp[4]][temp[5]][temp[6]][temp[7]][temp[8]];
		return temp1.first - temp1.second;
	}
	void Reverse()
	{
		PlayerType t = OnlinePlayer;
		OnlinePlayer = OfflinePlayer;
		OfflinePlayer = t;
	}
	//评价一步棋的价值改变量，-1放弃评价
	static int ComputeDeltaValue(GNode& DrawTarget, GobangMap& presentMap, int leftDepth)
	{
		//可展开，省一半时间
		if (DrawTarget.x == -1 && DrawTarget.y == -1)
		{
			return 0;
		}
		Link link1;
		Link link2;
		Link link3;
		Link link4;
		for (int i = -4; i <= 4; i++)
		{
			if (DrawTarget.x + i < 0 || DrawTarget.x + i >= Gmaxx)
			{
				link1.str[4 + i] = 'b';
			}
			else
			{
				link1.str[4 + i] = presentMap.Map[DrawTarget.x + i][DrawTarget.y];
			}
		}
		for (int i = -4; i <= 4; i++)
		{
			if (DrawTarget.y + i < 0 || DrawTarget.y + i >= Gmaxy)
			{
				link2.str[4 + i] = 'b';
			}
			else
			{
				link2.str[4 + i] = presentMap.Map[DrawTarget.x][DrawTarget.y + i];
			}
		}
		for (int i = -4; i <= 4; i++)
		{
			if (DrawTarget.x + i < 0 || DrawTarget.x + i >= Gmaxx || DrawTarget.y + i < 0 || DrawTarget.y + i >= Gmaxy)
			{
				link3.str[4 + i] = 'b';
			}
			else
			{
				link3.str[4 + i] = presentMap.Map[DrawTarget.x + i][DrawTarget.y + i];
			}
		}
		for (int i = -4; i <= 4; i++)
		{
			if (DrawTarget.x + i < 0 || DrawTarget.x + i >= Gmaxx || DrawTarget.y - i< 0 || DrawTarget.y - i >= Gmaxy)
			{
				link4.str[4 + i] = 'b';
			}
			else
			{
				link4.str[4 + i] = presentMap.Map[DrawTarget.x + i][DrawTarget.y - i];
			}
		}
		bool flag;
		PlayerType nup;
		if (leftDepth % 2 == 1 ? OnlinePlayer : OfflinePlayer == Player1)
		{
			flag = true;
			nup = Player1;
		}
		else
		{
			flag = false;
			nup = Player2;
		}
		int sum1 = 0;

		sum1 += ComputeLinkV(link1);
		sum1 += ComputeLinkV(link2);
		sum1 += ComputeLinkV(link3);
		sum1 += ComputeLinkV(link4);
		link1.str[4] = nup;
		link2.str[4] = nup;
		link3.str[4] = nup;
		link4.str[4] = nup;
		int sum2 = 0;
		sum2 += ComputeLinkV(link1);
		sum2 += ComputeLinkV(link2);
		sum2 += ComputeLinkV(link3);
		sum2 += ComputeLinkV(link4);

		return flag ? sum2 - sum1 : sum1 - sum2;
	}
	//获取可用下棋点
	vector<GNode> GetAvailableDraws(GobangMap& presentMap, int leftDepth, GNode& LastDraw)
	{
		vector<GNode> availableDraws;
		for (int x = 0; x < Inner::Gmaxx; ++x)
		{
			for (int y = 0; y < Inner::Gmaxy; ++y)
			{
				if (presentMap.Map[x][y] == '0')
				{
					GNode avd;
					avd.x = x;
					avd.y = y;
					avd.dv = ComputeDeltaValue(avd, presentMap, leftDepth - 1);
					availableDraws.push_back(avd);
				}
			}
		}
		if (leftDepth % 2 == 1)
		{
			sort(availableDraws.begin(), availableDraws.end(), CmpLess);
		}
		else
		{
			sort(availableDraws.begin(), availableDraws.end(), CmpGreater);
		}
		return availableDraws;
	}
	struct ChoiceNode
	{
		int Alpha;
		int Beta;
		int DeltaValue;
		int Value;
		GNode BestDraw;
	};
	static ChoiceNode DFSID(int leftDepth, ChoiceNode& fatherChoiceNode, GNode drawTarget, GobangMap& fatherMap)
	{
		ChoiceNode thisChoiceNode = fatherChoiceNode;
		GobangMap presentMap = fatherMap;

		thisChoiceNode.DeltaValue += drawTarget.dv;
		presentMap.Draw(drawTarget, leftDepth % 2 == 1 ? OnlinePlayer : OfflinePlayer);

		if (leftDepth == 0)
		{
			thisChoiceNode.Value = thisChoiceNode.DeltaValue;
			return thisChoiceNode;
		}
		else
		{
			vector<GNode> availableDraws = GetAvailableDraws(presentMap, leftDepth, drawTarget);

			int index = 0;
			//击杀剪枝
			if (availableDraws.front().dv > 1e6)
			{
				thisChoiceNode.Value = thisChoiceNode.DeltaValue;
				thisChoiceNode.BestDraw = availableDraws.front();
				return thisChoiceNode;
			}
			else
			{
				for (const GNode& nextDrawNode : availableDraws)
				{

					ChoiceNode childNode = DFSID(leftDepth - 1, thisChoiceNode, nextDrawNode, presentMap);
					//更新父节点并alpha beta剪枝


					if (leftDepth % 2 == 0)
					{
						if (childNode.Value > thisChoiceNode.Alpha)
						{
							thisChoiceNode.Value = childNode.Value;
							thisChoiceNode.Alpha = childNode.Value;
							thisChoiceNode.BestDraw = nextDrawNode;
						}
					}
					else
					{
						if (childNode.Value < thisChoiceNode.Beta)
						{
							thisChoiceNode.Value = childNode.Value;
							thisChoiceNode.Beta = childNode.Value;
							thisChoiceNode.BestDraw = nextDrawNode;
						}
					}
					if (thisChoiceNode.Beta <= thisChoiceNode.Alpha)
					{
						return thisChoiceNode;
					}
				}
			}

			return thisChoiceNode;
		}
	}

	GNode GDraw(GobangMap goMap)
	{
		ChoiceNode Start;
		Start.Alpha = 0x80000000;
		Start.Beta = 0x7fffffff;
		Start.DeltaValue = 0;
		GNode StartTarget;
		StartTarget.x = -1;
		StartTarget.y = -1;
		StartTarget.dv = 0;
		vector<GNode> pul;
		GNode o;
		o.x = 0;
		o.y = 0;
		pul.push_back(o);
		GNode anwser = DFSID(Max_Depth, Start, StartTarget, goMap).BestDraw;
		return anwser;
	}

}
