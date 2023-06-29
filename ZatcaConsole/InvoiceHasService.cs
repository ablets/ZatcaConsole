using System.Diagnostics;

namespace ZatcaInvoiceHash;

public class InvoiceHashService
{
    public static string GenerateInvoiceHash(string xmlFilePath, string? fatooraPath)
    {
        if (!File.Exists(xmlFilePath))
        {
            Console.WriteLine($"Could not find the file {xmlFilePath}");
        }

        if (string.IsNullOrEmpty(fatooraPath))
        {
            fatooraPath = Environment.GetEnvironmentVariable("FATOORA_HOME", EnvironmentVariableTarget.User);
        }

        if (string.IsNullOrEmpty(fatooraPath))
        {
            throw new ArgumentNullException("FATOORA_HOME", "Environment variable is not set");
        }

        ProcessStartInfo startInfo = new()
        {
            FileName = "cmd.exe",
            Arguments = $"/c fatoora -generateHash -invoice \"{xmlFilePath}\"",
            WorkingDirectory = fatooraPath,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        Process process = new()
        {
            StartInfo = startInfo
        };
        process.Start();
        process.WaitForExit();

        string result = process.StandardOutput.ReadToEnd();

        if (result.Contains("invoice hash has been generated successfully"))
        {
            string invoiceHashText = "INVOICE HASH = ";
            int invoiceHashStartIndex = result.IndexOf(invoiceHashText) + invoiceHashText.Length;
            string invoiceHash = result.Substring(invoiceHashStartIndex).Trim();

            return invoiceHash;
        }
        else
        {
            return string.Empty;
        }
    }
}