using System;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace CryptorWinUIDLL
{
    public class WindowsHelloAuth
    {
        /// <summary>
        /// ��������, �� ��������� Windows Hello �� �������.
        /// </summary>
        /// <returns>True, ���� ���������, ������ False.</returns>
        public async Task<bool> IsWindowsHelloAvailableAsync()
        {
            return await KeyCredentialManager.IsSupportedAsync();
        }

        /// <summary>
        /// ������ �������������� ����������� ����� Windows Hello.
        /// </summary>
        /// <returns>True, ���� �������������� ������, ������ False.</returns>
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
                    return false; // ������� ��������������
                }
            }
            return false; // Windows Hello �����������
        }
        public async Task<bool> AuthenticateAsync()
        {
            if (await IsWindowsHelloAvailableAsync())
            {
                try
                {
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                    // ����������, �� ��� � ���������� ����
                    var retrieveResult = await KeyCredentialManager.OpenAsync("CryptorCredential");

                    if (retrieveResult.Status == KeyCredentialStatus.Success)
                    {
                        // ���� ���� ����, �������� ��������� (�� ������ Windows Hello)
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

                    // ���� ���� �������, ��������� ����
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
                    return false; // ������� ��������������
                }
            }
            return false; // Windows Hello �����������
        }


    }
}
