using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace SpotiBot.Api.Bot
{
    /// <summary>
    /// This service only exists to override some defaults for sending messages.
    /// TODO: remove, or maybe combine with other services that are dependent on client?
    /// </summary>
    public class SendMessageService : ISendMessageService
    {
        private readonly ITelegramBotClient _telegramBotClient;

        private const ParseMode _defaultParseMode = ParseMode.Markdown;
        private const bool _disableWebPagePreview = true;

        public SendMessageService(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task<int> SendTextMessage(long chatId, string text, ParseMode parseMode = _defaultParseMode, bool disableWebPagePreview = _disableWebPagePreview, int replyToMessageId = 0, ReplyMarkup replyMarkup = null)
        {
            var replyParameters = replyToMessageId > 0
                ? new ReplyParameters { MessageId = replyToMessageId }
                : null;

            var linkPreviewOptions = new LinkPreviewOptions { IsDisabled = disableWebPagePreview };

            var message = await _telegramBotClient.SendMessage(new ChatId(chatId), text, parseMode: parseMode, linkPreviewOptions: linkPreviewOptions, replyParameters: replyParameters, replyMarkup: replyMarkup);

            return message.MessageId;
        }

        public Task EditMessageText(long chatId, int messageId, string text, ParseMode parseMode = _defaultParseMode, bool disableWebPagePreview = _disableWebPagePreview, InlineKeyboardMarkup replyMarkup = null)
        {
            var linkPreviewOptions = new LinkPreviewOptions { IsDisabled = disableWebPagePreview };

            return _telegramBotClient.EditMessageText(chatId, messageId, text, parseMode, linkPreviewOptions: linkPreviewOptions, replyMarkup: replyMarkup);
        }

        public async Task AnswerCallbackQuery(string callbackQueryId, string text = null, string url = null)
        {
            try
            {
                await _telegramBotClient.AnswerCallbackQuery(callbackQueryId, text, url: url);
            }
            catch (Exception exception)
            {
                // This may crash when the inline query is too old, just ignore it.
                if (exception?.Message == "Bad Request: query is too old and response timeout expired or query ID is invalid")
                    return;

                throw;
            }
        }

        public async Task AnswerInlineQuery(string inlineQueryId, IEnumerable<InlineQueryResultArticle> results, string switchPmText = null, string switchPmParameter = null)
        {
            try
            {
                var button = switchPmText != null
                    ? new InlineQueryResultsButton(switchPmText) { StartParameter = switchPmParameter }
                    : null;

                await _telegramBotClient.AnswerInlineQuery(inlineQueryId, results, cacheTime: 10, button: button);
            }
            catch (Exception exception)
            {
                // This may crash when the inline query is too old, just ignore it.
                if (exception?.Message == "Bad Request: query is too old and response timeout expired or query ID is invalid")
                    return;

                throw;
            }
        }
    }
}
