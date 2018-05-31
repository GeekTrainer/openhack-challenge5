using System.Threading.Tasks;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

namespace auth_test {
    public class AgentBot : IBot {
        public async Task OnTurn(ITurnContext context) {
            if(context.Activity.Type == ActivityTypes.Message) {
                var state = context.GetConversationState<AuthenticationState>();
                if(state.Authenticated) await context.SendActivity("You are logged in!!");
                else await context.SendActivity("You are not logged in");
            } else if(context.Activity.Type == ActivityTypes.ConversationUpdate) {
                await context.SendActivity(new Activity() {
                    Type = "event",
                    Text = context.Activity.From.Id,
                    Name = "authenticate"
                });
            } else if(context.Activity.Type == "event" && context.Activity.Text == "authenticated") {
                // authentication has taken place
                // check the state of the user and see if it's been updated
                var state = context.GetConversationState<AuthenticationState>();
                if(state.Authenticated) {
                    await context.SendActivity("You have been authenticated");
                }
            }
        }
    }

    public class AuthenticationState
    {
        public bool Authenticated { get; set; }
    }
}