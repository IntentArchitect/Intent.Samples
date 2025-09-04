using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;
using Microsoft.JSInterop;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.ModelDefinitionTemplate", Version = "1.0")]

namespace eShop.Web.Client.Services
{
    [IntentIgnore]
    public class BasketState
    {
        private readonly IJSRuntime _js;
        private const string BasketKey = "basket-id";

        public string Id { get; private set; } = string.Empty;
        public event Action<int>? OnCountChanged;

        public BasketState(IJSRuntime js)
        {
            _js = js;
        }

        public async Task InitializeAsync()
        {
            var storedId = await _js.InvokeAsync<string>("localStorage.getItem", BasketKey);

            if (!string.IsNullOrEmpty(storedId))
            {
                Id = storedId;
            }
            else
            {
                Id = ObjectIdGenerator.NewObjectId();
                await _js.InvokeVoidAsync("localStorage.setItem", BasketKey, Id);
            }
        }

        public void BasketUpdate(int count) => OnCountChanged?.Invoke(count);

        public async Task OrderPlacedAsync()
        {
            Id = ObjectIdGenerator.NewObjectId();
            await _js.InvokeVoidAsync("localStorage.setItem", BasketKey, Id);
            BasketUpdate(0);
        }
    }

    [IntentIgnore]
    public static class ObjectIdGenerator
    {
        private static readonly byte[] _random = new byte[5];
        private static int _counter = RandomNumberGenerator.GetInt32(0xFFFFFF);

        static ObjectIdGenerator()
        {
            RandomNumberGenerator.Fill(_random);
        }

        public static string NewObjectId()
        {
            Span<byte> bytes = stackalloc byte[12];

            // 4-byte timestamp
            int timestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            bytes[0] = (byte)((timestamp >> 24) & 0xFF);
            bytes[1] = (byte)((timestamp >> 16) & 0xFF);
            bytes[2] = (byte)((timestamp >> 8) & 0xFF);
            bytes[3] = (byte)(timestamp & 0xFF);

            // 5-byte random
            _random.CopyTo(bytes.Slice(4, 5));

            // 3-byte counter (increment safely)
            int counter = Interlocked.Increment(ref _counter) & 0xFFFFFF;
            bytes[9] = (byte)((counter >> 16) & 0xFF);
            bytes[10] = (byte)((counter >> 8) & 0xFF);
            bytes[11] = (byte)(counter & 0xFF);

            // Convert to 24-char hex string
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}