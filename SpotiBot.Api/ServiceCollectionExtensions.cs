using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SpotiBot.Api.Bot;
using SpotiBot.Api.Bot.Chats;
using SpotiBot.Api.Bot.HandleUpdate;
using SpotiBot.Api.Bot.HandleUpdate.Commands;
using SpotiBot.Api.Bot.HandleUpdate.Dto;
using SpotiBot.Api.Bot.Users;
using SpotiBot.Api.Bot.Votes;
using SpotiBot.Api.Bot.WebApp;
using SpotiBot.Api.Library.Options;
using SpotiBot.Api.Spotify;
using SpotiBot.Api.Spotify.Api;
using SpotiBot.Api.Spotify.Authorization;
using SpotiBot.Api.Spotify.Playlists;
using SpotiBot.Api.Spotify.Tracks;
using SpotiBot.Api.Spotify.Tracks.AddTrack;
using SpotiBot.Api.Spotify.Tracks.RemoveTrack;
using SpotiBot.Api.Spotify.Tracks.SyncHistory;
using SpotiBot.Api.Spotify.Tracks.SyncTracks;
using Telegram.Bot;

namespace SpotiBot.Api
{
    /// <summary>
    /// Extension methods that group all dependencies in a logical way.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // TODO: Check if the dependency tree is efficient enough.

            // Bot dependencies.
            return services
                .AddTransient<ICommandsService, CommandsService>()
                .AddTransient<IHandleMessageService, HandleMessageService>()
                .AddTransient<IHandleCallbackQueryService, HandleCallbackQueryService>()
                .AddTransient<IHandleInlineQueryService, HandleInlineQueryService>()
                .AddTransient<IHandleCommandService, HandleCommandService>()
                .AddTransient<IHandleInlineQueryCommandService, HandleInlineQueryCommandService>()
                .AddTransient<IUpdateDtoService, UpdateDtoService>()
                .AddTransient<ISendMessageService, SendMessageService>()
                .AddTransient<IKeyboardService, KeyboardService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IVoteService, VoteService>()
                .AddTransient<IVoteTextHelper, VoteTextHelper>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IVoteRepository, VoteRepository>()
                .AddTransient<IChatRepository, ChatRepository>()
                .AddTransient<IChatMemberRepository, ChatMemberRepository>()
                .AddTransient<IWebAppValidationService, WebAppValidationService>()

                // TODO: only use 1 http client, so inject it here.
                .AddSingleton<ITelegramBotClient>((serviceProvider) =>
                {
                    var options = serviceProvider.GetService<IOptions<TelegramOptions>>().Value;

                    return new TelegramBotClient(options.AccessToken);
                })

                // Spotify dependencies.
                .AddTransient<IAuthorizationService, AuthorizationService>()
                .AddTransient<ILoginRequestService, LoginRequestService>()
                .AddTransient<IAddTrackService, AddTrackService>()
                .AddTransient<IRemoveTrackService, RemoveTrackService>()
                .AddTransient<ISyncTracksService, SyncTracksService>()
                .AddTransient<ISyncHistoryService, SyncHistoryService>()
                .AddTransient<IParseHistoryJsonService, ParseHistoryJsonService>()
                .AddTransient<ISpotifyLinkHelper, SpotifyLinkHelper>()
                .AddTransient<IReplyMessageService, ReplyMessageService>()
                .AddTransient<IAuthorizationTokenRepository, AuthorizationTokenRepository>()
                .AddTransient<ILoginRequestRepository, LoginRequestRepository>()
                .AddTransient<ITrackRepository, TrackRepository>()
                .AddTransient<IPlaylistRepository, PlaylistRepository>()

                .AddTransient<ISpotifyClientFactory, SpotifyClientFactory>()
                .AddTransient<ISpotifyClientService, SpotifyClientService>();
        }

        /// <summary>
        /// Add classes that are needed for data storage.
        /// </summary>
        /// <param name="services">The services we want to add our storage to.</param>
        public static IServiceCollection AddStorage(this IServiceCollection services)
        {
            // Add the TableServiceClient as a singleton.
            return services.AddSingleton((serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<AzureOptions>>().Value;

                return new TableServiceClient(options.StorageAccountConnectionString);
            });
        }

        /// <summary>
        /// Add a mapper.
        /// </summary>
        /// <param name="services">The services we want to add our mapper to.</param>
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            return services
                .AddTransient<ApiModels.IMapper, ApiModels.Mapper>()
                .AddTransient<Bot.Chats.IMapper, Bot.Chats.Mapper>()
                .AddTransient<Bot.Users.IMapper, Bot.Users.Mapper>()
                .AddTransient<Bot.WebApp.IMapper, Bot.WebApp.Mapper>()
                .AddTransient<Spotify.Authorization.IMapper, Spotify.Authorization.Mapper>()
                .AddTransient<Spotify.Playlists.IMapper, Spotify.Playlists.Mapper>()
                .AddTransient<Spotify.Tracks.IMapper, Spotify.Tracks.Mapper>();
        }

        /// <summary>
        /// Get all options from the configuration and bind them to their respective options models.
        /// The configuration contains the appsettings in Azure, or local.settings.json when running local.
        /// </summary>
        /// <param name="services">The services we want to add our options models to.</param>
        public static IServiceCollection AddOptions(this IServiceCollection services)
        {
            return services
                .AddAndBindOptions<SentryOptions>()
                .AddAndBindOptions<AzureOptions>()
                .AddAndBindOptions<SpotifyOptions>()
                .AddAndBindOptions<TelegramOptions>()
                .AddAndBindOptions<TestOptions>();
        }

        private static IServiceCollection AddAndBindOptions<T>(this IServiceCollection services) where T : class
        {
            var optionsName = typeof(T).Name;

            // Add the values from the configuration to this options' type registration.
            services
                .AddOptions<T>()
                .Configure<IConfiguration>((options, configuration) =>
                    configuration.GetSection(optionsName).Bind(options));

            return services;
        }
    }
}