using Moq;
using Regent.AI.RPG.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regent.AI.RPG.Test.Services
{
    public class GameMasterServiceTests
    {
        private readonly GameMasterService _subject;

        public GameMasterServiceTests()
        {
            var _chatGtpMock = new Mock<IChatGptService>();
            _subject = new GameMasterService(_chatGtpMock.Object);
        }
    }
}
