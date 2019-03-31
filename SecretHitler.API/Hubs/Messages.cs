using System;
namespace SecretHitler.API.Hubs
{
    public static class WebSocketMessages
    {
        public const string PLAYER_JOINED = "PlayerJoined";
        public const string PLAYER_VOTED = "PlayerVoted";
        public const string START_GAME = "StartGame";
        public const string PLAYER_CHOSE = "PlayerChose";
        public const string NEXT_STATE = "NextState";
        public const string DISTRIBUTE_ROLE = "DistributeRole";
    }
}
