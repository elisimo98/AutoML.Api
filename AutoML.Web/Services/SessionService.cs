using Microsoft.JSInterop;

namespace AutoML.Web.Services
{
    public class SessionService
    {
        private readonly IJSRuntime _jsRuntime;

        public SessionService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private async Task InvokeVoidSafeAsync(string identifier, params object[] args)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync(identifier, args);
            }
            catch (JSDisconnectedException)
            {
                // Circuit disconnected (user navigated away/closed tab).
                // Safely ignore the error for set operations.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during JS void interop: {ex.Message}");
            }
        }

        private async Task<string?> InvokeStringSafeAsync(string identifier, params object[] args)
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>(identifier, args);
            }
            catch (JSDisconnectedException)
            {
                // Circuit disconnected. Safely return null (or String.Empty, depending on preferred default).
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during JS string interop: {ex.Message}");
                return null;
            }
        }

        public async Task SetProjectIdAsync(string projectId)
        {
            await InvokeVoidSafeAsync("setSessionData", "projectId", projectId);
        }

        public async Task SetProjectNameAsync(string projectName)
        {
            await InvokeVoidSafeAsync("setSessionData", "projectName", projectName);
        }

        public async Task<string?> GetProjectNameAsync()
        {
            return await InvokeStringSafeAsync("getSessionData", "projectName");
        }

        public async Task<string?> GetProjectIdAsync()
        {
            return await InvokeStringSafeAsync("getSessionData", "projectId");
        }
    }
}