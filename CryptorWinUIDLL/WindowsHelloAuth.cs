using System;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace CryptorWinUIDLL
{
    public class WindowsHelloAuth
    {
        /// <summary>
        /// Перевіряє, чи доступний Windows Hello на пристрої.
        /// </summary>
        /// <returns>True, якщо доступний, інакше False.</returns>
        public async Task<bool> IsWindowsHelloAvailableAsync()
        {
            return await KeyCredentialManager.IsSupportedAsync();
        }

        /// <summary>
        /// Виконує автентифікацію користувача через Windows Hello.
        /// </summary>
        /// <returns>True, якщо автентифікація успішна, інакше False.</returns>
        public async Task<bool> AuthenticateAsync1()
        {
            if (await IsWindowsHelloAvailableAsync())
            {
                try
                {
                    var result = await KeyCredentialManager.RequestCreateAsync("CryptorCredential", KeyCredentialCreationOption.ReplaceExisting);
                    return result.Status == KeyCredentialStatus.Success;
                }
                catch
                {
                    return false; // Помилка автентифікації
                }
            }
            return false; // Windows Hello недоступний
        }
        public async Task<bool> AuthenticateAsync()
        {
            if (await IsWindowsHelloAvailableAsync())
            {
                try
                {
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                    // Перевіряємо, чи вже є збережений ключ
                    var retrieveResult = await KeyCredentialManager.OpenAsync("CryptorCredential");

                    if (retrieveResult.Status == KeyCredentialStatus.Success)
                    {
                        // Якщо ключ існує, виконуємо підписання (це активує Windows Hello)
                        var credential = retrieveResult.Credential;
                        if (credential != null)
                        {
                            stopwatch.Restart();
                            var signResult = await credential.RequestSignAsync(new Windows.Storage.Streams.Buffer(32));
                            stopwatch.Stop();

                            System.Diagnostics.Debug.WriteLine($"Key signing time: {stopwatch.ElapsedMilliseconds} ms");

                            return signResult.Status == KeyCredentialStatus.Success;
                        }
                    }

                    // Якщо ключ відсутній, створюємо його
                    stopwatch.Restart();
                    var createResult = await KeyCredentialManager.RequestCreateAsync(
                        "CryptorCredential",
                        KeyCredentialCreationOption.FailIfExists
                    );
                    stopwatch.Stop();

                    System.Diagnostics.Debug.WriteLine($"Key creation time: {stopwatch.ElapsedMilliseconds} ms");

                    return createResult.Status == KeyCredentialStatus.Success;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Authentication error: {ex.Message}");
                    return false; // Помилка автентифікації
                }
            }
            return false; // Windows Hello недоступний
        }


    }
}
