using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace Example1.Dialogs
{
    [LuisModel("56fe6a3c-b017-4352-b24d-bd42a9940170", "863224eec48243e6b163c4bcbdd1a4c8")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Search")]
        public async Task Search(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            EntityRecommendation fileEntityRecommendation;

            if (result.TryFindEntity("File", out fileEntityRecommendation))
            {
                fileEntityRecommendation.Type = "Destination";
            }

            await context.PostAsync($"Executing query to find {fileEntityRecommendation.Entity}");

            context.Wait(this.MessageReceived);
        }
    }
}