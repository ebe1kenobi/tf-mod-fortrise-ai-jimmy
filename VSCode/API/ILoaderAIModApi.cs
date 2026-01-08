using System;
using TowerFall;

using System.Collections.Generic;

namespace TFModFortRiseLoaderAI;

public partial interface ILoaderAIModApi
{
  bool RegisterAgent(IList<IAgentLogic> logic);
  bool CanAddAgent();

	public interface IAgentLogic
	{
		public const int X = 0;

		public const int Y = 1;

		public const int JUMP_CHECK = 2;

		public const int JUMP_PRESSED = 3;

		public const int DODGE_CHECK = 4;

		public const int DODGE_PRESSED = 5;

		public const int SHOOT_CHECK = 6;

		public const int SHOOT_PRESSED = 7;

		public const int ALT_SHOOT_CHECK = 8;

		public const int ALT_SHOOT_PRESSED = 9;

		public const int ARROW_PRESSED = 10;

		string Type { get; }

		void Initialize(int index, KeyboardInput input);

		void SetLevel(Level level);

		List<int> Update();
	}

}
//namespace TFModFortRiseLoaderAI;

//public partial interface ILoaderAIModApi
//{
//  bool addAgent(String type, Agent[] agents);
//  bool CurrentPlayerIs(String type, int playerIndex);
//  String GetPlayerTypePlaying(int playerIndex);
//  String GetPlayerName(int playerIndex);
//  bool IsAgentPlaying(int playerIndex, Level level);
//  bool CanAddAgent();
//}
