using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using auth_test.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

namespace auth_test.Controllers
{
    public class AgentAuthenticationController : Controller
    {
        [Route("/api/agentauthentication/authenticate")]
        [Authorize()]
        public async Task<IActionResult> AuthenticateAsync(string id, IConfiguration configuration)
        {
            try {
                // create an adapter and context to load the state
                var adapter = new Microsoft.Bot.Builder.Adapters.BotFrameworkAdapter(new ConfigurationCredentialProvider(configuration));
                var context = new TurnContext(adapter, new Activity() {
                    From = new ChannelAccount(id),
                    Type = "event",
                    Text = "authenticated"
                });

                // load the state and set the Authenticated property
                var state = context.GetConversationState<AuthenticationState>();
                state.Authenticated = true;
                
                // Message the bot that authentication has taken place
                var bot = new AgentBot();
                await bot.OnTurn(context);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }
    }
}