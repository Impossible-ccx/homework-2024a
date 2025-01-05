#pragma once
#include "../CAPI/CLIAPI.h"
#include "GoBangAI.h"
using namespace CAPI;
using namespace GobangAI;
namespace EXPAI
{
	extern "C++" {
		//__declspec(dllexport) CNode Draw(CDrawMap map, CPlayerSide pla);
		CNode Draw(CDrawMap map, CPlayerSide pla);
	}

	#define MaxSize 12
}
