#pragma once
#pragma managed
using namespace WpfApp;
namespace CppCliClass {
	public ref class AIPlayer
	{
	public:

		// 通过 IPlayer 继承
		virtual WpfApp::Node Draw(array<WpfApp::DrawType, 2>^ map, WpfApp::PlayerSide u, int maxx, int maxy);
		virtual void OnFailToDraw(array<WpfApp::DrawType, 2>^ map, WpfApp::Node preDraw);
	};

}
