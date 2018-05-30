using System.Threading.Tasks;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

namespace auth_test {
    public class AgentBot : IBot {
        public async Task OnTurn(ITurnContext context) {
            if(context.Activity.Type != ActivityTypes.Message) return;

            await context.SendActivity("Hi there! I'm a sample echo bot");
        }
    }
}