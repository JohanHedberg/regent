using Regent.AI.RPG.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regent.AI.RPG.Test.Services
{
    public class ChatGptServiceTests
    {
        private readonly ChatGptService _subject;

        public ChatGptServiceTests()
        {
            var httpClient = new HttpClient();
            _subject = new ChatGptService(httpClient);
        }
    }
}