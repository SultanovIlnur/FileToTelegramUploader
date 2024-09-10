namespace FileToTelegramUploader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var arguments = Environment.GetCommandLineArgs()
                .Skip(1)
                .Select(arg => arg.Split(new[] { '=' }, 2))
                .Where(parts => parts.Length == 2)
                .ToDictionary(parts => parts[0].ToLowerInvariant(), 
                    parts => parts[1],
                    StringComparer.OrdinalIgnoreCase);

            // Define a helper method to retrieve arguments with fallback
            string GetArgument(string key, string environmentVariableName)
            {
                return arguments.TryGetValue(key, out var value)
                    ? value
                    : Environment.GetEnvironmentVariable(environmentVariableName);
            }

            // Retrieve arguments with fallback to environment variables
            string filePath = GetArgument("--filePath", "FILEPATH");
            string groupId = GetArgument("--groupId", "GROUP_ID");
            string botToken = GetArgument("--botToken", "BOT_TOKEN");
            bool logResult = GetArgument("--logResult", "LOG_RESULT")?.ToLower() == "true";

            filePath = filePath?.Trim('"');

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(botToken))
            {
                Console.WriteLine("Error: Missing arguments or environment variables for file path, group ID, or bot token.");
                return;
            }

            Console.WriteLine($"File Path: {filePath}");
            Console.WriteLine($"Group ID: {groupId}");
            Console.WriteLine($"Bot Token: {botToken}");

            // Upload file to Telegram
            string result = await UploadFileToTelegramAsync(filePath, groupId, botToken);

            Console.WriteLine(result);

            // Log result if logResult is true
            if (logResult)
            {
                string logFileName = $"executionResult_{DateTime.Now:yyyyMMdd_HHmmss}.log";
                AppendToLogFile(logFileName, result);
                Console.WriteLine($"Result logged to {logFileName}");
            }
        }

        // Function to upload a file to Telegram
        static async Task<string> UploadFileToTelegramAsync(string filePath, string groupId, string botToken)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var fileContent = new StreamContent(File.OpenRead(filePath));
                    var formData = new MultipartFormDataContent
                    {
                        { fileContent, "document", Path.GetFileName(filePath) }
                    };

                    string telegramApiUrl = $"https://api.telegram.org/bot{botToken}/sendDocument?chat_id={groupId}";
                    var response = await httpClient.PostAsync(telegramApiUrl, formData);

                    if (response.IsSuccessStatusCode)
                    {
                        return "File uploaded successfully!";
                    }
                    else
                    {
                        return $"Failed to upload file. Status code: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error uploading file: {ex.Message}";
                }
            }
        }

        static void AppendToLogFile(string logFileName, string result)
        {
            File.AppendAllText(logFileName, $"[{DateTime.Now}]: {result}{Environment.NewLine}", System.Text.Encoding.UTF8);
        }
    }
}
